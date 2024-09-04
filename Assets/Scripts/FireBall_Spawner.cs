using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall_Spawner : MonoBehaviour
{
    public ObjectPooling objectpool;

    public Transform[] Points;

    public float Delay = 3f;

    private void Start()
    {
        StartCoroutine(SpawnDelay(Delay));
    }

    IEnumerator SpawnDelay(float Delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);

            Transform Point = Points[Random.Range(0, Points.Length)];

            FireBallSpawn(Point);
        }
    }

    void FireBallSpawn(Transform Point)
    {
        GameObject FireBall = objectpool.GetObject();

        if (FireBall != null)
        {
            FireBall.transform.position = Point.position;
            FireBall.transform.rotation = Point.rotation;
            FireBall.SetActive(true);
        }
    }
}
