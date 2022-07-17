using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Ability ability;
    [SerializeField] private AdsInitializer adsInit;
    public bool isUsingPPVolume;
    private void Awake()
    {
        MakeSingleton();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name == "Gameplay")
        {
            AbilityController playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityController>();
            playerAbility.ability = ability;
           //adsInit.InitializeAds();
        }
        if(scene.name == "Menu")
        {
            RewardedAdsButton rewardedAdsButton = GameObject.Find("Ads Button").GetComponent<RewardedAdsButton>();
            rewardedAdsButton.LoadAd();
        }
    }
    public void ChooseForceField()
    {
        ability = Ability.FORCEFIELD;
    }
    public void ChooseEightSwords()
    {
        ability = Ability.SWORDAROUND;
    }
    public void ChooseEnergyShield()
    {
        ability = Ability.ENERGYSHIELD;
    }
    public void ChooseTimeControl()
    {
        ability = Ability.SLOWDOWN;
    }
}
