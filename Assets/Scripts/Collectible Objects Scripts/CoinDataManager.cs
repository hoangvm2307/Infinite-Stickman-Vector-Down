using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CoinDataManager  
{
    public int amountOfCoin;
    public CoinDataManager(CoinData coin)
    {
        amountOfCoin = coin.amountOfCoin;
    }
}
