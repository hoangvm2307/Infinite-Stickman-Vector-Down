using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "New AbilityData", menuName = "Ability Data", order = 52)]
public class AbilityData : ScriptableObject
{
    public Ability ability;
    [Header("Slowdown Ability")]
    public float slowDownFactor = 0.05f;
    public float slowdownLength = 2f;

    [Header("Force Ability")]
    public float fieldOfImpact;
    public float force;
    public LayerMask targetMask;
    [Header("Sword Around")]
    [SerializeField] public GameObject SwordRound;

    [Header("Mana Cost")]
    [SerializeField] public float manaCost;
    [SerializeField] private ManaManager manaManager;
 
    public void ActivateSlowmotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void DeactivateSlowmotion()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
    public void ForceField(Vector3 position)
    {
        Collider2D[] objects =  Physics2D.OverlapCircleAll(point: position, radius: fieldOfImpact, layerMask: targetMask);
        foreach(Collider2D obj in objects)
        {
            Debug.Log(obj);
            Vector2 direction = obj.transform.position - position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }
}
