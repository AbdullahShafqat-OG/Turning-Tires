using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(GameManager game);

    public abstract void UpdateState(GameManager game);
    public abstract void ExitState(GameManager game);

    public abstract void OnGUI(GameManager game);
}
