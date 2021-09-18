using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rudrac.TowerDefence
{

    public class PlayerController : MonoBehaviour
    {

        private bool OnTower = true;
        private bool NearTower;

        [SerializeField] float Speed;
        [SerializeField] Animator anim;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform ArrowAimobject;
        [SerializeField] Tower tower;

        public Transform ArrowFirePosition;
        public GameObject Arrow;
        public bool canwalk = true;

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


                Debug.Log(canwalk);

                if (canwalk)
                {
                    Debug.Log(horizontalInput);
                    anim.SetFloat("MovementSpeed", horizontalInput);

                    //applying vector to transform
                    transform.Translate(Movement);
                }
                else
                {
                    anim.SetFloat("MovementSpeed", 0);
                }

                if (verticalInput > 0 && NearTower)
                {
                    //TODO: Climb Up the Tower
                    transform.position = /*transform.TransformPoint(*/tower.TopPos.position;
                    OnTower = true;
                }
            }
            else
            {
                anim.SetFloat("MovementSpeed", 0);

                if (verticalInput < 0)
                {
                    //TODO: Climb down the Tower
                    transform.position = tower.BottomPos.position;
                    OnTower = false;
                    canwalk = true;
                }
            }
            #endregion

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tower"))
            {
                NearTower = true;
                tower = other.GetComponent<Tower>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Tower"))
            {
                NearTower = false;
                tower = null;
            }
        }

    }

}