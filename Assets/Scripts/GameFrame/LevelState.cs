using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    // Coins Info
    private int coins;
    public int targetCoins;
    public int totalStanbyCount;

    // CoinChangeEvent
    public delegate void CoinChangedDelegate(int numOfCoins);
    public event CoinChangedDelegate OnCoinChanged;

    // Level Start and End Event
    public delegate void LevelStandbyEvent();
    public delegate void LevelStartEvent();
    public delegate void LevelEndEvent();
    public event LevelStandbyEvent OnLevelStandby;
    public event LevelStartEvent OnLevelStart;
    public event LevelEndEvent OnLevelEnd;

    public int presentCoins
    {
        get => coins;
        set
        {
            coins = value;
            OnCoinChanged?.Invoke(coins);
        }
    }

    private void Awake()
    {
        coins = 0;
        Time.timeScale = 0f;
    }

    public void Start()
    {
        GameStandby();
        //GameStart();
    }

    public void GameStandby()
    {
        Time.timeScale = 0f;
        OnLevelStandby?.Invoke();
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        OnLevelStart?.Invoke();
    }

    public void GameFinish()
    {
        Time.timeScale = 0f;
        OnLevelEnd?.Invoke();
    }

    public void BindTimerCoroutine(IEnumerator timerCoroutine)
    {
        OnLevelStandby += () => { StartCoroutine(timerCoroutine); };
    }
}
