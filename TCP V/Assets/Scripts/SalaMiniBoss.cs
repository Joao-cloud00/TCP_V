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
    public string MusicBoss; // boi -> boitata, cabra -> chupa cabra, et -> Et, corposeco -> corpo seco
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
            //AudioManager.instance.StopMusic("background_fase1");
            //AudioManager.instance.PlayMusic("idBoss");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Sair == true)
        {
            Lista.fontStyle = FontStyles.Strikethrough;
            Porta.SetActive(false);
            Luz.SetActive(false);
            //AudioManager.instance.StopMusic("idBoss");
            //AudioManager.instance.PlayMusic("background_fase1");
        }
    }
}
