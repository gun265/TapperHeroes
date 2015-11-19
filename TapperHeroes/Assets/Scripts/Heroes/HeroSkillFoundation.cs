using UnityEngine;
using System.Collections;

public abstract class HeroSkillFoundation : MonoBehaviour
{
    public bool IsActivate = false;
    public bool IsApply = false;

    public abstract void ApplySkill(Hero_Behavior _Owner);
}
