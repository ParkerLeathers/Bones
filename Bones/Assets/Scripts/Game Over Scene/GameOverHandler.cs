using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{

    [Header("Game Objects")]
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject bonesLeft;
    [SerializeField]
    GameObject bonesRight;

    [SerializeField]
    string[] options;

    ChatHandler textScript;
    Rigidbody2D bonesLeftRb;
    Rigidbody2D bonesRightRb;

    void Awake() {
        textScript = text.GetComponent<ChatHandler>();
        bonesLeftRb = bonesLeft.GetComponent<Rigidbody2D>();
        bonesRightRb = bonesRight.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (!UniversalData.fakeDeath)
            textScript.StartText(options[Random.Range(0, options.Length)]);
        else
            textScript.StartText("You know you could move the whole time right?");
        UniversalData.fakeDeath = false;
        bonesLeftRb.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
        bonesRightRb.AddForce(new Vector2(2f, 1f), ForceMode2D.Impulse);
        bonesLeftRb.AddTorque(60f);
        bonesRightRb.AddTorque(-60f);

        StartCoroutine(GameOverRoutine());

        IEnumerator GameOverRoutine() {
            yield return new WaitForSeconds(4f); //wait 4 seconds
            while (!Input.GetKeyDown(KeyCode.Z)) //wait for Z press
                yield return null;
            SceneManager.LoadScene("Undertale");
        }
    }


    void Update()
    {
        
    }
}
