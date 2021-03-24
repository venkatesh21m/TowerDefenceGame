using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public interface IDestructable 
    {
       void OnDestruction(GameObject destroyer);
    }
}
