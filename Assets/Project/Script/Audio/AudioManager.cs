using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public enum EMusic_Type
    {
        Menu,
        Game,
        Fight
    }
    public enum ESound_Type
    {
        Sword,
        SwordSwish,
    }

    //Musics
    [SerializeField]
    AudioClip menu_music = null;
    [SerializeField]
    AudioClip game_music = null;
    [SerializeField]
    AudioClip fight_music = null;

    [SerializeField]
    AudioClip sample1 = null;
    [SerializeField]
    AudioClip sample2 = null;

    //Sounds
    [SerializeField]
    List<AudioClip> sword_clips = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> sword_swish_clips = new List<AudioClip>();

    //Mixer Groups
    [SerializeField]
    AudioMixerGroup music_group = null;
    [SerializeField]
    AudioMixerGroup sounds_group = null;

    List<AudioSource> sources = new List<AudioSource>();

    List<AudioSource> current_music_group = null;
    List<AudioSource> next_music_group = null;

    AudioSource NextMusicSource
    {
        get {
            next_music_group = new List<AudioSource>();
            AudioSource source = gameObject.AddComponent<AudioSource>();
            next_music_group.Add(source);
            source.loop = true;
            source.outputAudioMixerGroup = music_group;
            return source;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Play(EMusic_Type music)
    {
        if (music == EMusic_Type.Menu)
            ChangeMusic(menu_music);
        else if (music == EMusic_Type.Game)
            ChangeMusic(game_music);
        else if (music == EMusic_Type.Fight)
            ChangeMusic(fight_music);
    }

    public void Play(ESound_Type sound, Vector3 position)
    {
        AudioClip clip = null;
        if (sound == ESound_Type.Sword)
            clip = GetRandClip(sword_clips);

        if (clip)
        {
            GameObject go = new GameObject();
            go.transform.position = position;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            go.name = "Sound : " + source.clip.name;
            source.outputAudioMixerGroup = sounds_group;
            source.spatialBlend = 1.0f;
            source.Play();
            sources.Add(source);
            StartCoroutine(ManageSourceDestruct(source));
        }
        else
            throw new UnassignedReferenceException("Sound Missing !");
    }

    void ChangeMusic(AudioClip clip)
    {
        AudioSource next_source = NextMusicSource;
        next_source.clip = clip;
        next_source.Play();
        StartCoroutine(CrossFade(current_music_group, next_music_group));
    }

    void AddMusic(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = music_group;
        source.loop = true;
        current_music_group.Add(source);
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
            ChangeMusic(menu_music);
        else if (Input.GetKeyDown("g"))
            ChangeMusic(game_music);
        else if (Input.GetKeyDown("f"))
            ChangeMusic(fight_music);

        else if (Input.GetKeyDown("1"))
            ChangeMusic(sample1);
        if (Input.GetKeyDown("2"))
            AddMusic(sample2);

        if (Input.GetMouseButtonDown(1))
            Play(ESound_Type.Sword, new Vector3(0f, 0f, 0f));
    }

    private IEnumerator CrossFade(List<AudioSource> group_out, List<AudioSource> group_in)
    {
        float previous_time = Time.time;
        float delta = 0f;
        bool done = false;

        while (!done)
        {
            done = true;

            delta += Time.time - previous_time;
            if (group_out != null)
            {
                foreach (AudioSource source_out in group_out)
                {
                    if (source_out.volume != 0f)
                    {
                        source_out.volume = 1f - delta;
                        done = false;
                    }
                }
            }
            foreach (AudioSource source_in in group_in)
            {
                if (source_in.volume != 1f)
                {
                    source_in.volume = delta;
                    done = false;
                }
            }

            previous_time = Time.time;
            yield return new WaitForEndOfFrame();
        }

        if(group_out != null)
            foreach (AudioSource source_out in group_out)
                Destroy(source_out);

        current_music_group = next_music_group;

        StopCoroutine("ChangeMusic");
    }

    private AudioClip GetRandClip(List<AudioClip> clips)
    {
        return clips[Random.Range(0, clips.Count)];
    }

    private IEnumerator ManageSourceDestruct(AudioSource source)
    {
        while (source.isPlaying)
            yield return new WaitUntil(delegate { return source.isPlaying; });
        sources.Remove(source);
        Destroy(source.gameObject);
    }
}


