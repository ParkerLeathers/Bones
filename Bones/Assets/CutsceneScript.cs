using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{

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

    private VoiceScript vs;
    private ChatHandler[] speakersScript;
    private Queue<VoiceLine> lineQueue = new Queue<VoiceLine>();
    void Start()
    {
        foreach (VoiceLine i in lines)
            lineQueue.Enqueue(i);
        speakersScript = new ChatHandler[speakersText.Length];
        for (int i = 0; i < speakersText.Length; i++)
            speakersScript[i] = speakersText[i].GetComponent<ChatHandler>();
        vs = voice.GetComponent<VoiceScript>();
        StartCutscene();
    }


    private void StartCutscene() {
        StartCoroutine(CutsceneRoutine());

        IEnumerator CutsceneRoutine() {
            while (lineQueue.Count > 0) {
                while (!Input.GetKeyDown(KeyCode.Z))
                    yield return null;
                
                VoiceLine line = lineQueue.Dequeue();
                speakersScript[(int) line.speaker].StartText(line.text);
                if (line.clip != null)
                    vs.PlaySound(line.clip);

                while (Input.GetKeyDown(KeyCode.Z))
                    yield return null;
            }
        }
    }
    void Update()
    {
        
    }
}
