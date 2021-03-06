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

        public bool playerflagnotinplace = true;
        public bool enemyflagnotinplace = true;

        [Space]
        public float distanebetweenflagstowin = .25f;
        Transform playerflagStartPos;
        Transform enemyflagStartPos;

        public Transform PlayerflagStartPos
        {
            get
            {
                return playerflagStartPos;
            }
        }
        public Transform EnemyflagStartPos
        {
            get
            {
                return enemyflagStartPos;
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
            playerflagStartPos = playerflag;
            enemyflagStartPos = Enemyflag;
        }

        // Update is called once per frame
        void Update()
        {
            float distancebetweentwoflags = Vector3.Distance(Enemyflag.position, playerflag.position);
            if(distancebetweentwoflags < distanebetweenflagstowin)
            {
                //TODO: implement winning
            }

            if (Vector3.Distance(playerflagStartPos.position, playerflag.position) > 5)
            {
                playerflagnotinplace = false;
            }
            else
            {
                playerflagnotinplace = true;
            }

            if (Vector3.Distance(enemyflagStartPos.position, Enemyflag.position) > 5)
            {
                enemyflagnotinplace = false;
            }
            else
            {
                enemyflagnotinplace = true;
            }
        }
    }
}
