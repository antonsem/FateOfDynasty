using System;
using UnityEngine;
using Random = System.Random;

namespace GGJ21
{
    public class Flicker : MonoBehaviour
    {
        [SerializeField] private Light lightSource;
        [SerializeField, Range(0, 0.1f)] private float flickerRange = 0.05f;

        private float defaultIntensity = 1;
        private float timer = 0;

        private void Awake()
        {
            defaultIntensity = lightSource.intensity;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer > 0) return;
            timer = UnityEngine.Random.Range(0.05f, 0.1f);
            lightSource.intensity = UnityEngine.Random.Range(defaultIntensity - flickerRange, defaultIntensity + flickerRange);
        }

        private void Reset()
        {
            lightSource = GetComponent<Light>();
        }
    }
}