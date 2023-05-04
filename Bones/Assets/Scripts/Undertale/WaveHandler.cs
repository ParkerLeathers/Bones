using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public enum AttackEnum {
        Uncrush,
        CrushMono,
        CrushHold,
        Bombard,
        Rocketerate
    }

    [Serializable]
    private struct TimeStamp {
        public AttackEnum attack;
        public float seconds; //relative
    }

    [Serializable]
    private struct Wave {
        public TimeStamp[] stamps;
        public Vector2 pos;
    }

    [Header("Game Objects")]
    [SerializeField]
    private GameObject crusherator;
    [SerializeField]
    private GameObject bomberator;
    [SerializeField]
    private GameObject rocketerator;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private Wave[] waves;

    private CrusheratorScript crusheratorScript;
    private BomberatorScript bomberatorScript;
    private RocketeratorScript rocketeratorScript;
    private BossHandler bossHandler;

    private bool ready = true;

    private Queue<Wave> waveQueue = new Queue<Wave>();
    void Start()
    {
        foreach (Wave i in waves)
            waveQueue.Enqueue(i);

        crusheratorScript = crusherator.GetComponent<CrusheratorScript>();
        bomberatorScript = bomberator.GetComponent<BomberatorScript>();
        rocketeratorScript = rocketerator.GetComponent<RocketeratorScript>();
        bossHandler = boss.GetComponent<BossHandler>();
    }

    public bool Next() {
        ready = true;
        if (waveQueue.Count == 0)
            return false;
        return true;
    }

    public void StartWaves() {
        StartCoroutine(WaveRoutine());

        IEnumerator WaveRoutine() {
            while (waveQueue.Count > 0) {
                while (!ready)
                    yield return null;
                ready = false;

                Wave wave = waveQueue.Dequeue();

                bossHandler.MoveTo(wave.pos);

                foreach (TimeStamp i in wave.stamps) {
                    yield return new WaitForSeconds(i.seconds);
                    CallAttack(i.attack);
                }
            }
        }
    }

    void CallAttack(AttackEnum attack) {
        switch (attack) {
            case AttackEnum.Uncrush:
                crusheratorScript.Uncrush();
                break;
            case AttackEnum.CrushMono:
                crusheratorScript.MonoCrush();
                break;
            case AttackEnum.CrushHold:
                crusheratorScript.Crush();
                break;
            case AttackEnum.Bombard:
                bomberatorScript.Bombard(2f); //todo replace with something idk
                break;
            case AttackEnum.Rocketerate:
                rocketeratorScript.Rocketerate();
                break;
        }
    }

    void Update()
    {
        
    }
}
