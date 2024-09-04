
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform BossTrans, PlayerTransform, StartPoint ,ThrowPoint;
    public Transform[] ElectricPoints;
    public Rigidbody2D BossRb;
    public Animator BossAnimation;
    public GameObject Shield, GranadePrefb, Electricprefab, FIREPOOL;

    public SpriteRenderer Trigger;
    public Sprite Glow;

    public float MoveSpeed = 3f, MoveDelay = 3f, ShieldActivate = 5f, ShieldActivateDelay = 5f , ShieldDeActivate = 5f;
    public float ThrowForce = 5f;
    public float ElectricAttackDelay = 3f;
    public float GranadeDelay = 3f, AttackDelay = 1f;

    public static Boss Instance;
    void Start()
    {
        Instance = this;
        BossRb = GetComponent<Rigidbody2D>();
        BossAnimation = GetComponent<Animator>();
        BossAnimation.SetTrigger("Disabled");
        Trigger = GetComponent<SpriteRenderer>();

         StartCoroutine(ActivateShield());

    }
    private void Update()
    {
        if(BossDefeat() == true)
        {
            FIREPOOL.SetActive(false);
        }
    }

    // Update is called once per frame

    public IEnumerator ActivateShield()
    {
        while (true) 
        {
            yield return new WaitUntil(() => BossAnimation.GetBool("BossEnabled") == true);

            Debug.Log("Shield INActive");
            ShieldDisabled();
                yield return new WaitForSeconds(ShieldActivate);

            Debug.Log("Shield Active");
            ShieldEnable();
                yield return new WaitForSeconds(ShieldDeActivate);

            StartCoroutine(ElectricAttack());

            yield return new WaitForSeconds(ShieldDeActivate);
        }
    }

    IEnumerator AttackLoop()
    {

            BossAnimation.SetTrigger("Grenade");
            yield return new WaitForSeconds(AttackDelay);
            GranadePrefb.SetActive(true);
    }

    IEnumerator ElectricAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            BossAnimation.SetTrigger("Lightning");
        int RandomNumber = UnityEngine.Random.Range(0, ElectricPoints.Length);
        Transform RandomPoints = ElectricPoints[RandomNumber];
        Instantiate(Electricprefab, RandomPoints.position, RandomPoints.rotation);
        Electricprefab.SetActive(true);

        yield return new WaitForSeconds(ElectricAttackDelay);
}
    }

    void GranadeThrow()
    {
        GameObject Granade = Instantiate(GranadePrefb, ThrowPoint.position, ThrowPoint.rotation);
        Rigidbody2D GranadeRb = Granade.GetComponent<Rigidbody2D>();
        Vector2 Directrionthrow = -ThrowPoint.right;
        GranadeRb.AddForce(Directrionthrow * ThrowForce, ForceMode2D.Impulse);
    }
    void ShieldEnable()
    {
        Shield.SetActive(true);

        StartCoroutine(AttackLoop());
    }
    void ShieldDisabled()
    {
        Shield.SetActive(false);
        GranadePrefb.SetActive(false);
    }

    public bool BossDefeat()
    {
        return BossAnimation.GetBool("Dead"); ;
    }
}
