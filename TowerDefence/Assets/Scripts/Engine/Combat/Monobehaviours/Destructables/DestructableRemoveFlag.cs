using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class DestructableRemoveFlag : MonoBehaviour,IDestructable
    {
        AI.AIMovement aIMovement;

        private void OnEnable()
        {
            aIMovement = GetComponent<AI.AIMovement>();
        }

        public void OnDestruction(GameObject destroyer)
        {
            if(aIMovement != null && aIMovement.carryingflag)
            {
                if (GetComponent<Stats.CharacterStats>().enemy)
                {
                    Managers.LevelManager.instance.playerflag.transform.parent = transform.parent;
                }
                else
                {
                    Managers.LevelManager.instance.Enemyflag.transform.parent = transform.parent;
                }
            }
        }

    }
}
