using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    private bool allowLoading;

    [SerializeField] private GameObject abilityPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void CloseAbilityPanel()
    {
        abilityPanel.SetActive(false);
    }
    public void OpenAbilityPanel()
    {
        abilityPanel.SetActive(true);
    }
    public void ChooseForceField()
    {
        GameManager.instance.ability = Ability.FORCEFIELD;
    }
    public void ChooseEightSwords()
    {
        GameManager.instance.ability = Ability.SWORDAROUND;
    }
    public void ChooseEnergyShield()
    {
        GameManager.instance.ability = Ability.ENERGYSHIELD;
    }
    public void ChooseTimeControl()
    {
        GameManager.instance.ability = Ability.SLOWDOWN;
    }
    private IEnumerator callPlay()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
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
