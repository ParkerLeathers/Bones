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
    [SerializeField] private Dropdown button1;
    [SerializeField] private Dropdown button2;
    [SerializeField] private Dropdown button3;


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
        int c = 0;
        foreach (Resolution r in resolutions) {
            if (Mathf.Abs((float) r.width / r.height - (16f / 9)) < 0.001) {//sorry its just easier for me to only allow 16:9 :(
                c++;
                resolution.options.Add(new Dropdown.OptionData(r.width + "x" + r.height));

                if(r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height) {
                    resolution.value = c;
                    resolutionHit = true;
                }
            }
        }
        if (!resolutionHit)
            resolution.value = resolutions.Length-1;

        /*
         * inputs
        */


        string[] keyNames = System.Enum.GetNames(typeof(KeyCode));
        for (int i = 0; i < keyNames.Length; i++) {
            button1.options.Add(new Dropdown.OptionData(keyNames[i]));
            button2.options.Add(new Dropdown.OptionData(keyNames[i]));
            button3.options.Add(new Dropdown.OptionData(keyNames[i]));

            switch (button1.options[i].text) {
                case "Z":
                    button1.value = i;
                    break;
                case "X":
                    button2.value = i;
                    break;
                case "C":
                    button3.value = i;
                    break;
            }
                
        }
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

    public void SetInput(InputManager.InputName key) {
        InputManager.Set(key, (KeyCode) System.Enum.GetValues(typeof(KeyCode)).GetValue(button1.value));
    }

    public void SetInputButton1() {
        SetInput(InputManager.InputName.Button1);
    }

    public void SetInputButton2() {
        SetInput(InputManager.InputName.Button2);
    }

    public void SetInputButton3() {
        SetInput(InputManager.InputName.Button3);
    }
}
