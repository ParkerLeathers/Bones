using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{

    [Header("Components")]
    private SpriteRenderer sr;

    [Header("Game Objects")]
    [SerializeField] private GameObject bulletPrefab;


    private static readonly float DRAW_DISTANCE = 0.05f;

    private enum State {
        Ready,
        ReloadOut,
        ReloadIn
    }

    private Vector2 relPos;
    private float dist;

    private State state = State.Ready;

    private Weapon weapon;

    protected Vector2 target;
    protected bool fire;
    protected bool ally;

    private void UpdateTexture() {
        sr.sprite = Resources.Load<Sprite>(weapon.getPath());
    }
    
    private void Fire() {
        GameObject bullet;
        Destroy(
            bullet = Instantiate(bulletPrefab, 
            transform.position, 
            Quaternion.Euler(0, 0, Random.Range(-weapon.spread, weapon.spread) + transform.rotation.eulerAngles.z)), 4); //todo val here lol
        BulletHandler bulletHandler = bullet.GetComponent<BulletHandler>();
        bulletHandler.speed = weapon.speed;
        bulletHandler.damage = weapon.damage;
        bulletHandler.ally = ally;
        state = State.ReloadOut;
    }

    private void ReloadOut() {
        dist += weapon.rate * Time.deltaTime;
        transform.localPosition = relPos - dist * (Vector2) (transform.rotation * Vector2.right); //lord forgive me for this line of code

        if (dist > DRAW_DISTANCE)
            state = State.ReloadIn;
    }
    private void ReloadIn() {
        dist -= weapon.rate * Time.deltaTime;
        transform.localPosition = relPos - dist * (Vector2)(transform.rotation * Vector2.right); //lord forgive me for this line of code

        if (dist < 0) {
            dist = 0;
            transform.localPosition = relPos;
            state = State.Ready;
        }

    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
        UpdateTexture();
    }

    public Weapon GetWeapon() {
        return weapon;
    }

    private void CreateGun() {
        weapon.CreateGun();
        UpdateTexture();
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        relPos = transform.localPosition;
        weapon = new();
        CreateGun();
    }

    protected abstract void UpdateValues();

    void Update()
    {
        UpdateValues();
        Vector2 objToTarget = target - (Vector2)transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(objToTarget.y, objToTarget.x) * Mathf.Rad2Deg);
        

        switch (state) {
            case State.Ready:
                if (fire)
                    Fire();
                break;
            case State.ReloadOut:
                ReloadOut();
                break;
            case State.ReloadIn:
                ReloadIn();
                break;
        }
    }
}
