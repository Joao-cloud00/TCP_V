using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalaMiniBoss : MonoBehaviour
{
    public GameObject Porta;
    public bool Sair;
    public GameObject Luz;
    public GameObject MiniBoss;
    public TMP_Text Lista;
    private void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Sair == false)
        {
            Porta.SetActive(true);
            Luz.SetActive(true);
            MiniBoss.SetActive(true);
        }
        if(Sair == true)
        {
            Lista.fontStyle = FontStyles.Strikethrough;
            Porta.SetActive(false);
            Luz.SetActive(false);
        }
    }
}
