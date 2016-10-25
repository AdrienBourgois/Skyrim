using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class MusicGroup : MonoBehaviour {

    List<AudioSource> sub_sources = new List<AudioSource>();
    List<AudioClip> sub_clips = new List<AudioClip>();
    AudioClip main_clip = null;
    AudioSource main_source = null;

    public AudioMixerGroup MixerGroup { private get; set; }

    public enum EPlayState
    {
        PlaySingle,
        PlayFull,
        Stop,
    }

    private EPlayState state = EPlayState.PlaySingle;
    public EPlayState State
    {
        get { return state; }
        set
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

    public void Add(AudioClip clip)
    {
        if (main_clip == null)
            main_clip = clip;
        else
            sub_clips.Add(clip);
    }

    private void ToSinglePlay()
    {
        if (sub_sources.Count > 0)
        {
            foreach (AudioSource source in sub_sources)
            {
                StartCoroutine(CrossFadeDownAndDestroy(source));
            }
        }
        if(main_source == null)
            Sync(main_clip);
    }

    void ToFullPlay()
    {
        if (!main_source.isPlaying)
            Sync(main_clip);
        foreach (AudioClip clip in sub_clips)
            Sync(clip);
    }

    private void Sync(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        if (main_source != null)
        {
            if (main_source.isPlaying)
                source.timeSamples = main_source.timeSamples;
        }

        source.loop = true;
        source.clip = clip;
        source.outputAudioMixerGroup = MixerGroup;
        source.volume = 0f;
        source.dopplerLevel = 0f;
        source.Play();

        if (main_source == null)
            main_source = source;
        else
            sub_sources.Add(source);

        StartCoroutine(CrossFadeUp(source));
    }

    private IEnumerator Stop()
    {
        if (sub_sources.Count > 0)
        {
            foreach (AudioSource source in sub_sources)
            {
                StartCoroutine(CrossFadeDownAndDestroy(source));
            }
        }
        StartCoroutine(CrossFadeDownAndDestroy(main_source));
        yield return new WaitForSeconds(1.0f);
        Destruct();
    }

    private IEnumerator CrossFadeUp(AudioSource source)
    {
        float previous_time = Time.time;
        float delta = 0f;

        while (source.volume < 1f)
        {
            delta += Time.time - previous_time;
            source.volume = delta;

            previous_time = Time.time;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CrossFadeDownAndDestroy(AudioSource source)
    {
        float previous_time = Time.time;
        float delta = 0f;

        while (source.volume > 0f)
        {
            delta += Time.time - previous_time;
            source.volume = 1f - delta;

            previous_time = Time.time;
            yield return new WaitForEndOfFrame();
        }

        sub_sources.Remove(source);
        Destroy(source);
    }

    void Destruct()
    {
        Destroy(main_source);
        foreach (AudioSource source in sub_sources)
            Destroy(source);

        Destroy(this);
    }
}
