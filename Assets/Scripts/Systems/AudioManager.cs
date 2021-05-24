using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    protected AudioManager()
    {
    } // guarantee this will be always a singleton only - can't use the constructor!

    public enum AudioType { Music, Ambience, Atmosphere, SFX }

    public static float globalVolume = 1.0f;

    public static bool soundOn = true;

    public class AudioSourceHandle
    {
        private AudioSource source;
        private AudioType type;
        private int index;
        private bool isPaused = false;

        public AudioSourceHandle(AudioSource source, AudioClip clip, AudioType type, int index)
        {
            this.source = source;
            this.type = type;
            this.index = index;
            source.clip = clip;
        }

        public void Play(float startVolume, bool loop)
        {
            source.volume = startVolume * globalVolume;
            source.loop = loop;
            source.Play();
        }

        public void PauseSound()
        {
            if (source.isPlaying && !isPaused)
            {
                isPaused = true;
                source.Pause();
            }
        }

        public void ContinueSound()
        {
            if (isPaused)
            {
                source.UnPause();
            }
        }

        public void StopSound()
        {
            source.Stop();
        }

        public void ReleaseHandle()
        {

        }

        public void ChangeVolume(float volume)
        {
            if (source.isPlaying || isPaused)
            {
                source.volume = Mathf.Clamp(volume * globalVolume, 0.0f, 1.0f);
            }
        }
    }

    private static int MAX_SOURCES = 5;

    ////////////////////////////////////////////////////AUDIO STRINGS////////////////////////////////////////////////////////////////////////////////

    public static string Ambience_warehouse = "Factory Ambience Better";
    public static string Ambience_livingQuarters = "Factory Ambience Better";
    public static string Ambience_airDuct = "ES_Air Duct Ambience - SFX Producer";

    public static string Atmosphere_nightSky = "Ariza Ambience";
    public static string Atmosphere_salvation = "Docks_ Ambience";

    public static string Music_action = "Ariza Action";
    public static string Music_boss = "Ariza Boss";
    public static string Music_chase = "Chase";
    public static string Music_motive = "Motive";
    public static string Music_motiveReprise = "Motive Reprise";
    public static string Music_threeStages = "Three Stages";

    public static string SFX_footstepsMetallic = "50723__rutgermuller__footsteps-metallic-muffled";
    public static string SFX_doorOpen = "431117__inspectorj__door-front-opening-a";
    public static string SFX_doorClose = "Door Close-SoundBible.com-1305692306";
    public static string SFX_scifiDoorOpen = "543404__alexo400__sci-fi-door";
    public static string SFX_deathBeats = "Braam - Show Title";
    public static string SFX_deathImpact = "Impact - Deep Down";
    public static string SFX_newDayCello = "Braam - Sneaky Cello";
    public static string SFX_gotCaught = "Braam - Zone End";
    public static string SFX_gotCaughtBass = "Short - Bass Boy";
    public static string SFX_dangerPulse = "Braam - Retro Pulse";
    public static string SFX_dangerDrone = "Drone - Magic Pill";
    public static string SFX_airDuctFootsteps = "ES_Ship Footsteps - SFX Producer";
    public static string SFX_airDuctCrawl = "ES_Ventilation Shaft 2 - SFX Producer";
    public static string SFX_hit = "Punch - Pop";
    public static string SFX_hitGasp = "Punch - Splok";
    public static string SFX_hitGasp2 = "Punch - Sploop";
    public static string SFX_textPopup = "Short - Mini Popup";
    public static string SFX_interactionMenuPopup = "Short - Ploppy Plop";
    public static string SFX_failedInteraction = "Short - Stutter 303";
    public static string SFX_swishAndPop = "Short - Swish";


    ////////////////////////////////////////////////////AUDIO STRINGS END////////////////////////////////////////////////////////////////////////////////

    private bool musicIsPaused = false;
    private bool ambienceIsPaused = false;
    private bool atmosphereIsPaused = false;
    private bool SFXIsPaused = false;

    private AudioSource[] ambienceAudioSources = new AudioSource[MAX_SOURCES];
    private AudioSource[] musicAudioSources = new AudioSource[MAX_SOURCES];
    private AudioSource[] atmosphericAudioSources = new AudioSource[MAX_SOURCES];
    private AudioSource[] SFXAudioSources = new AudioSource[MAX_SOURCES];

    private AudioClip[] ambienceClips;
    private AudioClip[] musicClips;
    private AudioClip[] atmosphericClips;
    private AudioClip[] SFXClips;

    // Start is called before the first frame update
    void Awake()
    {
        ambienceClips = Resources.LoadAll<AudioClip>("Audio/Ambience");
        musicClips = Resources.LoadAll<AudioClip>("Audio/Music");
        atmosphericClips = Resources.LoadAll<AudioClip>("Audio/Atmosphere");
        SFXClips = Resources.LoadAll<AudioClip>("Audio/SFX");
    }

    private AudioClip GetClipFromArray(AudioClip[] clips, string clipName)
    {
        AudioClip clip = null;
        if (clips != null)
        {
            for (int index = 0; index < clips.Length; index++)
            {
                if (clips[index].name == clipName)
                {
                    clip = clips[index];
                    break;
                }
            }
        }
        return clip;
    }

    public void StopAllSound()
    {
        foreach (AudioType type in Enum.GetValues(typeof(AudioType)))
        {
            StopSound(type);
        }
    }

    public void PauseAllSound()
    {
        foreach (AudioType type in Enum.GetValues(typeof(AudioType)))
        {
            PauseSound(type);
        }
    }

    public void ContinueAllSound()
    {
        foreach (AudioType type in Enum.GetValues(typeof(AudioType)))
        {
            ContinueSound(type);
        }
    }

    public AudioSourceHandle GetAudioSourceHandle(string clipName, AudioType clipType)
    {
        AudioClip[] clips = GetClipArrayByType(clipType);
        AudioClip soundClip = GetClipFromArray(clips, clipName);
        if (soundClip == null)
        {
            Debug.LogError("Couldn't find clip with this name and type");
            return null;
        }
        AudioSource[] sources = GetSourceArrayByType(clipType);
        for (int i = 0; i < MAX_SOURCES; i++)
        {
            if (sources[i] == null)
            {
                sources[i] = Camera.main.gameObject.AddComponent<AudioSource>();
                return new AudioSourceHandle(sources[i], soundClip, clipType, i);
            }
        }
        Debug.LogError("Not enough audio sources available, consider increasing max number");
        return null;
    }

    private AudioClip[] GetClipArrayByType(AudioType clipType)
    {
        switch (clipType)
        {
            case AudioType.Ambience:
                return ambienceClips;
            case AudioType.Music:
                return musicClips;
            case AudioType.Atmosphere:
                return atmosphericClips;
            case AudioType.SFX:
                return SFXClips;
            default:
                return SFXClips;
        }
    }

    private AudioSource[] GetSourceArrayByType(AudioType clipType)
    {
        switch (clipType)
        {
            case AudioType.Ambience:
                return ambienceAudioSources;
            case AudioType.Music:
                return musicAudioSources;
            case AudioType.Atmosphere:
                return atmosphericAudioSources;
            case AudioType.SFX:
                return SFXAudioSources;
            default:
                return SFXAudioSources;
        }
    }

    public void StopSound(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Ambience:
                ambienceAudioSource.Stop();
                break;
            case AudioType.Music:
                musicAudioSource.Stop();
                break;
            case AudioType.Atmosphere:
                atmosphericAudioSource.Stop();
                break;
            case AudioType.SFX:
                SFXAudioSource.Stop();
                SFXSpareAudioSource.Stop();
                break;
        }
    }

    public void PauseSound(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Ambience:
                if (!ambienceAudioSource.isPlaying)
                    return;
                ambienceAudioSource.Pause();
                ambienceIsPaused = true;
                break;
            case AudioType.Music:
                if (!musicAudioSource.isPlaying)
                    return;
                musicAudioSource.Pause();
                musicIsPaused = true;
                break;
            case AudioType.Atmosphere:
                if (!atmosphericAudioSource.isPlaying)
                    return;
                atmosphericAudioSource.Pause();
                atmosphereIsPaused = true;
                break;
            case AudioType.SFX:
                if (SFXAudioSource.isPlaying)
                {
                    SFXAudioSource.Pause();
                    SFXIsPaused = true;
                }
                if (SFXSpareAudioSource.isPlaying)
                {
                    SFXSpareAudioSource.Pause();
                    SpareSFXIsPaused = true;
                }
                break;
        }
    }

    public void ContinueSound(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Ambience:
                if (ambienceIsPaused)
                {
                    ambienceIsPaused = false;
                    ambienceAudioSource.UnPause();
                }
                break;
            case AudioType.Music:
                if (musicIsPaused)
                {
                    musicIsPaused = false;
                    musicAudioSource.UnPause();
                }
                break;
            case AudioType.Atmosphere:
                if (atmosphereIsPaused)
                {
                    atmosphereIsPaused = false;
                    atmosphericAudioSource.UnPause();
                }
                break;
            case AudioType.SFX:
                if (SFXIsPaused)
                {
                    SFXIsPaused = false;
                    SFXAudioSource.UnPause();
                }
                if (SpareSFXIsPaused)
                {
                    SpareSFXIsPaused = false;
                    SFXSpareAudioSource.UnPause();
                }
                break;
        }
    }
}
