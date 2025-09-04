using System;

public class GameEvents
{
    public static event Action OnGameStart;
    public static event Action OnGameEnd;
    public static event Action<int> OnNewEnemyCount;

    public static void RaiseStartGame() => OnGameStart?.Invoke();
    public static void RaiseEndGame() => OnGameEnd?.Invoke();
    public static void RaiseNewEnemyCount(int count) => OnNewEnemyCount?.Invoke(count);
}
