using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image health;
    [SerializeField] public Transform healthBar;
    [SerializeField] private Image mana;
    [SerializeField] public RectTransform manaBar;
     
    [SerializeField] private float timeIndicator;

    [Header("Text")]
    [SerializeField] private Text coinAmountText;
    [SerializeField] public Text timeIndicatorText;
    [Header("Scriptable Objects")]
    [SerializeField] private HealthManager healthManagerScriptableObject;
    [SerializeField] private ManaManager manaManagerScriptableObject;
    [SerializeField] private CoinData coinDataScriptableObject;
    [SerializeField] private GameplayController gameplayController;
    private float manaHealthCheckTime;
    private void Awake()
    {
        coinAmountText.text = coinDataScriptableObject.amountOfCoin.ToString();
    }
    void Start()
    {
        //coinAmountText.text = coinDataScriptableObject.amountOfCoin.ToString();
    }
    private void OnEnable()
    {
        healthManagerScriptableObject.healthChangeEvent.AddListener(ChangeHealthValue);
        manaManagerScriptableObject.manaChangeEvent.AddListener(ChangeManaValue);
        coinDataScriptableObject.coinChangeEvent.AddListener(ChangeCoinValue);
    }
    private void OnDisable()
    {
        healthManagerScriptableObject.healthChangeEvent.RemoveListener(ChangeHealthValue);
        manaManagerScriptableObject.manaChangeEvent.RemoveListener(ChangeManaValue);
        coinDataScriptableObject.coinChangeEvent.RemoveListener(ChangeCoinValue);
    }
    // Update is called once per frame
    void Update()
    {
        ConstraintManaHealth();
        timeIndicatorText.text = gameplayController.timeIndicator.ToString("F4");
    }
    public void ChangeHealthValue(int amount)
    {
        health.fillAmount = ConvertIntToFloatDecimal(amount);
    }
    public void ChangeManaValue(float amount)
    {
        mana.fillAmount = amount / manaManagerScriptableObject.maxMana;
    }
    private float ConvertIntToFloatDecimal(int amount)
    {
        return (float)amount / healthManagerScriptableObject.maxHealth;
    }
    public void ChangeCoinValue(int amount)
    {
        coinAmountText.text = amount.ToString();
    }
    public void IncreaseManaScale()
    {
        Vector3 temp = manaBar.localScale;
        temp.x += 0.2f;
        manaBar.localScale = temp;
        manaManagerScriptableObject.IncreaseMana(30);
    }
    private void ConstraintManaHealth()
    {
        manaHealthCheckTime -= Time.deltaTime;
        if(manaHealthCheckTime <= 0)
        {
            manaManagerScriptableObject.tempMana = manaManagerScriptableObject.mana;
            healthManagerScriptableObject.tempHealth = healthManagerScriptableObject.health;

            manaHealthCheckTime = 0.5f;
        }
    }
}
