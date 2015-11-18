using UnityEngine;
using System.Collections;

public abstract class HeroFoundation : MonoBehaviour
{
    public float RegenTime = 120;

    public int HeroLevel = 1;
    public uint CurrentDamage = 0;
    public float CurrentAttackSpeed = 0;
    public float AttackTime = 0;
    public float CurrentRegenTime = 0;
    public bool IsDead = false;

    public abstract void Init();
    public abstract void Awake();
    public abstract void Update();
    public abstract void Attack();
    public abstract void Regeneration();
    public abstract IEnumerable PlayAttack();
    public abstract void PlayWait();
}
