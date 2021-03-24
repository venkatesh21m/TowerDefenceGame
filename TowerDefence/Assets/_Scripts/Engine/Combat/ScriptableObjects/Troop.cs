using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    [CreateAssetMenu(fileName ="Troop",menuName ="TowerDefence/Troop")]
    public class Troop : ScriptableObject
    {
        public GameObject TroopPrefab;
        public int numberofCharacters;
        public float ResetTime = 15;
        [Space]
        public characterStats_SO troopstats;
    }
}
