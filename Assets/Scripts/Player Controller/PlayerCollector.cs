using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private ManaManager manaManager;
    public Weapon playerWeapon;
    private bool canChangeWeapon;
    private void OnEnable()
    {
        GameplayController.OnPressedBulletButton += CanChangeWeapon;
    }
    private void OnDisable()
    {
        GameplayController.OnPressedBulletButton -= CanChangeWeapon;
    }
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Energy Drink"))
        {
            manaManager.IncreaseMana(30);
            SmartPool.instance.SpawnObjectFromPool("Blood FX", collision.transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Weapon Collector"))
        {
            if (GameplayController.instance.canChangeWeapon)
            {
                playerWeapon.weaponData = collision.GetComponent<WeaponCollect>().weaponData;
                SmartPool.instance.SpawnObjectFromPool("Blood FX", collision.transform.position, Quaternion.identity);
                collision.gameObject.SetActive(false);
            }
        }
        if(collision.CompareTag("Mana Upgrade"))
        {
            collision.GetComponent<ManaHealthUpgrade>().IncreaseManaScale();
            collision.gameObject.SetActive(false);
            SmartPool.instance.SpawnObjectFromPool("Blood FX", collision.transform.position, Quaternion.identity);
        }
        if(collision.CompareTag("Health Upgrade"))
        {
            collision.GetComponent<ManaHealthUpgrade>().IncreaseHealthScale();
            collision.gameObject.SetActive(false);
            SmartPool.instance.SpawnObjectFromPool("Blood FX", collision.transform.position, Quaternion.identity);
        }
    }
    private void CanChangeWeapon()
    {
        canChangeWeapon = true;
    }
    void FixedUpdate()
    {

    }
}
