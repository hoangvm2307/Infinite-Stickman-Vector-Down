using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NameWeapon
{
    MELEE,
    PISTOL,
    M4,
    SHOTGUN,
    DUAL,
    SNIPER,
    AK47
}
public class SubWeapon : MonoBehaviour
{
     

    protected float lastShot;
    public int currentBullet;
    public WeaponData weaponData;
    private void Awake()
    {
        //currentBullet = weaponData.BulletMax;
    }
    public virtual void FlipWeapon() { }

    public void CallAttack()
    {

        if (Time.time > lastShot + weaponData.FireRate)
        {
            if (currentBullet > 0)
            {
                if (weaponData.typeControl == TypeControlAttack.Click)
                    StartCoroutine(ProcessShoot());
                else
                    NormalShoot();
                //// animate shoot
                //playerAnim.AttackAnimation();


                lastShot = Time.time;
                currentBullet--;

            }
            else
            {
                // PLAY NO AMMO SOUND
            }
        }

    } // call attack
    public virtual IEnumerator ProcessShoot()
    {
        yield return 0;
    }
    public virtual void NormalShoot()
    {

    }
}
