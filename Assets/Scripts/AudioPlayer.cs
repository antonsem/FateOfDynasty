using System;
using System.Collections;
using System.Collections.Generic;
using ExtraTools;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [SerializeField] private int initializeSources = 4;
    [SerializeField] private AudioMixer mixer;
    [SerializeField, Range(-80, 20)] private float defaultVolume = 0;

    private const string _volumeLabel = "Volume";
    private static List<AudioSource> _sources = new List<AudioSource>();
    private static GameObject _thisObject;
    private IEnumerator _setMusicVolume;

    private void Start()
    {
        SetMusic(true);
    }

    private void OnDisable()
    {
        if (_setMusicVolume == null) return;
        StopCoroutine(_setMusicVolume);
        _setMusicVolume = null;
        _sources.Clear();
    }

    public override void Init()
    {
        _sources.Clear();
        _thisObject = gameObject;
        for (int i = 0; i < initializeSources; i++)
            _sources.Add(_thisObject.AddComponent<AudioSource>());
    }

    public static void PlaySound(in AudioClip clip, float minPitch = 0, float maxPitch = 0)
    {
        for (int i = 0; i < _sources.Count; i++)
        {
            if (_sources[i].isPlaying) continue;

            _sources[i].pitch = Random.Range(1 - minPitch, 1 + maxPitch);
            _sources[i].PlayOneShot(clip);
            return;
        }

        AudioSource source = _thisObject.AddComponent<AudioSource>();
        source.loop = false;
        source.pitch = Random.Range(1 - minPitch, 1 + maxPitch);
        source.PlayOneShot(clip);
        _sources.Add(source);
    }
    
    public void SetMusic(bool state, Action callback = null)
    {
        if (_setMusicVolume != null)
        {
            StopCoroutine(_setMusicVolume);
            _setMusicVolume = null;
        }
            
        mixer.GetFloat(_volumeLabel, out float _);
        _setMusicVolume = VolumeCoroutine(state ? defaultVolume : -80, callback);
        StartCoroutine(_setMusicVolume);
    }
    
    private IEnumerator VolumeCoroutine(float setTo, Action callback = null)
    {
        mixer.GetFloat(_volumeLabel, out float currentVolume);
        while (Mathf.Abs(currentVolume - setTo) > 0.025f)
        {
            currentVolume = Mathf.Lerp(currentVolume, setTo, Time.deltaTime * 3);
            mixer.SetFloat(_volumeLabel, currentVolume);
            yield return null;
        }

        mixer.SetFloat(_volumeLabel, setTo);
        _setMusicVolume = null;
        callback?.Invoke();
    }
}