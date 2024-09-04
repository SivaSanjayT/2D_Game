using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Slash : MonoBehaviour
{
    public Transform SlashTransform;
    public Rigidbody2D SlashRb;

    public Transform AbilitySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        SlashRb = GetComponent<Rigidbody2D>();
        SlashTransform.position = AbilitySpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy)
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("oN pOERuP");
            gameObject.SetActive(false);
        }
    }
}
