using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;

public class BlackboardMenu : MonoBehaviour
{
    // Variables privées modifiables depuis l'éditeur
    [SerializeField]
    private GameObject firstMainSelected, firstSettingsSelected;
    [SerializeField]
    private GameObject soundSelect, soundSlider, musicSelect, musicSlider;
    [SerializeField]
    private GameObject main_pannel;
    [SerializeField]
    private GameObject settings_pannel;
    [SerializeField]
    private TMP_Dropdown resolution_dropdown;
    [SerializeField]
    private Image fullscreen_visual;
    [SerializeField]
    private Sprite fullscreen_on_sprite;
    [SerializeField]
    private Sprite fullscreen_off_sprite;
    [SerializeField]
    private EventSystem EVRef;
    [SerializeField]
    private AudioMixer audio_mixer;

    // Variables privées
    private bool onMenu = false;
    private bool fullscreen_state = true;
    private Resolution[] resolutions;
    private float sound_volume, music_volume;

    void Start()
    {
        // Ajout de toutes les résolutions dispo au menu dépliant
        resolutions = Screen.resolutions;
        SetMenuNavState(false);

        resolution_dropdown.options.Clear();
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolution_dropdown.options.Add(new TMP_Dropdown.OptionData(ResToString(resolutions[i])));

            resolution_dropdown.value = i;
        }
    }

    private void Update()
    {
        if (!Camera.main) return;
        if (Camera.main.GetComponent<CameraController>().IsInPause && !onMenu)
            SetMenuNavState(true);
        else if(!Camera.main.GetComponent<CameraController>().IsInPause && onMenu)
            SetMenuNavState(false);
    }

    void SetMenuNavState(bool state)
    {
        EVRef.gameObject.SetActive(state);
        AccessMainPannel();
        onMenu = state;
    }

    string ResToString(Resolution res)
    {
        return res.width + " * " + res.height;
    }

    public void ResumeGame()
    {
        Camera.main.GetComponent<CameraController>().setPauseState(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleFullscreen()
    {
        fullscreen_state = !fullscreen_state;
        Screen.fullScreen = fullscreen_state;
        if (fullscreen_state)
            fullscreen_visual.sprite = fullscreen_on_sprite;
        else
            fullscreen_visual.sprite = fullscreen_off_sprite;
    }

    public void AccessSettingsPannel()
    {
        main_pannel.SetActive(false);
        settings_pannel.SetActive(true);
        EVRef.SetSelectedGameObject(firstSettingsSelected);
    }
    public void AccessMainPannel()
    {
        settings_pannel.SetActive(false);
        main_pannel.SetActive(true);
        EVRef.SetSelectedGameObject(firstMainSelected);
    }
    public void SetSoundVolume()
    {
        sound_volume = soundSlider.GetComponent<Slider>().value;
        audio_mixer.SetFloat("Master", sound_volume);
    }

    public void SetResolution()
    {
        Screen.SetResolution(resolutions[resolution_dropdown.value].width, resolutions[resolution_dropdown.value].height, fullscreen_state);
    }

    public void SetMusicVolume()
    {
        music_volume = musicSlider.GetComponent<Slider>().value;
        audio_mixer.SetFloat("Master", music_volume);
    }
}
