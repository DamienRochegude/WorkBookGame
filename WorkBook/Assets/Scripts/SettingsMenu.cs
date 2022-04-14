using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private AudioMixer audioMixer;
    
    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions.Select(Resolution => new Resolution { width = Resolution.width, height = Resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetVolumeSound(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
