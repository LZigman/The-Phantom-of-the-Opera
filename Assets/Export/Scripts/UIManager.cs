using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject musicToggle, sfxToggle;
    public Slider musicSlider, sfxSlider;
    [SerializeField] private Sprite musicOn, musicOff, sfxOn, sfxOff;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        if (AudioManager.Instance.musicSource.mute)
        {
            musicToggle.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            musicToggle.GetComponent<Image>().sprite = musicOn;
        }
    }

    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
        if (AudioManager.Instance.sfxSource.mute)
        {
            sfxToggle.GetComponent<Image>().sprite = sfxOff;
        }
        else
        {
            sfxToggle.GetComponent<Image>().sprite = sfxOn;
        }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    
    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(sfxSlider.value);
    }
}
