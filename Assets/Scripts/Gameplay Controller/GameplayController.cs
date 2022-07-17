using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameplayController : MonoBehaviour
{
    #region singleton
    public static GameplayController instance;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Awake()
    {
        MakeInstance();
         
        difficulty = 1;
        timeUpDifficulty = timeUpDifficulty_Editor;
        manaManagerSO.maxMana = 100;
        manaManagerSO.mana = 100;
        gameOver = false;

        healthManagerSO.maxHealth = 100;
        healthManagerSO.health = 100;
        playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityController>();
         
        Application.targetFrameRate = 60;
        highscoreText.text = PlayerPrefs.GetFloat("High Score", 0).ToString("F4");

    }
    void OnDisable()
    {
        instance = null;
    }
    public void Refresh()
    {
        if (timeIndicator > PlayerPrefs.GetFloat("High Score", 0))
        {
            PlayerPrefs.SetFloat("High Score", timeIndicator);
            highscoreText.gameObject.SetActive(true);
            currentscoreText.gameObject.SetActive(false);

            highscoreText.text = timeIndicator.ToString("F4");
            highscoreTextInform.text = highscoreText.text;
        }
        else
        {
            highscoreText.gameObject.SetActive(false);
            currentscoreText.gameObject.SetActive(true);

            currentscoreText.text = timeIndicator.ToString("F4");
            highscoreTextInform.text = highscoreText.text;
        }
         
        rewardedAdsButton.LoadAd();
        //adsInit.InitializeAds();
        SaveGame();
        gameOverPanel.SetActive(false);
        coinData.LoadCoin();
    }
    #endregion
    public int weaponUnlocked = 2;
    public List<WeaponData> weaponList = new List<WeaponData>();
    [SerializeField] private ManaManager manaManagerSO;
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private CoinData coinData;
    [SerializeField] private CoinDataScript coinDataScript;
    [SerializeField] private GameObject pausePanel;
    public float timeIndicator;
    public float difficulty;
    private float timeUpDifficulty;
    [SerializeField] private float timeUpDifficulty_Editor;

    public static event Action OnPressedBulletButton;
    public static event Action OnPressedAbilityButton;
    public static event Action OnpressedSwordButton;

    [HideInInspector] public int enemySpawnerCounter;
    private bool allowLoading;
    private Vector2 startingPoint;
    private int leftTouch = 99;
    AbilityController playerAbility;
    [SerializeField] GameObject abilityPanel;
    public bool isPausing;
    public bool gameOver;
    [HideInInspector] public bool isUsingSkill;
    [SerializeField] private GameObject gameOverPanel;
    private float highScore, currentScore;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text currentscoreText;
    [SerializeField] private Text highscoreTextInform;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private AdsInitializer adsInit;
    public bool canChangeWeapon;
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    void Start()
    {
        highscoreText.text = PlayerPrefs.GetFloat("High Score", 0).ToString("F4");
        isPausing = true;
    }
  
    void Update()
    {
        if (!gameOver && !isPausing)
        {
            timeIndicator += Time.deltaTime;    
            timeUpDifficulty -= Time.deltaTime;
        }
        if (difficulty <= 2)
        {
            if (timeUpDifficulty <= 0)
            {
                difficulty += 0.2f;
                timeUpDifficulty = timeUpDifficulty_Editor;
            }
        }
       
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("High Score");
    }
    public IEnumerator OpenGameOverPanel()
    {        
        yield return new WaitForSeconds(2);
        if (timeIndicator > PlayerPrefs.GetFloat("High Score", 0))
        {
            PlayerPrefs.SetFloat("High Score", timeIndicator);
            highscoreText.gameObject.SetActive(true);
            currentscoreText.gameObject.SetActive(false);

            highscoreText.text = timeIndicator.ToString("F4");
            highscoreTextInform.text = highscoreText.text;
        }
        else
        {
            highscoreText.gameObject.SetActive(false);
            currentscoreText.gameObject.SetActive(true);

            currentscoreText.text = timeIndicator.ToString("F4");
            highscoreTextInform.text = highscoreText.text;
        }
        gameOverPanel.SetActive(true);
        rewardedAdsButton.LoadAd();
        //adsInit.InitializeAds();
        SaveGame();
        Time.timeScale = 0f;
    }
    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SaveGame();
        gameOver = false;
        SceneManager.LoadSceneAsync("Gameplay");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("Gameplay");
        isUsingSkill = false;
    }
    public void SaveGame()
    {
        SaveSystem.SaveCoin(coinData);
    }
    public void ShootBullet()
    {
        OnPressedBulletButton?.Invoke();
    }
    public void CastAbility()
    {
        OnPressedAbilityButton?.Invoke();
    }
    public void SwapWeapon()
    {
        OnpressedSwordButton?.Invoke();
    }
    public void ChooseForceField()
    {
        playerAbility.ability = Ability.FORCEFIELD;
        Time.timeScale = 1f;
        abilityPanel.SetActive(false);
    }
    public void ChooseEightSwords()
    {
        playerAbility.ability = Ability.SWORDAROUND;
        Time.timeScale = 1f;
        abilityPanel.SetActive(false);
    }
    public void ChooseEnergyShield()
    {
        playerAbility.ability = Ability.ENERGYSHIELD;
        Time.timeScale = 1f;
        abilityPanel.SetActive(false);
    }
    public void ChooseTimeControl()
    {
        playerAbility.ability = Ability.SLOWDOWN;
        Time.timeScale = 1f;
        abilityPanel.SetActive(false);
    }
    public void CanChangeWeapon()
    {
        canChangeWeapon = true;
    }
    public void CannotChangeWeapon()
    {
        canChangeWeapon = false;
    }
    private IEnumerator callBack()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        while (!allowLoading)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
