using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject TelaInicio;
    public GameObject opcoes;
    public GameObject creditos;
    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            TelaInicio.SetActive(false);
        }
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void AbrirOpcoes()
    {
        opcoes.SetActive(true);    
    }

    public void FecharOpcoes()
    {
        opcoes.SetActive(false);
    }

    public void FecharJogo()
    {
        Application.Quit();
    }

    public void AbrirCreditos()
    {
        creditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        creditos.SetActive(false);
    }
}
