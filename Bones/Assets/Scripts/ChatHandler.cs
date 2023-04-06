using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour
{
    [Header("Text")]
    private Queue<char> textQueue;
    private Text textCmp;

    void Start()
    {
        textQueue = new Queue<char>();
        textCmp = GetComponent<Text>();
    }

    public void SetText(string text) {
        textQueue.Clear();
        StopCoroutine("TextRoutine");
        textCmp.text = "";
        foreach (char i in text)
            textQueue.Enqueue(i);
    }

    private void NextChar() {
        if (textQueue.Count > 0)
            textCmp.text += textQueue.Dequeue();
    }

    public void StartText() {
        StartCoroutine(TextRoutine());

        IEnumerator TextRoutine() {
            while (true) {
                yield return new WaitForSeconds(0.05f); //wait 0.05 seconds
                this.NextChar();
            }
        }
    }
    
    public void StartText(string text) {
        SetText(text);
        StartText();
    }

    void Update()
    {
        
    }
}
