using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instruções : MonoBehaviour
{
    public GameObject text;
    public GameObject desafios;
    public bool PassarDeFase;
    public int level;
    public int contBoss;

    private void Start()
    {
        desafios.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PassarDeFase == true)
        {
            if(level <= 3)
            {
                SceneManager.LoadScene(level);
            }
            if(level == 4 && contBoss == 3)
            {
                SceneManager.LoadScene(level);
                desafios.SetActive(false);
            }
            else
            {
                Debug.Log("Não matou todos os Boss");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            text.SetActive(true);
            if(gameObject.tag == "Fase")
            {
               PassarDeFase = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.SetActive(false);
            PassarDeFase=false;
        }
    }
}
