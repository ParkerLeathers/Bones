using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{


    [Header("Components")]
    private SpriteRenderer sr;

    [SerializeField] private Weapon.GunType gunType;
    private Weapon weapon;

    private bool pickupable = true;

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        weapon = new();
        weapon.CreateGun(gunType);
    }

    
    void Update()
    {
        
    }


    private void UpdateTexture() {
        sr.sprite = Resources.Load<Sprite>(weapon.getPath());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null || pickupable == false)
            return;

        if(other.CompareTag("Player") || other.CompareTag("Boss")) {
            Debug.Log("enter");
            pickupable = false;
            WeaponHandler weaponHandler = other.GetComponentInChildren<WeaponHandler>();
            Weapon swap = weapon;
            weapon = weaponHandler.GetWeapon();
            weaponHandler.SetWeapon(swap);
            gunType = weapon.gunType;
            UpdateTexture();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other == null || other.gameObject == null || pickupable == true)
            return;

        if (other.CompareTag("Player") || other.CompareTag("Boss")) {
            Debug.Log("exit");
            pickupable = true;
        }
    }
}
