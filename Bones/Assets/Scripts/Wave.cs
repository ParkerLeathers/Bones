using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{

    protected enum AttackEnum {
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5
    }
    public struct Stamp {
        int seconds;
        AttackEnum attack;
    }
    [SerializeReference]
    Stamp[] stamps;

    void Start() {

    }

    protected void CallAttack(AttackEnum attack) {
        switch(attack){
            case AttackEnum.Attack1:
                Attack1();
                break;
            case AttackEnum.Attack2:
                Attack2();
                break;
            case AttackEnum.Attack3:
                Attack3();
                break;
            case AttackEnum.Attack4:
                Attack4();
                break;
            case AttackEnum.Attack5:
                Attack5();
                break;
        }

    }

    abstract protected void Attack1();
    abstract protected void Attack2();
    abstract protected void Attack3();
    abstract protected void Attack4();
    abstract protected void Attack5();


}
