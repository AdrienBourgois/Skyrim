using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    static private AudioManager instance;
    static public AudioManager Instance
    {
        get
        {
            return instance ?? (instance = FindObjectOfType<AudioManager>());
        }
    }

    public enum EMusicType
    {
        Menu,
        Game,
        Fight,
        Any
    }
    [SerializeField]
    private EMusicType currentMusicType = EMusicType.Any;
    public enum ESoundType
    {
        Sword,
        SwordSwish
    }

    //Musics
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameCalmMusic;
    [SerializeField] private AudioClip gameFightMusic;

    //Sounds
    [SerializeField] private List<AudioClip> swordClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> swordSwishClips = new List<AudioClip>();

    //Mixer Groups
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundsMixerGroup;

    private MusicGroup currentMusicGroup;

    [SerializeField]
    private int countForFightMusic;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayMusic(EMusicType _music)
    {
        if (_music != currentMusicType || _music == EMusicType.Fight)
        {
            if (_music == EMusicType.Menu)
            {
                ChangeMusic(menuMusic);
                currentMusicType = EMusicType.Menu;
            }
            else if (_music == EMusicType.Game)
            {
                if (currentMusicType == EMusicType.Fight)
                {
                    countForFightMusic--;
                    if (countForFightMusic == 0)
                    {
                        currentMusicGroup.State = MusicGroup.EPlayState.PlaySingle;
                        currentMusicType = EMusicType.Game;
                    }
                }
                else
                {
                    ChangeMusic(gameCalmMusic);
                    AddMusic(gameFightMusic);
                    currentMusicType = EMusicType.Game;
                }
            }
            else if (_music == EMusicType.Fight)
            {
                if (currentMusicType == EMusicType.Game || currentMusicType == EMusicType.Fight)
                {
                    countForFightMusic++;
                    currentMusicGroup.State = MusicGroup.EPlayState.PlayFull;
                    currentMusicType = EMusicType.Fight;
                }
            }
        }
    }

    private void AddMusic(AudioClip _clip)
    {
        if (currentMusicGroup == null)
            currentMusicGroup = gameObject.AddComponent<MusicGroup>();

        currentMusicGroup.MixerGroup = musicMixerGroup;
        currentMusicGroup.Add(_clip);
    }

    private void ChangeMusic(AudioClip _clip)
    {
        if (currentMusicGroup != null)
            currentMusicGroup.State = MusicGroup.EPlayState.Stop;

        currentMusicGroup = gameObject.AddComponent<MusicGroup>();
        currentMusicGroup.MixerGroup = musicMixerGroup;
        currentMusicGroup.Add(_clip);
        currentMusicGroup.State = MusicGroup.EPlayState.PlaySingle;
    }

    #region sounds

    [Useless]
    public void PlaySound(ESoundType _sound, Vector3 _position)
    {
        AudioClip clip = null;
        if (_sound == ESoundType.Sword)
            clip = GetRandClip(swordClips);
        else if (_sound == ESoundType.SwordSwish)
            clip = GetRandClip(swordSwishClips);

        if (clip)
        {
            GameObject go = new GameObject();
            go.transform.position = _position;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            go.name = "Sound : " + source.clip.name;
            source.outputAudioMixerGroup = soundsMixerGroup;
            source.spatialBlend = 1.0f;
            source.dopplerLevel = 0f;
            source.Play();
            StartCoroutine(ManageSourceDestruct(source));
        }
        else
            throw new UnassignedReferenceException("Sound Missing !");
    }

    private AudioClip GetRandClip(List<AudioClip> _clips)
    {
        return _clips[Random.Range(0, _clips.Count)];
    }

    private IEnumerator ManageSourceDestruct(AudioSource _source)
    {
        while (_source.isPlaying)
            yield return new WaitUntil(() => _source.isPlaying);
        Destroy(_source.gameObject);
    }
    
    #endregion


    private void Update()
    {
        if(Input.GetKeyDown("o"))
            PlayMusic(EMusicType.Fight);
        else if (Input.GetKeyDown("l"))
            PlayMusic(EMusicType.Game);
    }
}


