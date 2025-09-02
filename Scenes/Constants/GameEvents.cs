using System;

public class GameEvents
{
    public static event Action OnGameStart;
    public static event Action OnGameEnd;

    public static void RaiseStartGame() => OnGameStart?.Invoke();
    public static void RaiseEndGame() => OnGameEnd?.Invoke();
}
