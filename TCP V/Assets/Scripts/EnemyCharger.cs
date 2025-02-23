using UnityEngine;
using System.Collections;

public class EnemyCharge : MonoBehaviour
{
    public float rotationSpeed = 200f; // Velocidade de rotação para olhar o jogador
    public float chargeSpeed = 10f; // Velocidade da investida
    public float chargeDuration = 0.5f; // Duração da investida
    private Transform player;
    private Rigidbody2D rb;
    private bool isCharging = false;
    private bool canTakeDamage = true;
    public int damage = 10;// ?? Controla se pode receber dano

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnemyBehavior());
    }

    IEnumerator EnemyBehavior()
    {
        while (true)
        {
            // 1?? Olha para o jogador por 3 segundos
            float followTime = 3f;
            while (followTime > 0)
            {
                RotateTowardsPlayer();
                followTime -= Time.deltaTime;
                yield return null;
            }

            // 2?? Ativa a investida e permite dano
            isCharging = true;
            canTakeDamage = true; // ?? Reseta a capacidade de levar dano
            rb.velocity = transform.up * chargeSpeed; // Move para frente
            yield return new WaitForSeconds(chargeDuration);
            rb.velocity = Vector2.zero;
            isCharging = false;

            // 3?? Fica parado por 2 segundos
            yield return new WaitForSeconds(2f);
        }
    }

    void RotateTowardsPlayer()
    {
        if (player == null || isCharging) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            Inimigo enemy = gameObject.GetComponent<Inimigo>();
            Debug.Log("Inimigo levou dano!");
            enemy.currentHealth -= damage;
            Debug.Log(gameObject.name + " tomou " + damage + " de dano. Vida restante: " + enemy.currentHealth);
            canTakeDamage = false; // ?? Agora ele não pode mais levar dano até a próxima investida
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isCharging && other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>(); // Pega o script da vida do jogador
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Jogador levou dano!");
            }

            // ? Cancela a investida e reseta o ciclo
            isCharging = false;
            rb.velocity = Vector2.zero;
        }
    }
}
