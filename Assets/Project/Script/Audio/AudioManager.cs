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
    AudioMixerGroup music_mixer_group = null;
    [SerializeField]
    AudioMixerGroup sounds_mixer_group = null;

    List<AudioSource> sources = new List<AudioSource>();

    MusicGroup current_music_group = null;

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

    public void AddMusic(AudioClip clip)
    {
        if (current_music_group == null)
            current_music_group = gameObject.AddComponent<MusicGroup>();

        current_music_group.MixerGroup = music_mixer_group;
        current_music_group.Add(clip);
    }

    public void ChangeMusic(AudioClip clip)
    {
        if (current_music_group != null)
            current_music_group.Stop();

        current_music_group = gameObject.AddComponent<MusicGroup>();
        current_music_group.MixerGroup = music_mixer_group;
        current_music_group.Add(clip);
    }

    public void PlaySound(ESound_Type sound, Vector3 position)
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
            source.outputAudioMixerGroup = sounds_mixer_group;
            source.spatialBlend = 1.0f;
            source.Play();
            sources.Add(source);
            StartCoroutine(ManageSourceDestruct(source));
        }
        else
            throw new UnassignedReferenceException("Sound Missing !");
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
            PlaySound(ESound_Type.Sword, new Vector3(0f, 0f, 0f));
    }


    #region sounds

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
    
    #endregion

}


