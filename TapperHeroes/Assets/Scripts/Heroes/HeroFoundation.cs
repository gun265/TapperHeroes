using UnityEngine;
using System.Collections;

public abstract class HeroFoundation : MonoBehaviour
{
    public float RegenTime = 120;

    protected int HeroLevel = 1;
    protected uint CurrentDamage = 0;
    protected float CurrentAttackSpeed = 0;
    protected float AttackTime = 0;    
    protected float CurrentRegenTime = 0;
    protected bool IsDead = false;
    protected Transform ThisTransform;

    public abstract void Init();
    public abstract void Awake();
    public abstract void Update();
    public abstract void Attack();
    public abstract void Regeneration();
}
