using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePlayState : GameBaseState
{
    private float timer;

    public override void EnterState(GameManager game)
    {
        game.carController.enabled = true;

        game.spawner.gameObject.SetActive(true);
        game.coinSpawner.gameObject.SetActive(true);
        game.decorationSpawner.gameObject.SetActive(true);
        game.obstacleSpawner.gameObject.SetActive(true);

        game.uiController.Open(game.uiController.playStatePanel);

        timer = game.speedDifficultyRampTime;

        game.scoreManager.ToggleScorer();
    }

    public override void UpdateState(GameManager game)
    {
        if (game.uiController.Paused)
            game.SwitchState(game.pauseState);

        if (!game.carController.alive)
            game.SwitchState(game.postState);

        game.carController.Move();
        game.carController.Turn();

        IncreaseDifficulty(game);
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

    private void IncreaseDifficulty(GameManager game)
    {
        if (timer <= 0)
        {
            if (game.obstacleSpawner.SafetyLevel > game.obstacleSpawner.MinSafetyLevel)
            {
                timer = game.obstacleDifficultyRampTime;
                game.obstacleSpawner.SafetyLevel -= game.obstacleDifficultyRamp;
            }
            else
            {
                timer = game.speedDifficultyRampTime;
                game.carController.speed += game.speedDifficultyRamp;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
