using UnityEngine;
using System.Collections;

public class Hero_Dead : Hero_FSM<Hero_Behavior>
{
    public override void Enter(Hero_Behavior _Owner)
    {
        
    }
    
    public override void Update(Hero_Behavior _Owner, float _Deltatime)
    {
        if((_Owner.current_regentime += Time.deltaTime) >= _Owner.RegenTime)
        {
            _Owner.current_regentime = 0;
            _Owner.StateMachine.ChangeState(new Hero_Live());
        }
    }

    public override void Exit(Hero_Behavior _Owner)
    {

    }
}
