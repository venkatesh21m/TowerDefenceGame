using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rudrac.TowerDefence.Combat
{

    public class Cloud : MonoBehaviour
    {

        [Tooltip("assign attackdefinition SO here")]
        [HideInInspector]
        public AttackDefinition attackDef;
        [HideInInspector]
        public Stats.CharacterStats stats;

        [HideInInspector]
        public GameObject _object;

        public LineRenderer LineRenderer;

        public float thunderRepeatingTime;
        public float radius;
        public float chargeDownTime = 0.5f;

        // Start is called before the first frame update
        void Start()
        {
            LineRenderer.useWorldSpace = true;
            InvokeRepeating("DoLighning", 1, thunderRepeatingTime);

        }

       void DoLighning()
        {
            Ray newRay = new Ray(new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2),transform.position.y-0.6f,-.8f), Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(newRay, out hit))
            {
                LineRenderer.SetPosition(0, newRay.origin);
                LineRenderer.SetPosition(1, hit.point);

                Collider[] hitColliders = Physics.OverlapSphere(hit.point, radius);
                foreach (var hitCollider in hitColliders)
                {
                    _object = hitCollider.gameObject;
                    Attack();
                }
            }
        }

        public void Attack()
        {
            //Create attack
            var attack = attackDef.CreateAttack(stats, _object.GetComponent<Stats.CharacterStats>());

            Debug.Log("enemy collided");

            //Get all attackables on the enemy
            var attackables = _object.GetComponentsInParent<IAttackable>();

            //call interfase function on each attackables
            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttack(stats.gameObject, attack);
            }
            
            Invoke("ChargeDown", chargeDownTime);

        }

        void ChargeDown()
        {
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, transform.position);
        }
    }
}
