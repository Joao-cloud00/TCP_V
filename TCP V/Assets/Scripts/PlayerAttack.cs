using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10; // Dano do ataque
    public Transform attackPoint; // Ponto de ataque
    public Collider2D attackCollider; // Collider do ataque
    public Animator anim;

    private void Start()
    {
        attackCollider.enabled = false; // O Collider do ataque começa desativado
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttack();
            anim.SetTrigger("atk");
        }
    }

    void StartAttack()
    {
        attackCollider.enabled = true; // Ativa o Collider do ataque
        Invoke("EndAttack", 0.1f); // Desativa após um curto período para simular o golpe

    }

    void EndAttack()
    {
        attackCollider.enabled = false; // Desativa o Collider do ataque
   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo")) // Se atingir um inimigo
        {
            Inimigo enemy = other.GetComponent<Inimigo>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}
