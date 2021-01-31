using System.Collections;
using System.Collections.Generic;
using ExtraTools;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [SerializeField] private int initializeSources = 4;

    private static List<AudioSource> _sources = new List<AudioSource>();
    private static GameObject _thisObject;
    private IEnumerator _setMusicVolume;

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
}