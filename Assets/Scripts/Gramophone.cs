using System;
using System.Collections;
using UnityEngine;

namespace GGJ21
{
    public class Gramophone : Item
    {
        [SerializeField] private AudioClip scratch;

        private bool _scratching = false;
        
        protected override void Use()
        {
            if(!_used)
                Events.Instance.displayMessage?.Invoke(data.use);
            
            _used = true;
            if (audioSource.isPlaying)
                Stop();
            else
                Play();
        }

        private void Stop()
        {
            if(_scratching) return;
            audioSource.Stop();
            StartCoroutine(Scratch(null));
        }
        
        private void Play()
        {
            if(_scratching) return;
            StartCoroutine(Scratch(() =>
            {
                audioSource.clip = data.sound;
                audioSource.Play();
            }));
        }

        private IEnumerator Scratch(Action callback)
        {
            _scratching = true;
            audioSource.PlayOneShot(scratch);
            while (audioSource.isPlaying)
                yield return null;
            
            callback?.Invoke();
            _scratching = false;
        }
    }
}