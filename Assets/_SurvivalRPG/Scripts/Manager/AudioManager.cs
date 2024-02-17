using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager instance;
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    [Tooltip(" 0: Dead, 1: Hit, 2: LevelUp, 3: Lose, 4: Melee, 5: Range, 6: Drop, 7: Win, 8: Loot, 9:Confirm, 10:Buy, 11:LootCoin")] public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp, Lose, Melee, Range, Drop, Win, Loot, Confirm, Buy, LootCoin }

    void Awake()
    {
        instance = this;
        Init();
        RegisterGameEvent();
    }
    private void RegisterGameEvent()
    {
        GameManager.Instance.GameOver += GameOver;

        GameManager.Instance.GameStart += GameStart;
    }

    private void GameStart()
    {

    }

    private void GameOver()
    {

    }

    void Init()
    {

        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void SetVolumeBMG(GameObject go)
    {
        bgmVolume = go.GetComponent<Slider>().value / 100;
        bgmPlayer.volume = bgmVolume;
    }
    public void SetVolumeFX(GameObject go)
    {
        sfxVolume = go.GetComponent<Slider>().value / 100;

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }
    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex] && sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = UnityEngine.Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            if (sfxPlayers[loopIndex] != null)
            {
                sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
                sfxPlayers[loopIndex].Play();
            }
            break;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Initialize SfxClips")]

    private void InitializeSfxClips()
    {
        // Get the values of the Sfx enum
        Sfx[] sfxValues = (Sfx[])Enum.GetValues(typeof(Sfx));

        sfxClips = new AudioClip[sfxValues.Length];

        Debug.Log("SfxClips initialized in Editor.");
        EditorUtility.SetDirty(this);
    }
#endif

}
