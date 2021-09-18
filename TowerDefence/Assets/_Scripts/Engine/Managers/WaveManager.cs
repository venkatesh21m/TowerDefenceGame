using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Managers
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager instance;

        public Wave[] Wave;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }
    }


    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public int numberofenemies;
    }

}
