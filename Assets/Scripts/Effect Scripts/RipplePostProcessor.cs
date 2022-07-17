using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += RippleEffect;
        EnemySpawner.OnEnemyDeath += RippleEffect;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= RippleEffect;
        EnemySpawner.OnEnemyDeath -= RippleEffect;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }
    public void RippleEffect()
    {
        this.Amount = this.MaxAmount;
        Vector3 pos = Input.mousePosition;
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
    }
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}