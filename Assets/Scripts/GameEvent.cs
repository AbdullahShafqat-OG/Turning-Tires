using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public const string COIN_COLLECTED = "COIN_COLLECTED";
    public const string COIN_PULLED = "COIN_PULLED";
    public const string GAME_OVER = "GAME_OVER";
    public const string OBSTACLE_DESTROYED = "OBSTACLE_DESTROYED";

    // UI Events
    public const string UI_UPDATE_COINS = "UI_UPDATE_COINS";
    public const string UI_UPDATE_SCORE = "UI_UPDATE_SCORE";
    public const string UI_UPDATE_DESTRUCTION = "UI_UPDATE_DESTRUCTION";
}
