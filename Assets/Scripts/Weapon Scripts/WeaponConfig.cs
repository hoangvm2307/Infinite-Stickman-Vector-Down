using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeControlAttack
{
    Click,
    Hold
}

[System.Serializable]
public struct DefaultConfig
{

    [Range(0, 100)]
    public int damage;

    [Range(0, 100)]
    public int criticalDamage;

    [Range(0.1f, 1.0f)]
    public float fireRate;

    [Range(1, 100)]
    public int criticalRate;
}