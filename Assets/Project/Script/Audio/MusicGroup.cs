using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class MusicGroup : MonoBehaviour {
    private List<AudioSource> subSources = new List<AudioSource>();
    private List<AudioClip> subClips = new List<AudioClip>();
    private AudioClip mainClip;
    private AudioSource mainSource;

    public AudioMixerGroup MixerGroup { private get; set; }

    public enum EPlayState
    {
        Init,
        PlaySingle,
        PlayFull,
        Stop
    }

    private EPlayState state = EPlayState.Init;
    public EPlayState State
    {
        set
        {
            if (state != value)
            {
                state = value;
                if (value == EPlayState.Stop)
                    StartCoroutine(Stop());
                else if (value == EPlayState.PlaySingle)
                    ToSinglePlay();
                else if (value == EPlayState.PlayFull)
                    ToFullPlay();
            }
        }
    }

    public void Add(AudioClip _clip)
    {
        if (mainClip == null)
            mainClip = _clip;
        else
            subClips.Add(_clip);
    }

    private void ToSinglePlay()
    {
        if (subSources.Count > 0)
            foreach (AudioSource source in subSources)
                StartCoroutine(CrossFadeDownAndDestroy(source));

        if(mainSource == null)
            Sync(mainClip);
    }

    private void ToFullPlay()
    {
        if (!mainSource.isPlaying)
            Sync(mainClip);
        foreach (AudioClip clip in subClips)
            Sync(clip);
    }

    private void Sync(AudioClip _clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(source);

        if (mainSource != null)
        {
            if (mainSource.isPlaying)
                source.timeSamples = mainSource.timeSamples;
        }

        source.loop = true;
        source.clip = _clip;
        source.outputAudioMixerGroup = MixerGroup;
        source.volume = 0f;
        source.dopplerLevel = 0f;
        source.Play();

        if (mainSource == null)
            mainSource = source;
        else
            subSources.Add(source);

        StartCoroutine(CrossFadeUp(source));
    }

    private IEnumerator Stop()
    {
        if (subSources.Count > 0)
        {
            foreach (AudioSource source in subSources)
            {
                StartCoroutine(CrossFadeDownAndDestroy(source));
            }
        }
        StartCoroutine(CrossFadeDownAndDestroy(mainSource));
        yield return new WaitForSeconds(1.0f);
        Destruct();
    }

    private IEnumerator CrossFadeUp(AudioSource _source)
    {
        float previousTime = Time.time;
        float delta = 0f;

        while (_source != null && _source.volume < 1f)
        {
            delta += Time.time - previousTime;
            _source.volume = delta;

            previousTime = Time.time;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CrossFadeDownAndDestroy(AudioSource _source)
    {
        float previousTime = Time.time;
        float delta = 0f;

        while (_source.volume > 0f)
        {
            delta += Time.time - previousTime;
            _source.volume = 1f - delta;

            previousTime = Time.time;
            yield return new WaitForEndOfFrame();
        }

        subSources.Remove(_source);
        Destroy(_source);
    }

    private void Destruct()
    {
        Destroy(mainSource);
        foreach (AudioSource source in subSources)
            Destroy(source);

        Destroy(this);
    }
}
