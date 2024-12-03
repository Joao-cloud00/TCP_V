using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    private Rigidbody2D rb;
    private Vector2 movement;
    public float rotationSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capturar entrada do teclado (WASD) e do joystick (eixos horizontal e vertical) sem delay
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Armazena a direção do movimento
        movement = new Vector2(horizontal, vertical).normalized;

        UpdateRotation();
    }

    void FixedUpdate()
    {
        // Movimentação do personagem
        rb.velocity = movement * speed;
    }
    void UpdateRotation()
    {
        if (movement != Vector2.zero)
        {
            // Calcula o ângulo desejado, ajustando para o personagem começar olhando para cima
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;

            // Converte o ângulo em uma rotação
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Suaviza a rotação em direção ao ângulo desejado
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ao colidir, pare o movimento e garanta que a rotação não seja atualizada
        rb.velocity = Vector2.zero;
        movement = Vector2.zero;
    }
}
