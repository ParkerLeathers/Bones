using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponHandler : WeaponHandler {

    [Header("GameObjects")]
    [SerializeField] private GameObject player;

    [Header("Stats")]
    [SerializeField] private float MAX_DISTANCE;

    protected override void UpdateValues() {
        ally = false;
        target = player.transform.position;
        fire = Vector2.Distance((Vector2) transform.position, (Vector2) player.transform.position) < MAX_DISTANCE;
        //todo potentially raycast to check for a wall in between player and boss
    }
}
