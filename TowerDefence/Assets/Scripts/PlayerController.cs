using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rudrac.TowerDefence
{

    public class PlayerController : MonoBehaviour
    {

        private bool OnTower = true;
        private bool CanFire = true;
        private bool NearTower;
        private Vector3 Target;

        [SerializeField] float Speed;
        [SerializeField] float ArrowSpeed = 15;
        [SerializeField] float turnSpeed = 2;
        [SerializeField] Animator anim;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform ArrowAimobject;
        
        public Transform ArrowFirePosition;
        public GameObject Arrow;
        Ray InputRay;
        RaycastHit hitpoint;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            #region Movement 
            // Getting Input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (!OnTower)  // checking whether on the Tower. if not continue
            {
                // calculating the translation vector
                Vector3 Movement = new Vector3(horizontalInput * Speed * Time.deltaTime, 0, 0);
               
                //applying vector to transform
                transform.Translate(Movement);

                if (verticalInput > 0 && NearTower)
                {
                    //TODO: Climb Up the Tower
                }
            }
            else
            {
                if (verticalInput < 0)
                {
                    //TODO: Climb down the Tower
                }
            }
            #endregion

            #region PlayerInput to Fire
            if (Input.GetMouseButtonDown(0))
            {
                InputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
               

                if (Physics.Raycast(InputRay, out hitpoint,100) && CanFire)
                {
                  //  hitpoint.point = new Vector3(hitpoint.point.x, hitpoint.point.y, 0);
                    //Fire Arrow
                    StartCoroutine(FireArrow(hitpoint.point));

                }
            }
            #endregion
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(InputRay.origin, hitpoint.point);
            Gizmos.DrawWireSphere(hitpoint.point,1);
        }

        private IEnumerator FireArrow(Vector3 TargetPosition)
        {
            //Target = TargetPosition;
            CanFire = false;
            //TargetPosition.z = 0;
            //Vector3 direction = (TargetPosition - ArrowFirePosition.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

            //float? angle = RotateHands();


            //Creating arrow
            GameObject arrow = Instantiate(Arrow, ArrowFirePosition.position, Quaternion.identity);
            //adding projectile values
            projectile projectile = arrow.GetComponent<projectile>();
            projectile.TargetObjectTF = TargetPosition;
            projectile.Launch();

            anim.SetTrigger("FireArrow");
            
            yield return new WaitForSeconds(1.5f);

            CanFire = true;
        }

        //float? CalculateAngle(bool low)
        //{
        //    Vector3 targetDir = ArrowAimobject.position - Target;
        //    float y = targetDir.y;
        //    targetDir.y = 0;
        //    float x = targetDir.magnitude;
        //    float gravity = 9.81f;
        //    float sSqr = ArrowSpeed * ArrowSpeed;
        //    float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);
        //    Debug.LogError(underTheSqrRoot);
        //    if (underTheSqrRoot >= 0f)
        //    {
        //        float root = Mathf.Sqrt(underTheSqrRoot);
        //        float highAngle = sSqr + root;
        //        float lowAngle = sSqr - root;
        //        if (low)
        //            return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
        //        else
        //            return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //float? RotateHands()
        //{
        //    float? angle = CalculateAngle(true);
            
        //    Debug.LogError(angle);

        //    if(angle!= null)
        //    {
        //       ArrowAimobject.localEulerAngles = new Vector3(360f - (float)angle, 0, 0);
        //    }
            
        //    return angle;
        //}
    }

}