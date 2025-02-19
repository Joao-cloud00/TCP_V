using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instruções : MonoBehaviour
{
    public GameObject text;
    public bool PassarDeFase;
    public int level;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PassarDeFase == true)
        {
            SceneManager.LoadScene(level);
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
