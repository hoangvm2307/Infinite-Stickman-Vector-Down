using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.05f;
    public float slowdownLength = 2f;
    public float manaCost;
    [SerializeField] private ManaManager manaManager;
    [SerializeField]private bool isUsingSkill;
    public void ActivateSlowmotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        GameplayController.instance.isUsingSkill = true;
    }
    public void DeactivateSlowmotion()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        GameplayController.instance.isUsingSkill = false;
    }
    public void ActivateSlowmotionOnShoot()
    {
        if (!GameplayController.instance.isUsingSkill)
        {
            Time.timeScale = 0.4f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
    public void DeactivateSlowmotionOnShoot()
    {
        if (!GameplayController.instance.isUsingSkill)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }

}
