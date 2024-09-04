using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricball : MonoBehaviour
{
    public LayerMask Playerlayer;
    public float ElectricRadi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] Player = Physics2D.OverlapCircleAll(transform.position, ElectricRadi, Playerlayer);
        
        foreach (Collider2D colli in Player)
        {
            Health playerHealth = colli.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.health(100);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
