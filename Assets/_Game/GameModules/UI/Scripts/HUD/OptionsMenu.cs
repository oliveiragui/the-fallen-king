using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown ResolutionDropdown;


    private void Start()
    {
        int CurrentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string Option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(Option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolumeMaster(float volume)
    {
        AudioMixer.SetFloat("MasterVol", volume);
    }

    public void SetVolumeBackgroundMusic(float volume)
    {
        AudioMixer.SetFloat("BgMusicVol", volume);
    }

    public void SetVolumeFX(float volume)
    {
        AudioMixer.SetFloat("FXVol", volume);
    }

    public void SetVolumeAmbience(float volume)
    {
        AudioMixer.SetFloat("AmbienceVol", volume);
    }
    public void SetVolumeInterface(float volume)
    {
        AudioMixer.SetFloat("InterfaceVol", volume);
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}