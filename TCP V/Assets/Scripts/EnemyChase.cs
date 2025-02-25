using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 3f; // Velocidade do inimigo
    public int damage = 10; // Dano ao tocar no jogador
    public bool canTakeDamage = false; // S� pode levar dano quando TRUE
    public float rotationSpeed = 5f; // Velocidade da rota��o

    private Transform player;
    private Rigidbody2D rb;
    public Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            // Movimenta��o em dire��o ao jogador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // Rotacionar para olhar para o jogador
            RotateTowardsPlayer();
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Quando leva dano, perde a vulnerabilidade
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            Debug.Log("Inimigo levou dano!");
            canTakeDamage = false; // Bloqueia dano at� ser reativado
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                anim.SetTrigger("atk");
                playerHealth.TakeDamage(damage);
                Debug.Log("Jogador levou dano do inimigo!");
            }
        }
    }
}
