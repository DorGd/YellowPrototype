using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public interface IAudioSourceHandler
    {
        public void SetClip(AudioClip clip);
        public void SetClip(String clipName);
        public void Play(float startVolume = 1.0f, bool loop = false);
        public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f);
        public void PlayOneShot(String clipName, float volumeScale = 1.0f);
        public void SetVolume(float volume);
        public void SetLoop(bool loop);
        public void Pause();
        public void UnPause();
        public void Stop();
        public void ReleaseHandler();
    }

    private class AudioSourceHandle : IAudioSourceHandler
    {
        public AudioSource source;
        private bool isPaused = false;
        public bool IsPaused
        {
            get
            {
                return isPaused;
            }
        }
        private bool isAllocated = false;
        public bool IsAllocated
        {
            get
            {
                return isAllocated;
            }
        }
        public AudioSourceHandle(AudioSource source)
        {
            this.source = source;
        }

        public void Play(float startVolume = 1.0f, bool loop = false)
        {
            source.volume = startVolume * Instance.globalVolume;
            source.loop = loop;
            source.Play();
            if (Instance.IsAudioPaused)
            {
                source.Pause();
            }
        }

        public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
        {
            if (Instance.IsAudioPaused)
                return;
            source.PlayOneShot(clip, volumeScale * Instance.globalVolume);
        }

        public void PlayOneShot(string clipName, float volumeScale = 1)
        {
            if (Instance.IsAudioPaused)
                return;
            source.PlayOneShot(Instance.GetClipByName(clipName), volumeScale * Instance.globalVolume);
        }

        public void Stop()
        {
            source.Stop();
            isPaused = false;
        }

        public void Pause()
        {
            if (source.isPlaying && !isPaused)
            {
                isPaused = true;
                source.Pause();
            }
        }

        public void UnPause()
        {
            if (isPaused && !Instance.IsAudioPaused)
            {
                isPaused = false;
                source.UnPause();
            }
        }

        public void SetClip(AudioClip clip)
        {
            source.clip = clip;
        }

        public void SetClip(string clipName)
        {
            source.clip = Instance.GetClipByName(clipName);
        }

        public void SetLoop(bool loop)
        {
            source.loop = loop;
        }

        public void SetVolume(float volume)
        {
            if (source.isPlaying || isPaused)
            {
                source.volume = Mathf.Clamp(volume * Instance.globalVolume, 0.0f, 1.0f);
            }
        }

        public void ReleaseHandler()
        {
            isAllocated = false;
        }

        public void AllocateHandle()
        {
            isAllocated = true;
        }
    }

    protected AudioManager()
    {
    } // guarantee this will be always a singleton only - can't use the constructor!

    private static int MAX_SOURCES = 32;

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
    public static string SFX_conveyor = "79573__razzvio__rolling-metal-conveyor-belt";
    public static string SFX_liftoff = "520557__aj-heels__wehaveliftoff";


    ////////////////////////////////////////////////////AUDIO STRINGS END////////////////////////////////////////////////////////////////////////////////

    public float globalVolume = 1.0f;

    private bool isAudioPaused = false;

    public bool IsAudioPaused
    {
        get
        {
            return isAudioPaused;
        }
    }

    private AudioSourceHandle[] audioSourceHandlesPool = new AudioSourceHandle[MAX_SOURCES];

    private AudioClip[] clips;
    private AudioSource oneShotSource;

    // Start is called before the first frame update
    void Awake()
    {
        clips = Resources.LoadAll<AudioClip>("Audio");
        oneShotSource = Camera.main.gameObject.GetComponent<AudioSource>();
    }

    public AudioClip GetClipByName(string clipName)
    {
        if (clips != null)
        {
            for (int index = 0; index < clips.Length; index++)
            {
                if (clips[index].name == clipName)
                {
                    return clips[index];
                }
            }
        }
        Debug.LogError("Couldn't find clip name");
        return null;
    }

    public IAudioSourceHandler GetAvailableAudioSourceHandle()
    {
        for (int i = 0; i < MAX_SOURCES; i++)
        {
            if (audioSourceHandlesPool[i] == null)
            {
                AudioSource source = Camera.main.gameObject.AddComponent<AudioSource>();
                AudioSourceHandle handle = new AudioSourceHandle(source);
                audioSourceHandlesPool[i] = handle;
                handle.AllocateHandle();
                return handle;
            }
            else if (!audioSourceHandlesPool[i].IsAllocated)
            {
                audioSourceHandlesPool[i].AllocateHandle();
                return audioSourceHandlesPool[i];
            }
        }
        Debug.LogError("Not enough audio sources available, consider increasing max number");
        return null;
    }

    public void StopAllSound()
    {
        foreach(AudioSourceHandle handle in audioSourceHandlesPool)
        {
            if(handle != null)
            {
                handle.Stop();
            }
        }
    }

    public void PauseAllSound()
    {
        isAudioPaused = true;
        foreach (AudioSourceHandle handle in audioSourceHandlesPool)
        {
            if (handle != null && !handle.IsPaused)
            {
                handle.source.Pause();
            }
        }
    }

    public void UnpauseAllSound()
    {
        isAudioPaused = false;
        foreach (AudioSourceHandle handle in audioSourceHandlesPool)
        {
            if (handle != null && !handle.IsPaused)
            {
                handle.source.UnPause();
            }
        }
    }

    public void PlayOneShot(string clipName, float volumeScale = 1.0f)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip == null)
            return;
        if (oneShotSource == null) 
            oneShotSource = Camera.main.gameObject.GetComponent<AudioSource>();
        oneShotSource.PlayOneShot(clip, volumeScale * globalVolume);
    }

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
    {
        if (oneShotSource == null) 
            oneShotSource = Camera.main.gameObject.GetComponent<AudioSource>();
        oneShotSource.PlayOneShot(clip, volumeScale * globalVolume);
    }
}
