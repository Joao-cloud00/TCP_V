using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativador_SFX : MonoBehaviour
{
    //public Collider2D colliderSound;
    public string nome;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Se atingir um inimigo
        {
            AudioManager.instance.StopMusic("intro");
            AudioManager.instance.PlaySFX(nome);
            //Debug.Log("play SFX");
            this.gameObject.SetActive(false);
        }
        
    }
}
