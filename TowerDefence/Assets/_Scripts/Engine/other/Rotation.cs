using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Utills
{
  
    public class Rotation : MonoBehaviour
    {
        public Vector3 rotationAxis;


        // Update is called once per frame
        public void Update()
        {
            transform.Rotate(rotationAxis * Time.deltaTime);
        }
    }
}
