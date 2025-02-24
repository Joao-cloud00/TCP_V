using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 300f; // 5 minutos (300 segundos)
    public TMP_Text timerText; // Referência ao texto do tempo na UI
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        timeLimit -= Time.deltaTime;
        UpdateTimerUI();

        if (timeLimit <= 0)
        {
            GameOver();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLimit / 60);
        int seconds = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Tempo acabou! Derrota!");
        // Aqui você pode chamar uma tela de Game Over ou reiniciar a cena
    }
}
