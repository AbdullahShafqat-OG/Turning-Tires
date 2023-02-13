using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnGameOver()
    {
        gameOver = true;
    }

    void OnGUI()
    {
        if (!gameOver) return;

        int sizeX = 100, sizeY = 20;
        float posX = cam.pixelWidth / 2 - sizeX / 4;
        float posY = cam.pixelHeight / 2 - sizeY / 2;

        if (GUI.Button(new Rect(posX, posY, sizeX, sizeY), "Play Again"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
