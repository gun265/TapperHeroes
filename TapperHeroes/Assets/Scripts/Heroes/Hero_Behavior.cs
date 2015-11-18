using UnityEngine;
using System.Collections;
using System;

public class Hero_Behavior : HeroFoundation
{
    public uint Ori_Damage = 1;
    public float Ori_AttackSpeed = 1.0f;
    public string Name = "";
    public GameObject AttackParticle = null;

    public override void Init()
    {
        CurrentAttackSpeed = Ori_AttackSpeed;
        CurrentDamage = Ori_Damage;
    }

    public override void Awake()
    {
        Init();
    }

    public override void Update()
    {
        if( !IsDead)
        {
            if ((AttackTime += Time.deltaTime) >= CurrentAttackSpeed)
            {
                AttackTime = 0;
                Attack();
            }
        }
        else
        {
            if ((CurrentRegenTime += Time.deltaTime) >= RegenTime)
            {
                CurrentRegenTime = 0;
                Regeneration();
            }
        }
    }

    public override void Attack()
    {
        GameObject Temp = NGUITools.AddChild(GameMgr.GetInstance().Effect, AttackParticle);
        Camera UICam = NGUITools.FindCameraForLayer(Temp.layer);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Temp.transform.position = UICam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0));

        GameMgr.GetInstance().AddAttackEffect(Temp);
        GameMgr.GetInstance().Attack(CurrentDamage, Color.blue);
    }

    public override void Regeneration()
    {
        
    }
}
