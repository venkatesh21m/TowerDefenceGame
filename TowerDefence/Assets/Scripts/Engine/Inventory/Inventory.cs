using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rudrac.TowerDefence.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;

        public Stats.CharacterStats stats;
        public Image[] hotbarImages;

        public List<ItemPickUP_SO> inventoryitmes;
        
        int currentID;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                if(inventoryitmes[i] != null)
                    hotbarImages[i].sprite = inventoryitmes[i].itemIcon;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                UseItem(1);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                UseItem(2);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                UseItem(3);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                UseItem(4);
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                UseItem(5);
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                UseItem(6);
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                UseItem(7);
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                UseItem(8);
            else if (Input.GetKeyDown(KeyCode.Alpha9))
                UseItem(9);
            else if (Input.GetKeyDown(KeyCode.Alpha0))
                UseItem(10);
        }

        public void UseItem(int id)
        {
            hotbarImages[currentID].transform.parent.GetComponent<Image>().color = Color.white;
            hotbarImages[id - 1].transform.parent.GetComponentInParent<Image>().color = Color.green;
            currentID = id - 1;
            if (inventoryitmes[id - 1] == null) stats.removeWeapon();
            inventoryitmes[id - 1].UseItem(stats);
        }
    
    }
}