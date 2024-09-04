using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{   
    public LayerMask BossLayer, FireBallLayer;

    public ObjectPooling ObjectPool;
    public ObjectPooling SlashObj;
    public GameObject SlashPrefab;

    public float SlashRadi;
    public float Delay;

    // Start is called before the first frame update
    void Start()
    {
        SlashObj = FindAnyObjectByType<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] Boss = Physics2D.OverlapCircleAll(transform.position, SlashRadi, BossLayer);
        Collider2D[] FireBall = Physics2D.OverlapCircleAll(transform.position, SlashRadi, FireBallLayer);

        foreach ( Collider2D coll in Boss)
        {
            Health BossHealth = coll.GetComponent<Health>();

            if (BossHealth != null)
            {
                BossHealth.health(50);
                SlashObj.ReturnObject(gameObject);

            }
        }

        foreach (Collider2D fireballs in FireBall)
        {
            if (ObjectPool != null)
            {
                Debug.Log("Attacked by player");
                ObjectPool.ReturnObject(fireballs.gameObject);
                SlashObj.ReturnObject(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           SlashObj.ReturnObject(gameObject);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SlashRadi);
    }
}
