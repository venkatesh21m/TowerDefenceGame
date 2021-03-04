using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rudrac.TowerDefence.Inventory.UI
{
    public class InventoryUiScript : MonoBehaviour
    {
        public Image sprite;
        public Image Fillingsprite;
        public TMPro.TMP_Text text;
        public bool canFire = false;
        ItemPickUP_SO inventoryitem;
        float startTime;

        public void setDetails(ItemPickUP_SO inventoryItem)
        {
            this.inventoryitem = inventoryItem;
            text.text = inventoryItem.itemName;
            sprite.sprite = inventoryItem.itemIcon;
            startTimer();

        }

        public void selected(ItemPickUP_SO item)
        {
            Fillingsprite.color = Color.green;
            if (startTime >= inventoryitem.SkillDefinition.attackRate)
            {
                canFire = true;
                FindObjectOfType<PointAim>().ResetShoot();
            }

        }

        public void notselected()
        {
            Fillingsprite.color = Color.blue;
            
        }

        private void Update()
        {
            if (!canFire) {
                startTime += Time.deltaTime;

                Fillingsprite.transform.localScale = new Vector3(startTime / inventoryitem.SkillDefinition.attackRate, 1, 1);

                if (startTime >= inventoryitem.SkillDefinition.attackRate)
                {
                    canFire = true;
                    FindObjectOfType<PointAim>().ResetShoot();
                }
            }
        }

        internal void startTimer()
        {
            canFire = false;
            startTime = 0;
        }
    }

  

}
