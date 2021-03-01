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
            if (Physics.Raycast(ray, out RayHit))
            {
                var Hitpoint = RayHit.point;
                Debug.DrawLine(Camera.main.transform.position, Hitpoint, Color.blue, 0.1f);
                direction = new Vector2(Hitpoint.x,Hitpoint.y) - bowposition;
                ArrowSpawnPos.right = direction;
                if (Input.GetMouseButtonDown(0) && canshoot)
                {
                    shoot();
                }

                for (int i = 0; i < numberofPoints; i++)
                {
                    Vector2 pos = dotposition(i * spacebetweenpoints);
                    dots[i].transform.position = new Vector3(pos.x, pos.y, ArrowSpawnPos.position.z);

                }

            }
        }

        /// <summary>
        /// function to fire an arrow
        /// </summary>
        private void shoot()
        {
            
           canshoot = false;
           pc.canwalk = false;

           anim.SetTrigger("FireArrow");
           Invoke("ResetShoot", 1.25f);
           GameObject arrow = Instantiate(Arrow, ArrowSpawnPos.position, ArrowSpawnPos.rotation);
           arrow.GetComponent<Rigidbody>().velocity = ArrowSpawnPos.right * launchspeed;

           Asource.PlayOneShot(ArrowFiringClip);
        }


        Vector2 dotposition(float t)
        {
            Vector2 pos =new Vector2(ArrowSpawnPos.position.x, ArrowSpawnPos.position.y) + direction.normalized * launchspeed * t + .5f * new Vector2(Physics.gravity.x, Physics.gravity.y) * (t*t);
            return pos;
        }

        void ResetShoot()
        {
            canshoot = true;
            pc.canwalk = true;
        }
    }
}
