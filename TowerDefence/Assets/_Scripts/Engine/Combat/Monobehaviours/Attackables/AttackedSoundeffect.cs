using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rudrac.TowerDefence;


namespace Rudrac.TowerDefence.Combat
{
    [RequireComponent(typeof(AudioSource))]
    public class AttackedSoundeffect : MonoBehaviour,IAttackable
    {
        public AudioClip[] clips;

        public void OnAttack(GameObject attacker, Attack attack)
        {
            GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}