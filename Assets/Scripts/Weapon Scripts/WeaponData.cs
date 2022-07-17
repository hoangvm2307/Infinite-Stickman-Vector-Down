using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New WeaponData", menuName = "WeaponData", order = 51)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;     
    [SerializeField] private float forceBody;

    public NameWeapon nameWp;
    public TypeControlAttack typeControl;

    [SerializeField] [Range(0, 100)] private int attackDamage;
    [SerializeField] [Range(0, 100)] private int criticalDamage;
    [SerializeField] [Range(0.1f, 1.0f)] private float fireRate;
    [SerializeField] [Range(0, 100)] private float criticalRate;
    [SerializeField] [Range(0, 100)] private int bulletMax;
    [SerializeField] [Range(0.1f, 2f)] private float timeExist;

    public string WeaponName
    {
        get
        {
            return weaponName;
        }
    }
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }
    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
    }
    public float ForceBody
    {
        get
        {
            return forceBody;
        }
    }
    public float CriticalDamage
    {
        get
        {
            return criticalDamage;
        }
    }
    public float FireRate
    {
        get
        {
            return fireRate;
        }
    }
    public float CriticalRate
    {
        get
        {
            return criticalRate;
        }
    }
    public int BulletMax
    {
        get
        {
            return bulletMax;
        }
    }
    public float TimeExist
    {
        get
        {
            return timeExist;
        }
    }
}
