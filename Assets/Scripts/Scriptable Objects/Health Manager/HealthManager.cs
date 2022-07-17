using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Health Manager", menuName = "Health Manager")]
public class HealthManager : ScriptableObject
{
    public int health = 100;
    [SerializeField]
    public int maxHealth = 100;
    public int tempHealth;
    [System.NonSerialized]
    public UnityEvent<int> healthChangeEvent;

    private void OnEnable()
    {
        if(healthChangeEvent == null)
        {
            healthChangeEvent = new UnityEvent<int>();
        }
    }
    public void IncreaseHealth(int amount)
    {
        health += amount;
        if (health - tempHealth > amount)
        {
            health = tempHealth + amount;
            tempHealth = health;
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        healthChangeEvent.Invoke(health);
    }
    public void DecreaseHealth(int damage)
    {
        health -= damage;
        if(tempHealth - health > damage)
        {
            health = tempHealth - damage;
        }
        healthChangeEvent.Invoke(health);
    }
}
