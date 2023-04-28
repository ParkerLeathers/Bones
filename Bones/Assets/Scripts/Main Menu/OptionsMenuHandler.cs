using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuHandler : MonoBehaviour
{
    [SerializeField] private Canvas optionsMenu;
    [SerializeField] private Slider master;
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider music;
    [SerializeField] private Slider voice;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Dropdown resolution;
    [SerializeField] private Toggle fullscreen;


    private Resolution[] resolutions;
    private void Start() {

        /*
         * audio
        */
        if (PlayerPrefs.GetInt("set first time volume") == 0) {
            PlayerPrefs.SetInt("set first time volume", 1);
            master.value = 0.5f;
            sfx.value = 0.5f;
            music.value = 0.5f;
            voice.value = 0.5f;
        }

        master.value = PlayerPrefs.GetFloat("Master");
        sfx.value = PlayerPrefs.GetFloat("SFX");
        music.value = PlayerPrefs.GetFloat("Music");
        voice.value = PlayerPrefs.GetFloat("Voice");

        /*
         * resolution
        */

        resolutions = Screen.resolutions;
        bool resolutionHit = false;
        for (int i = 0; i < resolutions.Length; i++) {
            if (Mathf.Abs((float)resolutions[i].width / resolutions[i].height - (16f / 9)) < 0.001) {//sorry its just easier for me to only allow 16:9 :(
                resolution.options.Add(new Dropdown.OptionData(resolutions[i].width + "x" + resolutions[i].height));

                if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                    resolution.value = i;
                    resolutionHit = true;
                }
            }
        }
        if (!resolutionHit)
            resolution.value = resolutions.Length-1;
    }

    public void Open() {
        optionsMenu.enabled = true;
    }

    public void Close() {
        optionsMenu.enabled = false;
    }

    public void SetMasterVolume() {
        SetVolume("Master", master.value);
    }
    public void SetSFXVolume() {
        SetVolume("SFX", sfx.value);
    }
    public void SetMusicVolume() {
        SetVolume("Music", music.value);
    }
    public void SetVoiceVolume() {
        SetVolume("Voice", voice.value);
    }

    void SetVolume(string name, float value) {
        float volume = Mathf.Log10(value) * 20;
        if (value == 0)
            volume = -80;

        mixer.SetFloat(name, volume);
        PlayerPrefs.SetFloat(name, value);
    }

    public void SetResolution() {
        Screen.SetResolution(resolutions[resolution.value].width, resolutions[resolution.value].height, fullscreen.isOn);
    }
}
