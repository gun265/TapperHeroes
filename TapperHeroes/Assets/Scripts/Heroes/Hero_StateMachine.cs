using UnityEngine;
using System.Collections;

public class Hero_StateMachine<T>
{
    T Owner;
    Hero_FSM<T> CurrentState = null;

    // 초기상태설정
    public void Init(T _Owner, Hero_FSM<T> _InitialState)
    {
        Owner = _Owner;
        ChangeState(_InitialState);
    }

    // 상태변경
    public bool ChangeState(Hero_FSM<T> _NewState)
    {
        if( _NewState == CurrentState)
        {
            return false;
        }

        if( CurrentState != null)
        {
            CurrentState.Exit(Owner);
        }

        CurrentState = _NewState;

        if( CurrentState != null)
        {
            CurrentState.Enter(Owner);
            return true;
        }

        return false;
    }


    // 업데이트
    public bool Update(float Deltatime)
    {
        if( CurrentState != null)
        {
            CurrentState.Update(Owner, Deltatime);
            return true;
        }
        return false;       
    }
}
