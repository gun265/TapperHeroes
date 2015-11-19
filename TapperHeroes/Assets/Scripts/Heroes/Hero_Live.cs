using UnityEngine;
using System.Collections;

public class Hero_Live : Hero_FSM<Hero_Behavior>
{
    public override void Enter(Hero_Behavior _Owner)
    {
        _Owner.PlayIdleAnimation();
    }

    public void Attack(Hero_Behavior _Owner)
    {
        GameObject Temp = NGUITools.AddChild(GameMgr.GetInstance().Effect, _Owner.AttackParticle);
        Camera UICam = NGUITools.FindCameraForLayer(Temp.layer);
        Vector3 pos = Camera.main.WorldToScreenPoint(_Owner.transform.position);
        Temp.transform.position = UICam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0));

        _Owner.Ani.CrossFade("Lumbering",0.2f);

        GameMgr.GetInstance().AddAttackList(_Owner.gameObject, _Owner.Damage, _Owner.AttackSpeed, Temp);
    }

    public override void Update(Hero_Behavior _Owner, float _Deltatime)
    {
        if (!_Owner.IsDead)
        {
            if ((_Owner.attacktime += _Deltatime) >= _Owner.AttackSpeed)
            {
                _Owner.attacktime = 0;
                Attack(_Owner);
            }
        }
        else
        {
            _Owner.StateMachine.ChangeState(new Hero_Dead());
        }
    }

    public override void Exit(Hero_Behavior _Owner)
    {
        _Owner.Init();
    }
}
