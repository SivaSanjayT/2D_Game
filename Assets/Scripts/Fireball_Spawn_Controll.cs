using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Spawn_Controll : MonoBehaviour
{

    private ObjectPooling objectpooling;

    public float Delay = 2f;

    private bool Iscalled = false;

    public void CalledOn(ObjectPooling pool)
    {
        ObjectPooling pooling = pool;
        Iscalled = true;
    }
    private void OnEnable()
    {
        if (Iscalled)
        {
            StartCoroutine(OnDelayReturnToPool(Delay));
        }
        else
        {
            Debug.Log("Object reference not set OnEnable");
        }
    }
    IEnumerator OnDelayReturnToPool(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        if (objectpooling != null)
        {
            Debug.Log("retuneing fire ball after delay");
            objectpooling.ReturnObject(gameObject);
        }
        else
        {
            Debug.Log("object reference not set onDelayReturnTopool");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (objectpooling != null)
            {
                objectpooling.ReturnObject(gameObject);
            }
        }
        else
        {
            Debug.Log("Object reference not set in OnTrigger");
        }
    }
}
