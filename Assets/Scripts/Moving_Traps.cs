
using UnityEngine;


public class Moving_Traps : MonoBehaviour
{
    public float FireBallSpeed = 10f;
    void Update()
    {
        transform.position += FireBallSpeed * Time.deltaTime * -transform.right;
    } 

    
}
