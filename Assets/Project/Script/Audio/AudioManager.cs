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

    AudioSource music1 = null;
    AudioSource music2 = null;

    AudioSource current_music_source = null;
    AudioSource NextMusicSource
    {
        get {
            if (current_music_source == music1)
                current_music_source = music2;
            else
                current_music_source = music1;
            return current_music_source;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        music1 = gameObject.AddComponent<AudioSource>();
        music2 = gameObject.AddComponent<AudioSource>();
        music1.outputAudioMixerGroup = music_group;
        music2.outputAudioMixerGroup = music_group;
        music1.loop = true;
        music2.loop = true;
        current_music_source = music1;
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
        AudioSource current_source = current_music_source;
        AudioSource next_source = NextMusicSource;
        next_source.clip = clip;
        next_source.Play();
        StartCoroutine(CrossFade(current_source, next_source));
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
            ChangeMusic(menu_music);
        else if (Input.GetKeyDown("g"))
            ChangeMusic(game_music);
        else if (Input.GetKeyDown("f"))
            ChangeMusic(fight_music);

        if (Input.GetMouseButtonDown(1))
            Play(ESound_Type.Sword, new Vector3(0f, 0f, 0f));
    }

    private IEnumerator CrossFade(AudioSource source_out, AudioSource source_in)
    {
        float previous_time = Time.time;
        float delta = 0f;

        while (source_out.volume > 0f || source_in.volume < 1f)
        {
            delta += Time.time - previous_time;
            source_out.volume = 1f - delta;
            source_in.volume = delta;
            previous_time = Time.time;
            yield return new WaitForEndOfFrame();
        }

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


