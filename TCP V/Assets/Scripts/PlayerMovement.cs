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

        // Armazena a dire��o do movimento
        movement = new Vector2(horizontal, vertical).normalized;

        UpdateRotation();
    }

    void FixedUpdate()
    {
        // Movimenta��o do personagem
        rb.velocity = movement * speed;
    }
    void UpdateRotation()
    {
        if (movement != Vector2.zero)
        {
            // Calcula o �ngulo desejado, ajustando para o personagem come�ar olhando para cima
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;

            // Converte o �ngulo em uma rota��o
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Suaviza a rota��o em dire��o ao �ngulo desejado
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ao colidir, pare o movimento e garanta que a rota��o n�o seja atualizada
        rb.velocity = Vector2.zero;
        movement = Vector2.zero;
    }
}
