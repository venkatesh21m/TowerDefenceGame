using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rudrac.TowerDefence;

namespace Rudrac.TowerDefence.Inventory
{

    [CreateAssetMenu(fileName = "Item",menuName = "TowerDefence/Inventory/Item")]
    public class ItemPickUP_SO : ScriptableObject
    {
        public string itemName = "New Item";
        [Space]
        [Tooltip("Icon to show in Inventory Bar")]
        public Sprite itemIcon;
        [Space]
        public SkillType skilltype;

        [Header("For Skill")]
        public Combat.Weapon SkillDefinition;

        [Header("For Troop")]
        public Combat.Troop TroopDefinition;
      
        //[Header("For Troop")]
        //Troop definition

        public void UseItem(Stats.CharacterStats stats )
        {
            switch (skilltype)
            {
                case SkillType.Weapon:
                    stats.changeWeapon(SkillDefinition);
                    break;
                case SkillType.Troop:
                    //get troop prefab
                    //instantiate in the level
                    break;
                default:
                    break;
            }
        }
    }

    public enum SkillType
    {
        Weapon,
        Troop
    }
}
