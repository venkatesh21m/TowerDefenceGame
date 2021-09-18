using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence
{
    public class PointAim : MonoBehaviour
    {
        public GameObject Arrow;
        
        [Tooltip("position where arrow spawns")]
        [SerializeField] Transform ArrowSpawnPos;
        
        [Tooltip("Starting velocity of the arrow")]
        [SerializeField] float launchspeed;

        [Header(" to display path")]
        [SerializeField] GameObject dot;
        GameObject[] dots;
        [SerializeField] int numberofPoints;
        [SerializeField] float spacebetweenpoints;

        [Header("components")]
        [SerializeField] Animator anim;
        [SerializeField] AudioSource Asource;

        [Header("AudioClips")]
        [SerializeField] AudioClip ArrowFiringClip;

        PlayerController pc;
        RaycastHit RayHit;
        Ray ray;
        Vector2 direction;
        private bool canshoot = true;



        [SerializeField] bool LowAngle;

        private float currentSpeed;
        private float currentAngle;
        private float currentTimeOfFlight;





        // Start is called before the first frame update
        void Start()
        {
            pc = GetComponent<PlayerController>();
            dots = new GameObject[numberofPoints];
            for (int i = 0; i < numberofPoints; i++)
            {
                dots[i] = Instantiate(dot, ArrowSpawnPos);
            }

            if(anim == null)
            {
                anim = GetComponentInChildren<Animator>();
            }
            if(Asource == null)
            {
                Asource = GetComponent<AudioSource>();
            }
           
        }

        private void OnDisable()
        {
            for (int i = 0; i < numberofPoints; i++)
            {
                Destroy(dots[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 bowposition = transform.position;
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RayHit, 100, LayerMask.GetMask("raycast")))
            {

                var Hitpoint = RayHit.point;
                SetTargetWithSpeed(Hitpoint, launchspeed, LowAngle);


                //Debug.DrawLine(Camera.main.transform.position, Hitpoint, Color.blue, 0.1f);
                //direction = new Vector2(Hitpoint.x,Hitpoint.y) - bowposition;
                //ArrowSpawnPos.right = direction;
                if (Input.GetMouseButtonDown(0) && canshoot)
                {
                    shoot();
                }

                //for (int i = 0; i < numberofPoints; i++)
                //{
                //    Vector2 pos = dotposition(i * spacebetweenpoints);
                //    dots[i].transform.position = new Vector3(pos.x, pos.y, ArrowSpawnPos.position.z);

                //}

            }
        }

        /// <summary>
        /// function to fire an arrow
        /// </summary>
        private void shoot()
        {
            if(FindObjectOfType<Inventory.Inventory>().GetSkillType() == Inventory.SkillType.Weapon)
            {
                canshoot = false;
                // pc.canwalk = false;
                anim.SetTrigger("FireArrow");
                
                // Invoke("ResetShoot", 1.25f);
                FindObjectOfType<Inventory.Inventory>().StartTimer();
                Fire();

                Asource.PlayOneShot(ArrowFiringClip);
            }

        }


        Vector2 dotposition(float t)
        {
            Vector2 pos =new Vector2(ArrowSpawnPos.position.x, ArrowSpawnPos.position.y) + direction.normalized * launchspeed * t + .5f * new Vector2(Physics.gravity.x, Physics.gravity.y) * (t*t);
            return pos;
        }

       
       public void ResetShoot()
        {
            canshoot = true;
           // pc.canwalk = true;
        }







        public void SetTargetWithSpeed(Vector3 point, float speed, bool useLowAngle)
        {
            currentSpeed = launchspeed;

            Vector3 direction = point - ArrowSpawnPos.position;
            float yOffset = direction.y;
            direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
            float distance = direction.magnitude;

            float angle0, angle1;
            bool targetInRange = ProjectileMath.LaunchAngle(speed, distance, yOffset, Physics.gravity.magnitude, out angle0, out angle1);

            if (targetInRange)
                currentAngle = useLowAngle ? angle1 : angle0;

            //projectileArc.UpdateArc(speed, distance, Physics.gravity.magnitude, currentAngle, direction, targetInRange);
            SetTurret(direction, currentAngle * Mathf.Rad2Deg);

            currentTimeOfFlight = ProjectileMath.TimeOfFlight(currentSpeed, currentAngle, -yOffset, Physics.gravity.magnitude);
        }

        private void SetTurret(Vector3 planarDirection, float turretAngle)
        {
            transform.rotation = Quaternion.LookRotation(planarDirection) * Quaternion.Euler(0, -90, 0);
            ArrowSpawnPos.localRotation = Quaternion.Euler(90, 90, 0) * Quaternion.AngleAxis(turretAngle, Vector3.left);
        }

        public void Fire()
        {
            //GameObject p = Instantiate(Arrow, ArrowSpawnPos.position, Quaternion.identity);
            //p.GetComponent<Rigidbody>().velocity = ArrowSpawnPos.up * currentSpeed;


            GameObject arrow = Instantiate(Arrow, ArrowSpawnPos.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody>().velocity = ArrowSpawnPos.up * currentSpeed;
            arrow.GetComponent<Combat.projectile>().stats = GetComponent<Stats.CharacterStats>();

        }










    }
}
