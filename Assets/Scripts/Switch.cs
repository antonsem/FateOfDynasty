using System;
using System.Collections;
using UnityEngine;

namespace GGJ21
{
    public class Switch : Item
    {
        [SerializeField] private AudioClip slideSound;
        [SerializeField] private Vector3 _endPos;
        private Vector3 _startPos;
        
        private void OnEnable()
        {
            _startPos = transform.localPosition;
        }

        protected override void Use()
        {
            if(_used) return;
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            float t = 0;
            float slideTime = 1;
            if (slideSound)
            {
                if (audioSource)
                {
                    audioSource.PlayOneShot(slideSound);
                    slideTime = 1 / (slideSound.length / audioSource.pitch);
                }
                else
                    AudioPlayer.PlaySound(slideSound);
            }
            
            while (t < 1)
            {
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, t);
                t += Time.deltaTime * slideTime;
                yield return null;
            }

            transform.localPosition = _endPos;
            base.Use();
            Events.Instance.switchPressed?.Invoke();
        }
    }
}