using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class GrenadeExplode : MonoBehaviour
{
    public Transform GrenadeTrans;
    public GameObject ExplodePrefab, grenadePrefab;
    public LayerMask PlayerLayer;

    public float ExplodeDelay = 3f;
    public float BurstRadius = 1f;

    public Material GlowMaterial;
    public Renderer GrenadeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (isActiveAndEnabled)
        {
            StartCoroutine(OnDelayExplode(ExplodeDelay));
            GrenadeRenderer = grenadePrefab.GetComponent<Renderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator OnDelayExplode(float ExplodeDelay)
    {
        float CurrentTime = 0f;

        while (CurrentTime < ExplodeDelay)
        {
            float lerpValue = CurrentTime / ExplodeDelay;

            float GlowIntencity = Mathf.Lerp(0f, 50f, lerpValue);

            GlowEffect(GlowIntencity);

            yield return null;

            CurrentTime += Time.deltaTime;
        }

        GlowEffect(10f);

        Instantiate(ExplodePrefab, GrenadeTrans.position, GrenadeTrans.rotation);

        Destroy(gameObject);
        
        Collider2D[] player = Physics2D.OverlapCircleAll(GrenadeTrans.position, BurstRadius, PlayerLayer);

        foreach (Collider2D Burst in player)
        {
            Health healths = Burst.GetComponent<Health>();

            if (healths != null)
            {
                healths.health(100);
            }
        }
    }

    void GlowEffect( float intensity)
    {
        if (GlowMaterial != null)
        {
            GlowMaterial.SetColor("_Glow", Color.white * intensity);
            GrenadeRenderer.material = GlowMaterial;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GrenadeTrans.position, BurstRadius);
    }
}
