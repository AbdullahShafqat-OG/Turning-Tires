using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GamePauseState : GameBaseState
{
    private float _countdownDuration = 3f;
    private float _currentTime;
    private Timer timer;

    private bool _switch;

    public override void EnterState(GameManager game)
    {
        game.scoreManager.ToggleScorer();

        Time.timeScale = 0.0f;
        game.uiController.Open(game.uiController.pauseStatePanel);

        _switch = false;

        _currentTime = _countdownDuration;
    }

    public override void UpdateState(GameManager game)
    {
        if (!game.uiController.Paused && timer == null && _currentTime >= 0)
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        if (_switch)
            game.SwitchState(game.playState);
    }

    public override void ExitState(GameManager game)
    {
        Time.timeScale = 1.0f;
        game.uiController.Close(game.uiController.pauseStatePanel);
    }

    public override void OnGUI(GameManager game)
    {
        
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _currentTime--;

        if (_currentTime >= 0)
        {
            Debug.Log(_currentTime);
        }
        else
        {
            // Countdown finished, stop and dispose of the timer
            timer.Stop();
            timer.Dispose();
            timer = null;

            Debug.Log("GO");
            _switch = true;
        }
    }
}
