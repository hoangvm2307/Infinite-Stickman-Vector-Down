using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Ability
{
    SLOWDOWN,
    FORCEFIELD,
    SWORDAROUND,
    GREATSWORD,
    ENERGYSHIELD
}
public class AbilityController : MonoBehaviour
{
    [Header("Ability")]
    [SerializeField] private TimeManager timeManagerAbility;
    [SerializeField] private ForceField forceFieldAbility;
    [SerializeField] private SwordRound swordRoundAbility;
    [SerializeField] private EnergyShield energyShieldAbility;
    [Header("Data")]
    [SerializeField] private ManaManager manaManagerSO;
    [SerializeField] private Transform playerHips;
    [SerializeField] public Ability ability;

    [Header("Objects")]
    [SerializeField] private GameObject swordRound;
    [SerializeField] private GameObject shockwave;
    [SerializeField] private GameObject energyShield;
    private bool isSlow;
    private bool isActivatingAbility;
    [SerializeField] public int manaCost;
    private GameObject tempSwordRound, tempEnergyShield;
    private void Awake()
    {
        isSlow = false;
        isActivatingAbility = false;
        playerHips = GameObject.Find(NameTag.PLAYER_HIPS).transform;
    }
    void Start()
    {
        //isSlow = false;
        //isActivatingAbility = false;
        //playerHips = GameObject.Find(NameTag.PLAYER_HIPS).transform;
    }
    private void OnEnable()
    {
        GameplayController.OnPressedAbilityButton += CastAbility;
    }
    private void OnDisable()
    {
        GameplayController.OnPressedAbilityButton -= CastAbility;
    }
    // Update is called once per frame
    void Update()
    {
        if (isSlow || isActivatingAbility)
        {
            switch (ability)
            {
                case Ability.SWORDAROUND:
                    tempSwordRound.transform.position = playerHips.position;
                    manaManagerSO.DecreaseMana(swordRoundAbility.manaCost);
                    if(manaManagerSO.mana <= 0)
                    {
                        tempSwordRound.SetActive(false);
                        isActivatingAbility = false;
                    }
                    break;
                case Ability.ENERGYSHIELD:
                    tempEnergyShield.transform.position = playerHips.position;
                    manaManagerSO.DecreaseMana(energyShieldAbility.manaCost);
                    if(manaManagerSO.mana <= 0)
                    {
                        tempEnergyShield.SetActive(false);
                        isActivatingAbility = false;
                    }
                    break;
                case Ability.SLOWDOWN:
                    manaManagerSO.DecreaseMana(timeManagerAbility.manaCost);
                    if (manaManagerSO.mana <= 0)
                    {
                        isActivatingAbility = false;
                        timeManagerAbility.DeactivateSlowmotion();
                    }
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (ability)
            {
                case Ability.FORCEFIELD:
                    ActivateForceFieldAbility();
                    break;
                case Ability.SLOWDOWN:
                    ActivateSlowmotionAbility();
                    break;
                case Ability.SWORDAROUND:
                    ActivateSwordRoundAbility();
                    break;
                case Ability.ENERGYSHIELD:
                    ActivateEnergyShield();
                    break;
            }
        }
    }
    private void ActivateForceFieldAbility()
    {
        if (manaManagerSO.mana >= forceFieldAbility.manaCost)
        {
            forceFieldAbility.ForceFieldAbility(playerHips.position);
            Instantiate(shockwave, playerHips.position, Quaternion.identity);
            manaManagerSO.DecreaseManaOneTime(forceFieldAbility.manaCost);
        }
    }
    private void ActivateSlowmotionAbility()
    {
        isSlow = !isSlow;
        if (isSlow == true)
        {
            timeManagerAbility.ActivateSlowmotion();

        }
        else
        {
            timeManagerAbility.DeactivateSlowmotion();
        }
    }
    private void ActivateSwordRoundAbility()
    {
        if (manaManagerSO.mana >= swordRoundAbility.manaCost)
        {
            isActivatingAbility = !isActivatingAbility;
            if (isActivatingAbility)
            {
                tempSwordRound = SmartPool.instance.SpawnObjectFromPool("SwordRound", position: playerHips.position, Quaternion.identity);
            }
            else
            {
                tempSwordRound.SetActive(false);
            }
        }
    }
    private void ActivateEnergyShield()
    {
        if(manaManagerSO.mana >= energyShieldAbility.manaCost)
        {
            isActivatingAbility = !isActivatingAbility;
            if (isActivatingAbility)
            {
                tempEnergyShield = SmartPool.instance.SpawnObjectFromPool("Energy Shield", position: playerHips.position, Quaternion.identity);
            }
            else
            {
                tempEnergyShield.SetActive(false);
            }
        }
    }
    private void CastAbility()
    {
        switch (ability)
        {
            case Ability.FORCEFIELD:
                ActivateForceFieldAbility();
                break;
            case Ability.SLOWDOWN:
                ActivateSlowmotionAbility();
                break;
            case Ability.SWORDAROUND:
                ActivateSwordRoundAbility();
                break;
            case Ability.ENERGYSHIELD:
                ActivateEnergyShield();
                break;
        }
    }
}
