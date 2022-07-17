using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaHealthUpgrade : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] ManaManager manaManagerScriptableObject;
    [SerializeField] HealthManager healthManagerScriptableObject;
    private float manaCheckTime;
    private float healthCheckTime;

    private float tempManaScale;
    private float tempMaxMana;

    private float tempHealthScale;
    private int tempMaxHealth;
    void Start()
    {
        manaCheckTime = 0.5f;
        uiManager = GameObject.Find("UI Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        manaCheckTime -= Time.deltaTime;
        if(manaCheckTime <= 0)
        {
            if (transform.CompareTag("Mana Upgrade"))
            {
                Vector3 temp = uiManager.manaBar.localScale;
                tempManaScale = temp.x;
                tempMaxMana = manaManagerScriptableObject.maxMana;
            }

            if (transform.CompareTag("Health Upgrade"))
            {
                Vector3 tempHealth = uiManager.healthBar.localScale;
                tempHealthScale = tempHealth.x;
                tempMaxHealth = healthManagerScriptableObject.maxHealth;
            }
            manaCheckTime = 0.5f;
        }
    }
    public void IncreaseManaScale()
    {
        Vector3 temp = uiManager.manaBar.localScale;
        temp.x += 0.2f;
        if (temp.x - tempManaScale > 0.2f)
        {
            temp.x = tempManaScale + 0.2f;
        }
        uiManager.manaBar.localScale = temp;
         
        manaManagerScriptableObject.maxMana += 30;
        if (manaManagerScriptableObject.maxMana - tempMaxMana > 30)
        {
            manaManagerScriptableObject.maxMana = tempMaxMana + 30;
        }
        manaManagerScriptableObject.IncreaseMana(30);
    }
    public void IncreaseHealthScale()
    {
        Vector3 temp = uiManager.healthBar.localScale;
        temp.x += 0.2f;
        if (temp.x - tempHealthScale > 0.2f)
        {
            temp.x = tempHealthScale + 0.2f;
        }
        uiManager.healthBar.localScale = temp;

        healthManagerScriptableObject.maxHealth += 30;
        if (healthManagerScriptableObject.maxHealth - tempMaxHealth > 30)
        {
            healthManagerScriptableObject.maxHealth = tempMaxHealth + 30;
        }
        healthManagerScriptableObject.IncreaseHealth(30);
    }
}
