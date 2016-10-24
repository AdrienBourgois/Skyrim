using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    private AudioManager instance;
    public AudioManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

            return instance;
        }
    }

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
    AudioClip game_calm_music = null;
    [SerializeField]
    AudioClip game_fight_music = null;

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

    List<AudioSource> sound_sources = new List<AudioSource>();

    MusicGroup current_music_group = null;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayMusic(EMusic_Type music)
    {
        /*if (music == EMusic_Type.Menu)
            ChangeMusic(menu_music);
        else if (music == EMusic_Type.Game)
            ChangeMusic(game_music);
        else if (music == EMusic_Type.Fight)
            ChangeMusic(fight_music);*/
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
            current_music_group.State = MusicGroup.EPlayState.Stop;

        current_music_group = gameObject.AddComponent<MusicGroup>();
        current_music_group.MixerGroup = music_mixer_group;
        current_music_group.Add(clip);
        current_music_group.State = MusicGroup.EPlayState.PlaySingle;
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
            ChangeMusic(menu_music);
        else if (Input.GetKeyDown("g"))
        {
            ChangeMusic(game_calm_music);
            AddMusic(game_fight_music);
        }
        else if (Input.GetKeyDown("f"))
            current_music_group.State = MusicGroup.EPlayState.PlayFull;
        else if (Input.GetKeyDown("c"))
            current_music_group.State = MusicGroup.EPlayState.PlaySingle;

        if (Input.GetMouseButtonDown(1))
            PlaySound(ESound_Type.Sword, new Vector3(0f, 0f, 0f));
    }


    #region sounds

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
            source.dopplerLevel = 0f;
            source.Play();
            sound_sources.Add(source);
            StartCoroutine(ManageSourceDestruct(source));
        }
        else
            throw new UnassignedReferenceException("Sound Missing !");
    }

    private AudioClip GetRandClip(List<AudioClip> clips)
    {
        return clips[Random.Range(0, clips.Count)];
    }

    private IEnumerator ManageSourceDestruct(AudioSource source)
    {
        while (source.isPlaying)
            yield return new WaitUntil(delegate { return source.isPlaying; });
        sound_sources.Remove(source);
        Destroy(source.gameObject);
    }
    
    #endregion

}


