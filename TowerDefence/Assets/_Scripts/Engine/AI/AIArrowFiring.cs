using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.AI
{
    public class AIArrowFiring : MonoBehaviour
    {
        public Transform Target;

        [Range(20.0f, 75.0f)] public float LaunchAngle;
        public Combat.AttackDefinition attackDefinition;
        [HideInInspector] Stats.CharacterStats stats;
        public GameObject _projectile;

        private Transform Projectile;



        void Start()
        {
            stats = GetComponent<Stats.CharacterStats>();
        }

        public void FireArrow()
        {
            Projectile = Instantiate(_projectile, transform.position+Vector3.up, Quaternion.identity).transform;
            Projectile.GetComponent<Combat.projectile>().attackDef = attackDefinition;
            Projectile.GetComponent<Combat.projectile>().stats = stats;
            
            // StartCoroutine(SimulateProjectile());
            Launch();
        }


        //IEnumerator SimulateProjectile()
        //{
        //    // Short delay added before Projectile is thrown
        //    yield return new WaitForSeconds(1.5f);

        //    // Move projectile to the position of throwing object + add some offset if needed.
        //    Projectile.position = myTransform.position + new Vector3(0, 1.2f, 0);

        //    // Calculate distance to target
        //    float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        //    // Calculate the velocity needed to throw the object to the target at specified angle.
        //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //    // Extract the X  Y componenent of the velocity
        //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //    // Calculate flight time.
        //    float flightDuration = target_Distance / Vx;

        //    // Rotate projectile to face the target.
        //    Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        //    float elapse_time = 0;

        //    while (elapse_time < flightDuration)
        //    {
        //        Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

        //        elapse_time += Time.deltaTime;

        //        yield return null;
        //    }
        //}


        void Launch()
        {
            // think of it as top-down view of vectors: 
            //   we don't care about the y-component(height) of the initial and target position.
            Vector3 projectileXZPos = new Vector3(Projectile.transform.position.x, 0.0f, Projectile.transform.position.z);
            Vector3 targetXZPos = new Vector3(Target.position.x, 0.0f, Target.position.z);

            // rotate the object to face the target
            Projectile.LookAt(targetXZPos);
           // transform.LookAt(targetXZPos);
            // shorthands for the formula
            float R = Vector3.Distance(projectileXZPos, targetXZPos);
            float G = Physics.gravity.y;
            float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
            float H = Target.position.y - Projectile.transform.position.y;

            // calculate the local space components of the velocity 
            // required to land the projectile on the target object 
            float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
            float Vy = tanAlpha * Vz;

            // create the velocity vector in local space and get it in global space
            Vector3 localVelocity = new Vector3(0f, Vy, Vz);
            Vector3 globalVelocity = Projectile.transform.TransformDirection(localVelocity);

            // launch the object by setting its initial velocity and flipping its state
            Projectile.GetComponent<Rigidbody>().velocity = globalVelocity;
        }
    }
}
