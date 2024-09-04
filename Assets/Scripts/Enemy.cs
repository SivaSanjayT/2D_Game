using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public Transform EnemyTransform, PlayerTransform, attackPoint, Healthbar;
    public Rigidbody2D EnRb;
    public Animator Animator;
    public Transform[] Points;
    public LayerMask PlayerLayer;
   
    
    private Collider2D coll;

    public float EnemyMoveSpeed = 2f, PointRadius = 1f, ColliderRadi = 0.1f;
    public int CurrentPoint = 0;

    public enum State { Run_Knight, Attack_Knight}
    private State Cstate = State.Run_Knight;

    // Start is called before the first frame update
    void Start()
    {
        EnRb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.right, PointRadius, PlayerLayer);

        if (Hit.collider != null && Hit.collider.CompareTag("Player"))
        {

            print("Player detected");
            if (Cstate != State.Attack_Knight)
            {
                Cstate = State.Attack_Knight;
            }
        }
        else
        {
            if (Cstate != State.Run_Knight)
            {
                Cstate = State.Run_Knight;
            }
        }

        Animator.SetInteger("State", (int)Cstate);
        Patroling();
    }

    void Patroling()
    {
        

        float Distance = Vector3.Distance(EnemyTransform.position, Points[CurrentPoint].position);

        if ( Distance < 0.1f)
        {
            CurrentPoint++;
            EnemyTransform.rotation = Quaternion.Euler(0, 0, 0);
            Healthbar.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (CurrentPoint >= Points.Length)
        {
            CurrentPoint = 0;
            EnemyTransform.rotation = Quaternion.Euler(0, 180, 0);
            Healthbar.rotation = Quaternion.Euler(0, 0, 0);

        }
        EnemyTransform.position = Vector3.MoveTowards(EnemyTransform.position, Points[CurrentPoint].position, EnemyMoveSpeed * Time.deltaTime);
    }
    public void EnemyAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, ColliderRadi, PlayerLayer);

        foreach( Collider2D Player in player)
        {
            Health playerHealth = Player.GetComponent<Health>();
            print("player detected");
           
            if (playerHealth != null)
            {
                playerHealth.health(25);
            }
        }
        print("player detected");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, ColliderRadi);
        Gizmos.DrawRay(transform.position, transform.right* PointRadius);
    }
}
