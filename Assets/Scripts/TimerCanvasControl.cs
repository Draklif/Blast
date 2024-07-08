using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCanvasControl : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Text pointsText;
    [SerializeField] float timerRemaining;
    [SerializeField] GameObject gameOverGO;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Mientras el juego no haya terminado, mantiene el timer
        if (!player.GetComponent<PlayerController>().isGameOver)
        {
            if (timerRemaining > 0)
            {
                timerRemaining -= Time.deltaTime;
            }
            else
            {
                timerRemaining = 0;
                GameOver();
            }
            int minutes = Mathf.FloorToInt(timerRemaining / 60);
            int seconds = Mathf.FloorToInt(timerRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void GameOver()
    {
        // Funcionamiento similar a la pausa, pero oculta menú de pausa, bloquea funciones y actualiza los puntos
        player.GetComponent<PlayerController>().isGameOver = true;
        PauseCanvasControl pauseControl = gameObject.GetComponent<PauseCanvasControl>();
        pauseControl.TogglePause(true);
        pauseControl.pauseGO.SetActive(false);
        gameOverGO.SetActive(true);
        pointsText.text = player.GetComponent<CanvasController>().actualScore + " puntos";
    }
}
