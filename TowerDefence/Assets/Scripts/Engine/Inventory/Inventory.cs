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
        public UI.InventoryUiScript[] hotbarUIitems;
        [System.Serializable]
        public struct inventoryRow { public List<ItemPickUP_SO> inventoryRowitems; }

        public inventoryRow[] inventoryitmes;
        int currentRow;
        int currentID;
        // Start is called before the first frame update
        void Start()
        {
            currentRow = 0;

            SetData();

            UseItem(1);

        }

        void SetData()
        {
             for (int j = 0; j < inventoryitmes[currentRow].inventoryRowitems.Count; j++)
             {
                if (inventoryitmes[currentRow].inventoryRowitems[j] != null)
                {
                    hotbarUIitems[j].setDetails(inventoryitmes[currentRow].inventoryRowitems[j]);
                }
                else 
                {
                    hotbarUIitems[j].setDetails();
                }
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


            if(Input.mouseScrollDelta == Vector2.down)
            {
                currentRow++;
                if(currentRow> inventoryitmes.Length-1)
                {
                    currentRow = 0;
                }

                SetData();
                UseItem(1);
            }
            
            if(Input.mouseScrollDelta == Vector2.up)
            {
                currentRow--;
                if(currentRow< 0)
                {
                    currentRow = inventoryitmes.Length-1;
                }

                SetData();
                UseItem(1);
            }
        }

        public void UseItem(int id)
        {


            for (int i = 0; i < hotbarUIitems.Length; i++)
            {
                if (i == id - 1)
                {
                    hotbarUIitems[i].selected(inventoryitmes[currentRow].inventoryRowitems[i]);
                    
                    if (inventoryitmes[currentRow].inventoryRowitems[i] == null) stats.removeWeapon();
                    
                    if(inventoryitmes[currentRow].inventoryRowitems[i]!= null)
                        inventoryitmes[currentRow].inventoryRowitems[i].UseItem(stats);
                    
                    currentID = i;

                }
                else
                    hotbarUIitems[i].notselected();
            }
        }

        public void StartTimer()
        {
            hotbarUIitems[currentID].startTimer();
        }

    }
}
