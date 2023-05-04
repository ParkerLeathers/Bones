using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{

    //i am a goat for coming up with this godly cutscene organization
    //and an egomaniac as well i guess

    public enum Speaker {
        Bones,
        SkullKing
    }
    [Serializable]
    public struct VoiceLine {
        public AudioClip clip;
        public string text;
        public Speaker speaker;
    }
    [SerializeField]
    private VoiceLine[] lines;
    [SerializeField]
    private Text[] speakersText;
    [SerializeField]
    private GameObject voice;

    private AudioScript audioScript;
    private ChatHandler[] speakersScript;
    private Queue<VoiceLine> lineQueue = new Queue<VoiceLine>();

    public bool done = false;

    void Start()
    {
        foreach (VoiceLine i in lines)
            lineQueue.Enqueue(i);
        speakersScript = new ChatHandler[speakersText.Length];
        for (int i = 0; i < speakersText.Length; i++)
            speakersScript[i] = speakersText[i].GetComponent<ChatHandler>();
        audioScript = voice.GetComponent<AudioScript>();
        StartCutscene();
    }


    private void StartCutscene() {
        StartCoroutine(CutsceneRoutine());

        IEnumerator CutsceneRoutine() {
            while (lineQueue.Count > 0) {
                while (!InputManager.GetKey(InputManager.InputName.Button1))
                    yield return null;


                VoiceLine line = lineQueue.Dequeue();
                speakersScript[(int) line.speaker].StartText(line.text);
                if (line.clip != null)
                    audioScript.PlaySound(line.clip);


                while (InputManager.GetKey(InputManager.InputName.Button1))
                    yield return null;
            }
            if (lineQueue.Count == 0)
                done = true;
        }
    }
    void Update()
    {
        
    }
}
