using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePreState : GameBaseState
{
    public override void EnterState(GameManager game)
    {
        game.spawner.gameObject.SetActive(false);
        game.coinSpawner.gameObject.SetActive(false);
        game.decorationSpawner.gameObject.SetActive(false);
        game.obstacleSpawner.gameObject.SetActive(false);

        game.uiController.playBtn.onClick.AddListener(() => game.SwitchState(game.playState));
        game.uiController.Open(game.uiController.preStatePanel);
    }

    public override void UpdateState(GameManager game)
    {
        
    }

    public override void ExitState(GameManager game)
    {
        game.uiController.Close(game.uiController.preStatePanel);
    }

    public override void OnGUI(GameManager game)
    {
        //int sizeX = 100, sizeY = 20;
        //float posX = Camera.main.pixelWidth / 2 - sizeX / 4;
        //float posY = Camera.main.pixelHeight / 2 - sizeY / 2;

        //if (GUI.Button(new Rect(posX, posY, sizeX, sizeY), "PLAY"))
        //{
        //    game.SwitchState(game.playState);
        //}
    }
}
