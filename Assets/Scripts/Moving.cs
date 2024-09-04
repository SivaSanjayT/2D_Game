using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Moving : MonoBehaviour
{
    public Transform[] Points;

    public int Currentpos = 0;
    public float moveSpeed = 2f;

    public bool isOnPlatform = false;
    public bool canMove = true;
    public Renderer PlatformRender;
    public Material GlowTex, MainTex;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            float distance = Vector3.Distance(transform.position, Points[Currentpos].position);

            if (distance < 0.1f)
            {
                Currentpos++;
            }

            if (Currentpos >= Points.Length)
            {
                Currentpos = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, Points[Currentpos].position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOnPlatform)
        {
            isOnPlatform = true;
            PlatformRender.material = GlowTex;
        }
    }
}
