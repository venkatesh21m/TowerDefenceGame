using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class Attack 
    {

        #region variables

        private readonly float _damage;
        private readonly bool _Critical;

        #endregion

        #region Properties
        public float Damage
        {
            get { return _damage; }
        }

        public bool isCritical
        {
            get { return _Critical; }
        }
        #endregion

        #region Methods

        public Attack(float damage, bool critical)
        {
            _damage = damage;
            _Critical = critical;
        }
        
       
        #endregion

    }
}