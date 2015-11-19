using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Hero_Behavior : HeroFoundation
{
    public long Ori_Damage = 1;
    public float Ori_AttackSpeed = 1.0f;
    public float CriticalPercent = 10;
    public string Name = "";
    public GameObject AttackParticle = null;
    public Animation Ani = null;
    public Hero_StateMachine<Hero_Behavior> StateMachine = null;
    public List<HeroSkillFoundation> SkillList = null;

    public override void Init()
    {
        CurrentAttackSpeed = Ori_AttackSpeed;
        CurrentDamage = Ori_Damage;
        if (Ani == null)
        {
            Ani = GetComponent<Animation>();
        }

        if (StateMachine == null)
        {
            StateMachine = new Hero_StateMachine<Hero_Behavior>();
            StateMachine.Init(this, new Hero_Live());
        }

        int index = 0;
        foreach (HeroSkillFoundation _Skill in SkillList)
        {
            if (index < ActivateSkillNumber)
            {
                _Skill.IsActivate = true;
                _Skill.ApplySkill(this);
            }
            else
            {
                _Skill.IsActivate = false;
                _Skill.IsApply = false;
            }
            index++;
        }
    }

    public void PlayIdleAnimation()
    {
        Ani.CrossFade("Idle",0.2f);
    }

    public override void Awake()
    {
        Init();
    }

    public override void Update()
    {
        CalculateCurrentState();
        StateMachine.Update(Time.deltaTime);
    }

    public void CalculateCurrentState()
    {
        Damage = CurrentDamage + Buff_Damage  * (long)(HeroLevel);
        AttackSpeed = CurrentAttackSpeed - Buff_AttackSpeed;
        if (AttackSpeed < 0.5f)
        {   
            AttackSpeed = 0.5f;
        }
    }
}
