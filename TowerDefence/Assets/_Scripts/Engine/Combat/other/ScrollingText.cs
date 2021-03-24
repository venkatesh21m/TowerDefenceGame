using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rudrac.TowerDefence.extras
{
    public class ScrollingText : MonoBehaviour
    {
        public float duration = 1;
        public float Speed;

        private TMP_Text textMesh;
        private float startTime;

        private void Awake()
        {
            textMesh = GetComponent<TMP_Text>();
            startTime = Time.time;
        }
        
        // Update is called once per frame
        void Update()
        {
            if(Time.time - startTime < duration)
            {
                transform.LookAt(Camera.main.transform);
                transform.Translate(Vector3.up * Speed * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetText(string text)
        {
            textMesh.text = text;
        }

        public void SetColor(Color color)
        {
            textMesh.color = color;
        }
    }
}
