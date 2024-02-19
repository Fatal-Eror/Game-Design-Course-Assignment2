using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : SingleTon<GameState>
{
    private int coins;
    public int targetCoins;
    // CoinChangeEvent
    public delegate void CoinChangedDelegate(int numOfCoins);
    public event CoinChangedDelegate OnCoinChanged;

    public int presentCoins
    {
        get => coins;
        set
        {
            coins= value;
            OnCoinChanged?.Invoke(coins);
        }
    }
}
