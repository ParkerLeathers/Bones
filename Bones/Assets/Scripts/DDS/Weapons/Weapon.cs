using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{


    private static readonly float DAMAGE_RANGE = 1.5f;
    private static readonly float RATE_RANGE = 0.2f;
    private static readonly float SPREAD_RANGE = 5f;
    private static readonly float SPEED_RANGE = 1.5f;

    public enum GunType {
        Sniper,
        Machine,
        Launcher,
        CountMinusOne //for some reason the Enum class doesnt exist here ?!? idk bro, this is my funny solution
    }

    public enum PropertyType {
        Damage,
        Rate,
        Spread,
        Speed
    }

    private struct Gun {
        public string path;
        public float damageOffset;
        public float damageMultiplier;
        public float rateOffset;
        public float rateMultiplier;
        public float spreadOffset;
        public float spreadMultiplier;
        public float speedOffset;
        public float speedMultiplier;
    }

    private static readonly Dictionary<GunType, Gun> guns = new()
    {
        {
            GunType.Sniper,
            new Gun() {
                path = "Sniper",
                damageOffset = 8,
                damageMultiplier = 1,
                rateOffset = 0.1f,
                rateMultiplier = 0.25f,
                spreadOffset = 0,
                spreadMultiplier = 0,
                speedOffset = 10,
                speedMultiplier = 1
            }
        },
        {
            GunType.Machine,
            new Gun() {
                path = "Machine",
                damageOffset = 2,
                damageMultiplier = 0.5f / DAMAGE_RANGE,
                rateOffset = 0.3f,
                rateMultiplier = 1,
                spreadOffset = 15,
                spreadMultiplier = 2,
                speedOffset = 6,
                speedMultiplier = 2
            }
        },
        {
            GunType.Launcher,
            new Gun() {
                path = "Launcher",
                damageOffset = 9,
                damageMultiplier = 1 / DAMAGE_RANGE,
                rateOffset = 0.1f,
                rateMultiplier = 0.25f,
                spreadOffset = 15,
                spreadMultiplier = 1,
                speedOffset = 4,
                speedMultiplier = 1 / SPEED_RANGE
            }
        }
    };


    public GunType gunType = 0;
    public float damage = 0;
    public float rate = 0;
    public float spread = 0;
    public float speed = 0;

    public Weapon(GunType gunType, float damage, float rate, float spread, float speed) {
        this.gunType = gunType;
        this.damage = damage;
        this.rate = rate;
        this.spread = spread;
        this.speed = speed;
    }

    public Weapon() : this(0, 0f, 0f, 0f, 0f) { }


    public void SetValues(GunType gunType, float damage, float rate, float spread, float speed) {
        this.gunType = gunType;
        this.damage = damage;
        this.rate = rate;
        this.spread = spread;
        this.speed = speed;
    }

    public string getPath() {
        return guns[gunType].path;
    }

    private static float Modify(float value, float offset, float multiplier) {
        float outf = offset + value * multiplier;
        if (outf < 0)
            return 0f;
        return outf;
    }

    private static float ApplyModifier(float value, GunType gunType, PropertyType prop) {
        Gun gun = guns[gunType];
        switch (prop) {
            case PropertyType.Damage:
                return Modify(value, gun.damageOffset, gun.damageMultiplier);
            case PropertyType.Rate:
                float outf = Modify(value, gun.rateOffset, gun.rateMultiplier);
                if (outf < 0.01f)
                    return 0.01f;
                return outf;
            case PropertyType.Spread:
                return Modify(value, gun.spreadOffset, gun.spreadMultiplier);
            case PropertyType.Speed:
                return Modify(value, gun.speedOffset, gun.speedMultiplier);
        }
        return -1;
    }

    public void CreateGun(GunType gun) {
        SetValues(gun,
            ApplyModifier(Random.Range(-DAMAGE_RANGE, DAMAGE_RANGE), gun, PropertyType.Damage),
            ApplyModifier(Random.Range(-RATE_RANGE, RATE_RANGE), gun, PropertyType.Rate),
            ApplyModifier(Random.Range(-SPREAD_RANGE, SPREAD_RANGE), gun, PropertyType.Spread),
            ApplyModifier(Random.Range(-SPEED_RANGE, SPEED_RANGE), gun, PropertyType.Speed));
    }

    public void CreateGun() {
        CreateGun((GunType)Random.Range(0, (int)GunType.CountMinusOne));
    }
}
