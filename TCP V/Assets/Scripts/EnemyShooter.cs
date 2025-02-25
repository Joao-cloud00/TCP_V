using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float speed = 2f;
    public float moveLimitLeft;
    public float moveLimitRight;
    public float chaseSpeed = 3f;
    public float fireRate = 1f;
    public GameObject fireballPrefab;
    public Transform firePoint;
    private Transform player;
    public int damageToPlayer = 10;
    private bool isChasing = false;
    private bool isReturning = false;
    private Vector2 startPosition;
    private bool movingRight = true;
    public Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        StartCoroutine(HorizontalMovementPhase()); // Agora começa corretamente
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else if (isReturning)
        {
            ReturnToStart();
        }
    }

    private IEnumerator HorizontalMovementPhase()
    {
        isChasing = false;
        isReturning = false;

        StartCoroutine(ShootFireballs());

        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            MoveHorizontally();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(ShootFireballs());
        StartCoroutine(ChasePhase());
    }

    private IEnumerator ChasePhase()
    {
        isChasing = true;
        yield return null;
    }

    private void MoveHorizontally()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= moveLimitRight)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= moveLimitLeft)
                movingRight = true;
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        isChasing = false;
        isReturning = true;
    }

    private void ReturnToStart()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, startPosition) < 0.1f)
        {
            isReturning = false;
            StartCoroutine(HorizontalMovementPhase());
        }
    }

    private IEnumerator ShootFireballs()
    {
        while (!isChasing && !isReturning)
        {
            Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

            if (this.gameObject.name == "ET")
            {
                AudioManager.instance.PlaySFX("zap");
            }
            else
            {
                AudioManager.instance.PlaySFX("fireball");
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isChasing && collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //anim.SetTrigger("atk");
                playerHealth.TakeDamage(damageToPlayer);
            }
            TakeDamage(); // Faz o inimigo voltar para a posição inicial
        }
    }

}
