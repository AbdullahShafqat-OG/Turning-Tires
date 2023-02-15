using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameBaseState
{
    public override void EnterState(GameManager game)
    {
        game.coinSpawner.gameObject.SetActive(true);
        game.obstacleSpawner.gameObject.SetActive(true);

        game.uiController.Open(game.uiController.playStatePanel);
    }

    public override void UpdateState(GameManager game)
    {
        if (!game.carController.alive)
            game.SwitchState(game.postState);

        game.carController.Turn();

        game.uiController.scoreTxt.text = game.scoreManager.score.ToString();
        game.uiController.coinsTxt.text = game.scoreManager.coins.ToString();
    }

    public override void ExitState(GameManager game)
    {
        game.uiController.Close(game.uiController.playStatePanel);
    }

    public override void OnGUI(GameManager game)
    {
        //GUI.Box(new Rect(10, 10, 100, 20), new GUIContent(game.scoreManager.score.ToString()));
        //GUI.Box(new Rect(10, 40, 100, 20), new GUIContent(game.scoreManager.coins.ToString()));
    }
}
