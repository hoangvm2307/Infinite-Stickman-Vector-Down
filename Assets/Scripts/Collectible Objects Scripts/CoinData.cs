using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Collectible Objects", menuName = "Coin SO")]
public class CoinData : ScriptableObject
{
    public int amountOfCoin;
    [System.NonSerialized]
    public UnityEvent<int> coinChangeEvent;
    public int tempCoin;
    private void OnEnable()
    {
        if(coinChangeEvent == null)
        {
            coinChangeEvent = new UnityEvent<int>();
        }
    }
    public void IncreaseCoin(int amount)
    {
        amountOfCoin += amount;
        coinChangeEvent.Invoke(amountOfCoin);
    }

    public CoinData(CoinData coin)
    {
        amountOfCoin = coin.amountOfCoin;
    }
    public void LoadCoin()
    {
        CoinDataManager data = SaveSystem.LoadCoin();

        amountOfCoin = data.amountOfCoin;
    }
}
