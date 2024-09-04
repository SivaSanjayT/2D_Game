
using System.Collections;

using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Transforms")]
    public Transform Playertransform;
    public Transform CamTransform;
    public Transform AttackPoint;
    public Transform HoverPoint;

    [Header("Animations")]
    public Animator animator;
    public Animator BossAnimator;
    public Animator HoverAniamtion;

    [Header("Colliders and RigidBody")]
    public Rigidbody2D Rb;
    public BoxCollider2D BoxCollider2D;

    [Header("Layers and Vectors")]
    public LayerMask Grounded;
    public LayerMask Enemy;
    public LayerMask FireBall;
    public LayerMask Boss;
    public LayerMask healths;

    public Vector3 camoff;
    public Vector3 HoverOff;

    [Header("Script References")]
    public ObjectPooling objectpool;
    public ObjectPooling Slashpool;

    [Header("ObjectPrefabs")]
    public GameObject SlashPrefab;
    public GameObject HoverPrefab; 
    public GameObject HoverOrb;
    public GameObject BossSlider;

    [Header("Floats and Int values")]
    public float MoveSpeed = 1f;
    public float Jumpforce = 1f;
    public float ColliderRadi = 1;
    public float SlashSpeed = 1f;
    public float HoverAniDelay = 1.5f;
    public float SlashAttackCoolDown = 1f;
    public float LastdSlashAttackTime;
    public int AttackCount = 0;

    [Header("Bools")]
    public bool HoverStatus = false;
    public bool CanMove = true;

    [Header("Materials")]
    public SpriteRenderer TriggerRender;
    public Sprite Glow;
    public enum State { Idle_Jugger, Run_Jugger, Jump_Jugger, Attack_Jugger, Fall_Jugger}
    public static bool isPlayerDead = false;

    private Transform OriginalParent;
    private State state = State.Idle_Jugger;
    private Vector3 PrevPos;
    [SerializeField] GameObject GameStartPos;
    [SerializeField] Boss BossScript;
    public static Player Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Slashpool = FindObjectOfType<ObjectPooling>();
        CanMove = true;
        LastdSlashAttackTime -= SlashAttackCoolDown;
        
        Playertransform.position = GameStartPos.transform.position;
        PrevPos = transform.position;
        isPlayerDead = false;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(BoxCollider2D.bounds.center, Vector2.down * 0.1f, Color.red);

        Camerafunction();
        HoverPointPosition();
        if (isPlayerDead == false)
        {
            if (CanMove)
            {
                HandleMovement();
                HandleAnimations();
            }

            if (Input.GetMouseButtonDown(1) && HoverStatus == true)
            {
                if (Time.time >= LastdSlashAttackTime + SlashAttackCoolDown)
                {
                    SlashAttack();
                    LastdSlashAttackTime = Time.time;
                }

            }
        }
        if(animator.GetBool("Dead") == true)
        {
            isPlayerDead = true;
            CanMove = false;
        }
        else
        {
            isPlayerDead = false;
            CanMove = true;
        }


        if (Rb.velocity.y < -15f)
        {
            isPlayerDead = true;
        }
    }

    // Input, Movement, Animation and other Functions
    public void ResetPlayerPos()
    {
        PrevPos = GameStartPos.transform.position;
    }
    void Camerafunction()
    {
        CamTransform.position = new Vector3(Playertransform.position.x + camoff.x, Playertransform.position.y + camoff.y, -50 + camoff.z);
    }

    void HoverPointPosition()
    {

        if (HoverStatus)
        {
            HoverPoint.position = new Vector3(Playertransform.position.x + HoverOff.x, Playertransform.position.y + HoverOff.y, camoff.z);
        }
    }
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Rb.velocity = new Vector3(horizontal * MoveSpeed, Rb.velocity.y);

        if (horizontal > 0)
        {
            state = State.Run_Jugger;
            Playertransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            state = State.Run_Jugger;
            Playertransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        animator.SetInteger("State", (int)state);
    }

    void HandleAnimations()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Rb.velocity = new Vector2(Rb.velocity.x, Jumpforce);
            state = State.Jump_Jugger;
        }
        else if (Rb.velocity.y < -4f)
        {
            state = State.Fall_Jugger;
        }
        else if (Input.GetMouseButtonDown(0) && state != State.Attack_Jugger)
        {
            if (IsGrounded())
            {
                state = State.Attack_Jugger;
            }
            Debug.Log(state);
        }
        else if (Mathf.Abs(horizontal) > 0.1f)
        {
            state = State.Run_Jugger;
        }
        else
        {
            state = State.Idle_Jugger;
        }
        animator.SetInteger("State", (int)state);

        
    }

    void SlashAttack()
    {
        GameObject Slash = Slashpool.GetObject();

        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 SlashPointPos = HoverPoint.position;
        Vector2 Direction = (MousePos - SlashPointPos).normalized;

        Slash.transform.position = SlashPointPos;
        Slash.SetActive(true);

        Rigidbody2D SlashPrefabRb = Slash.GetComponent<Rigidbody2D>();
        SlashPrefabRb.velocity = Direction * SlashSpeed;

        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Slash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
    }

    // Event Trigger Functions
    public void PlayerAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(AttackPoint.transform.position, ColliderRadi, Enemy);
        Collider2D[] BoSs = Physics2D.OverlapCircleAll(AttackPoint.transform.position, ColliderRadi, Boss);
        Collider2D[] fireBall = Physics2D.OverlapCircleAll(AttackPoint.transform.position, ColliderRadi, FireBall);




        foreach (Collider2D Enemy in enemy)
        {
            Health enemyHealth = Enemy.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.health(25);
            }
        }

        foreach (Collider2D boss in BoSs)
        {
            AttackCount++;

        }

        foreach (Collider2D FireBall in fireBall)
        {
            if (objectpool != null)
            {
                Debug.Log("Attacked by player");
                objectpool.ReturnObject(FireBall.gameObject);
            }
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, ColliderRadi);
    }
    // Collision and Trigger Functions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MovingObj"))
        {
            OriginalParent = Playertransform.parent;
            Playertransform.SetParent(collision.transform);
        }
        if (collision.gameObject.CompareTag("PushableBox"))
        {
            Debug.Log("Pushable bOX deteced");
            animator.SetTrigger("Push");
        }
        if (collision.gameObject.CompareTag("HoverOrb"))
        {
            Destroy(HoverOrb);
            StartCoroutine(HoverAnimationDelay(HoverAniDelay));
            DisableMovemovements();
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingObj"))
        {
            Playertransform.SetParent(OriginalParent);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health Playerhealth = GetComponent<Health>();
        if (collision.gameObject.CompareTag("Traps"))
        {
            
            if (Playerhealth != null)
            {
                Playerhealth.health(25);
            }

        }

        if (collision.gameObject.CompareTag("BossTrigger"))
        {
            Debug.Log("collider detected");
            TriggerRender.sprite = Glow;
            BossAnimator.SetTrigger("Enabled");
            BossAnimator.SetBool("BossEnabled", true);
            BossSlider.SetActive(true);
        }

        if (collision.gameObject.CompareTag("HealthUp"))
        {
            Health PlayerHealth = GetComponent<Health>();
            if(Playerhealth != null) 
            {
              PlayerHealth.ResetHealth(300f);
            }
        }
    }
  
    // Coroutine/ IeNumnerator functions

    IEnumerator HoverAnimationDelay(float HoverAniDelay)
    {
        animator.SetTrigger("SummonWard");
        HoverStatus = true;
        HoverPrefab.SetActive(true);

        yield return new WaitForSeconds(HoverAniDelay);
        Debug.Log("Set true Movements");

        EnableMovemoments();
    }

    // Bool value Setters
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider2D.bounds.center, BoxCollider2D.bounds.size, 0f, Vector2.down, 0.3f, Grounded);

        return hit.collider != null;
    }
    public void EnableMovemoments()
    {
        Debug.Log("Move enabled");
        CanMove = true;
    }

    public void DisableMovemovements()
    {
        CanMove = false;
    }
    public void PlayRunSfx()
    {
        AudioManager.instance.PlaySFXLoop("Run");
    }

    public void MoveLeft()
    {
        float horizontal = Input.GetAxis("Horizontal");
        horizontal = -1;
    }

    public void MoveRight()
    {
        float horizontal = Input.GetAxis("Horizontal");
        horizontal = 1;
    }
    public void StopMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        horizontal = 0;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            AudioManager.instance.PlaySFX("Jump");
            Rb.velocity = new Vector2(Rb.velocity.x, Jumpforce);
            state = State.Jump_Jugger;
        }
    }

    public void Attack()
    {
        if (state != State.Attack_Jugger && IsGrounded())
        {
            state = State.Attack_Jugger;
            animator.SetTrigger("Attack");
        }
    }

    public void Slash()
    {
        if (HoverStatus == true && Time.time >= LastdSlashAttackTime + SlashAttackCoolDown)
        {
            SlashAttack();
            LastdSlashAttackTime = Time.time;
        }
    }


}
