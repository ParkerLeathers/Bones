using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : WeaponHandler {
    protected override void UpdateValues() {
        ally = true;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fire = Input.GetMouseButton(0);
    }
}
