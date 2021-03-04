using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rudrac.TowerDefence.Combat
{
    public class LigningArrow : projectile
    {
        public GameObject Lightingprefab;
        public float destructionTime = 10;
        bool activated = false;
        public virtual void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Space) && !activated)
            {
                activated = true;
                GameObject cloud = Instantiate(Lightingprefab, transform.position, Quaternion.identity);
                Cloud c = cloud.GetComponent<Cloud>();
                c.stats = stats;
                c.attackDef = attackDef;
                Destroy(cloud, destructionTime);
                Destroy(gameObject);
            }
        }
    }
}
