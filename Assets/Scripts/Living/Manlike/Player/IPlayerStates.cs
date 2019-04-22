using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PBaseState
{
    MOVE,
    ROLL,
    ATTACK1
}

public enum PSubState
{
    SHIELD,
}

public interface IPlayerStates 
{

    void BS_Move();

    void BS_Roll();

    void BS_Attack1();

    void SS_Shield();

    

}
