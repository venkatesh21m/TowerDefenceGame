using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rudrac.TowerDefence;

namespace Rudrac.TowerDefence.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {

        public ItemPickUP_SO ItemDefinition;

        public Stats.CharacterStats stats;

        Inventory inventory;

        /// <summary>
        /// constructor
        /// </summary>
        public ItemPickUp()
        {
            inventory = Inventory.instance;
        }


        // Start is called before the first frame update
        void Start()
        {
            if(stats == null)
            {
                stats = FindObjectOfType<PlayerController>().gameObject.GetComponent<Stats.CharacterStats>();
            }
        }

       public void UseItem()
        {
            switch (ItemDefinition.skilltype)
            {
                case SkillType.Weapon:
                    stats.changeWeapon(ItemDefinition.SkillDefinition);
                    break;
                case SkillType.Troop:
                    //
                    break;
                default:
                    break;
            }
        }
       
    }
}
