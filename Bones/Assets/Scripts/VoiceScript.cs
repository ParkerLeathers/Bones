using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceScript : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip ac) {
        audioSource.PlayOneShot(ac);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
