using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Mana Manager", menuName = "Mana Manager")]
public class ManaManager : ScriptableObject
{
    public float mana = 100;
    [SerializeField]
    public float maxMana = 100;
    [System.NonSerialized]
    public UnityEvent<float> manaChangeEvent;
    public float tempMana;

    private void OnEnable()
    {
        if (manaChangeEvent == null)
        {
            manaChangeEvent = new UnityEvent<float>();
        }
    }
    
    public void DecreaseMana(float amount)
    {
        mana -= amount * Time.fixedDeltaTime;
        if(mana <= 0)
        {
            mana = 0;
        }
        manaChangeEvent.Invoke(mana);
    }
    public void DecreaseManaOneTime(float amount)
    {
        mana -= amount;
        if(mana <= 0)
        {
            mana = 0;
        }
        manaChangeEvent.Invoke(mana);
    }
    public void IncreaseMana(float amount)
    {       
        mana += amount;
        if(mana - tempMana > amount)
        {
            mana = tempMana + amount;
        }
        if(mana >= maxMana)
        {
            mana = maxMana;
        }
        manaChangeEvent.Invoke(mana);
    }
}
