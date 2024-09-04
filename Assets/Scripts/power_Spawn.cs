using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Power_Spawn : MonoBehaviour
{

    public Transform trans, SpawnPosition;
    public Rigidbody2D Rb;

    public float ThrowForece;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = new Vector2(-transform.localScale.x, 1) * ThrowForece;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
