using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDSBossHandler : MonoBehaviour
{

    [Header("GameObjects")]
    [SerializeField] private GameObject point;
    [SerializeField] private Text text;

    private GameObject previousPoint;

    [Header("Components")]
    private BossHandler bossHandler;

    [Header("Stats")]
    [SerializeField] private float health = 100;

    void Awake() 
    {
        bossHandler = GetComponent<BossHandler>();
        previousPoint = point;
    }

    
    void Update()
    {
        Movement();
    }

    private void Movement() {
        if (bossHandler.done) {
            DDSWaypoint pointScript = point.GetComponent<DDSWaypoint>();
            GameObject[] wayPointObjs = pointScript.wayPointObjs;
            if (wayPointObjs.Length == 1) {
                previousPoint = point;
                point = wayPointObjs[0];
            } else {
                GameObject[] potentialPoints = new GameObject[wayPointObjs.Length];
                int potLoc = 0; // now this is spatial partition
                foreach (GameObject i in wayPointObjs) // according to some random guy online this compiles in such a way that it still practices spatial partitioning. cool
                    if (!i.Equals(previousPoint)) {
                        potentialPoints[potLoc] = i;
                        potLoc += 1;
                    }
                previousPoint = point;
                point = potentialPoints[Random.Range(0, potLoc)];
            }
            bossHandler.MoveTo(point.transform.position);
        }
    }

    private void Death() {
        //todo IMPLEMENT
        Debug.Log("big money ukilled him");
    }

    private void UpdateHealth() {
        text.text = ""+(int) health;
        if (health < 1) //i do less than 1 so that way the display never says "0" despite having like 0.5
            Death();
    }

    private void TakeDamage(float dmg) {
        health -= dmg;
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        switch (other.tag) {
            case "Projectile": //death
                BulletHandler bh = other.GetComponent<BulletHandler>();
                if (bh != null && bh.ally) {
                    TakeDamage(bh.damage);
                    Destroy(other.gameObject); //heads up make sure u dont destroy the skull king before this can run
                }
                break;
            default:
                break;
        }
    }
}
