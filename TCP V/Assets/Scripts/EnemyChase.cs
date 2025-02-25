using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 3f; // Velocidade do inimigo
    public int damage = 10; // Dano ao tocar no jogador
    public bool canTakeDamage = false; // ?? Só pode levar dano quando TRUE

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
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    // ? Quando leva dano, perde a vulnerabilidade
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            Debug.Log("Inimigo levou dano!");
            canTakeDamage = false; // ?? Bloqueia dano até ser reativado
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
