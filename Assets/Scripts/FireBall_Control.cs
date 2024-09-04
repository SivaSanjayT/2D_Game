
using UnityEngine;

public class FireBall_Control : MonoBehaviour
{
    public ObjectPooling objectpool;

    public Transform ColliderP;
    public LayerMask EndCollide;

    public float ColiderRadi;

    private void Update()
    {
        Collider2D[] EndCollider = Physics2D.OverlapCircleAll(ColliderP.transform.position, ColiderRadi, EndCollide);

        foreach (Collider2D EndCollid in EndCollider)
        {
            objectpool.ReturnObject(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            objectpool.ReturnObject(gameObject);
        }
    }
}
