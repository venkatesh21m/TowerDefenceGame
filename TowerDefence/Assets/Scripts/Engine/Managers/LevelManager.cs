using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        public Transform Enemyflag;
        public Transform playerflag;

        [Space]
        public Transform playerTroopStartpos;
        public Transform enemyTroopStartpos;

        [Space]
        public float distanebetweenflagstowin = .25f;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            float distancebetweentwoflags = Vector3.Distance(Enemyflag.position, playerflag.position);
            if(distancebetweentwoflags < distanebetweenflagstowin)
            {
                //TODO: implement winning
            }
        }
    }
}
