using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void FecharGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
