using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rudrac.TowerDefence;


namespace Rudrac.TowerDefence.Combat
{
    public class AttackedScrollingText : MonoBehaviour,IAttackable
    {
        public extras.ScrollingText text;
        public Color TextColor;

        public void OnAttack(GameObject attacker, Attack attack)
        {
            var _text = ((int)attack.Damage).ToString();
            var scrollingText = Instantiate(text, transform.position, Quaternion.identity);
            scrollingText.transform.parent = transform;
            scrollingText.SetText(_text);
            scrollingText.SetColor(TextColor);
        }
    }
}
