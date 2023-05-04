using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip ac) {
        audioSource.Stop();
        audioSource.PlayOneShot(ac);
    }

    public void Play(AudioClip ac) {
        audioSource.PlayOneShot(ac);
    }
    
    public void Play() {
        audioSource.Play();
    }

    void Update()
    {
        
    }
}
