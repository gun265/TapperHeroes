using UnityEngine;
using System.Collections;

public class Skill_IncreaseAttackSpeed : HeroSkillFoundation
{
    public float IncreasePercent = 0.1f;

    public override void ApplySkill(Hero_Behavior _Owner)
    {
        _Owner.Buff_AttackSpeed += _Owner.Ori_AttackSpeed * IncreasePercent;
        IsApply = true;
    }
}
