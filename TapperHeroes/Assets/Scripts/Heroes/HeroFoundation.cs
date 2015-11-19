using UnityEngine;
using System.Collections;

public abstract class HeroFoundation : MonoBehaviour
{
    public long Buff_Damage = 0;
    public long Damage = 0;
    public float RegenTime = 120;
    public float Buff_AttackSpeed = 0;
    public float AttackSpeed = 0;
    public float ActivateSkillNumber = 0;

    protected float AttackTime = 0;
    protected float CurrentRegenTime = 0;
    protected long CurrentDamage = 0;
    protected float CurrentAttackSpeed = 0;

    public long current_damage
    {
        get
        {
            return CurrentDamage;
        }
    }
    public float attacktime
    {
        get
        {
            return AttackTime;
        }
        set
        {
            AttackTime = value;
        }
    }
    public float current_regentime
    {
        get
        {
            return CurrentRegenTime;
        }
        set
        {
            CurrentRegenTime = current_regentime;
        }
    }
    public float current_attackspeed
    {
        get
        {
            return CurrentAttackSpeed;
        }
    }

    public int HeroLevel = 1;
    public bool IsDead = false;

    public abstract void Init();
    public abstract void Awake();
    public abstract void Update();
}
