using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    protected AudioManager()
    {
    } // guarantee this will be always a singleton only - can't use the constructor!

    ////////////////////////////////////////////////////AUDIO STRINGS////////////////////////////////////////////////////////////////////////////////
    public enum AudioType { Music, Ambience, Atmosphere, SFX}

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
    private bool SpareSFXIsPaused = false;

    private AudioSource ambienceAudioSource;
    private AudioSource musicAudioSource;
    private AudioSource atmosphericAudioSource;
    private AudioSource SFXAudioSource;
    private AudioSource SFXSpareAudioSource;

    private AudioClip[] ambienceClips;
    private AudioClip[] musicClips;
    private AudioClip[] atmosphericClips;
    private AudioClip[] SFXClips;

    // Start is called before the first frame update
    void Awake()
    {
        ambienceAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        atmosphericAudioSource = gameObject.AddComponent<AudioSource>();
        SFXAudioSource = gameObject.AddComponent<AudioSource>();
        SFXSpareAudioSource = gameObject.AddComponent<AudioSource>();
        ambienceClips = Resources.LoadAll<AudioClip>("Audio/Ambience");
        musicClips = Resources.LoadAll<AudioClip>("Audio/Music");
        atmosphericClips = Resources.LoadAll<AudioClip>("Audio/Atmosphere");
        SFXClips = Resources.LoadAll<AudioClip>("Audio/SFX");
    }

    private AudioClip GetClip(AudioClip[] list, string clipName)
    {
        AudioClip clip = null;

        if (list != null)
        {
            for (int index = 0; index < list.Length; index++)
            {
                if (list[index].name == clipName)
                {
                    clip = list[index];
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

    public void PlaySound(string clipName, AudioType clipType)
    {
        AudioClip[] sounds;
        AudioSource source;
        switch(clipType)
        {
            case AudioType.Ambience:
                sounds = ambienceClips;
                source = ambienceAudioSource;
                break;
            case AudioType.Music:
                sounds = musicClips;
                source = musicAudioSource;
                break;
            case AudioType.Atmosphere:
                sounds = atmosphericClips;
                source = atmosphericAudioSource;
                break;
            case AudioType.SFX:
                sounds = SFXClips;
                if (!SFXAudioSource.isPlaying)
                {
                    source = SFXAudioSource;
                }
                else
                {
                    source = SFXSpareAudioSource;
                }
                break;
            default:
                sounds = SFXClips;
                source = SFXAudioSource;
                break;
        }

        AudioClip soundClip = GetClip(sounds, clipName);

        if (soundClip != null)
        {
            source.PlayOneShot(soundClip, 0.5f);
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
