using UnityEngine;
using System.Collections;

public abstract class Monster : MonoBehaviour
{
    public int MaxHP = 100;
    protected int CurrentHP = 0;

    public float CurrentHPpercent
    {
        get
        {
            return ((float)(CurrentHP / MaxHP));
        }
    }
	
}
