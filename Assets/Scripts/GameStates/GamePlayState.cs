using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePlayState : GameBaseState
{
    public override void EnterState(GameManager game)
    {
        game.spawner.gameObject.SetActive(true);
        game.coinSpawner.gameObject.SetActive(true);
        game.decorationSpawner.gameObject.SetActive(true);
        game.obstacleSpawner.gameObject.SetActive(true);

        game.uiController.Open(game.uiController.playStatePanel);
    }

    public override void UpdateState(GameManager game)
    {
        //if (!game.carController.alive)
        //    game.SwitchState(game.postState);

        //game.carController.Turn();

        if (!game.carController.Any(c => c.alive))
            game.SwitchState(game.postState);

        foreach (CarController c in game.carController)
            c.Turn();

        game.uiController.scoreTxt.text = "Score: " + game.scoreManager.score.ToString();
        game.uiController.coinsTxt.text = "Coins: " + game.scoreManager.coins.ToString();
        game.uiController.destructionTxt.text = "Destruction: " + game.scoreManager.destruction.ToString();
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
