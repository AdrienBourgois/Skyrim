using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class MusicGroup : MonoBehaviour {

    List<AudioSource> sources = new List<AudioSource>();
    List<AudioClip> clips_to_add = new List<AudioClip>();
    bool ready_to_destruct = false;
    bool is_playing_subclips = false;

    private AudioMixerGroup mixer_group;
    public AudioMixerGroup MixerGroup
    {
        get { return mixer_group; }
        set { mixer_group = value; }
    }


    public void Add(AudioClip clip)
    {
        clips_to_add.Add(clip);
    }

    void Update()
    {
        if(clips_to_add.Count != 0)
        {
            foreach (AudioClip clip in clips_to_add)
                Sync(clip);

            clips_to_add.Clear();
        }

        if (ready_to_destruct)
            Destruct();
    }

    private void Sync(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        if (sources.Count != 0)
            source.timeSamples = sources[0].timeSamples;

        source.loop = true;
        source.clip = clip;
        source.outputAudioMixerGroup = MixerGroup;
        source.Play();
        source.volume = 0f;
        StartCoroutine(CrossFade_Up(source));

        sources.Add(source);
    }

    public void Stop()
    {
        foreach (AudioSource source in sources)
        {
            StartCoroutine(CrossFade_Down(source));
        }
    }

    private IEnumerator CrossFade_Up(AudioSource source)
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

    private IEnumerator CrossFade_Down(AudioSource source)
    {
        float previous_time = Time.time;
        float delta = 0f;

        while (source.volume > 0f)
        {
            ready_to_destruct = false;
            delta += Time.time - previous_time;
            source.volume = 1f - delta;

            previous_time = Time.time;
            yield return new WaitForEndOfFrame();
        }
        ready_to_destruct = true;
    }

    void Destruct()
    {
        foreach (AudioSource source in sources)
            Destroy(source);

        Destroy(this);
    }
}
