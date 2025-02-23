using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroi a bola de fogo após um tempo
    }

    private void Update()
    {
        if(this.gameObject.name == "BolaDeFogo")
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime); // Move para baixo
        }
        else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime); // Move para cima
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroi a bola de fogo ao acertar o jogador
        }
        else if (other.CompareTag("Ground")) // Se bater no chão, destrói
        {
            Destroy(gameObject);
        }
    }
}
