using UnityEngine;
using System.Collections;
using System;

public class Hero_Behavior : HeroFoundation
{
    public uint Ori_Damage = 1;
    public float Ori_AttackSpeed = 1.0f;
    public string Name = "";
    public GameObject AttackParticle = null;
    Animation Ani = null;

    public override void Init()
    {
        CurrentAttackSpeed = Ori_AttackSpeed;
        CurrentDamage = Ori_Damage;
        if( Ani == null)
        {
            Ani = GetComponent<Animation>();
        }
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
                PlayAttack();
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

        GameMgr.GetInstance().AddAttackList(gameObject, CurrentDamage, CurrentAttackSpeed, Temp);
    }

    public override void Regeneration()
    {
        
    }

    public override IEnumerable PlayAttack()
    {
        Ani.CrossFade("Lumbering", 0.5f);
        yield return new WaitForSeconds(CurrentAttackSpeed);
        PlayWait();
    }

    public override void PlayWait()
    {
        Ani.CrossFade("Idle", 0.5f);
    }
}
