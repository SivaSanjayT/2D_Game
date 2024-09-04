using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Falling : MonoBehaviour
{

    public Rigidbody2D RbPlatform;
    public float DelayFall = 2;
    public bool isOnPlatform = false;

    public Material MainTex;
    public Material GlowTex;
    public Renderer PlatformRender;



    // Start is called before the first frame update
    void Start()
    {
        RbPlatform = GetComponent<Rigidbody2D>();
        RbPlatform.bodyType = RigidbodyType2D.Static;
        PlatformRender = GetComponent<Renderer>();
        PlatformRender.material = MainTex;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOnPlatform)
        {
            isOnPlatform = true;
            PlatformRender.material = GlowTex;
            StartCoroutine(DelayforMaterialoFF(1.5f));
            StartCoroutine(OnDelayFall(DelayFall));
        }
    }

    IEnumerator DelayforMaterialoFF(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        PlatformRender.material = MainTex;
    }
    IEnumerator OnDelayFall(float DelayTimer)
    {

        yield return new WaitForSeconds(DelayTimer);
        RbPlatform.bodyType = RigidbodyType2D.Dynamic;
    }
}
