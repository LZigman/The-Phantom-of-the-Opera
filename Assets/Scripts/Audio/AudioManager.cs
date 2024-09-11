using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MainTheme");
    }

    public void PlayMusic(string soundName)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == soundName);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string soundName)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == soundName);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            sfxSource.clip = sound.clip;
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
