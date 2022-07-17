using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDataScript : MonoBehaviour
{
    [SerializeField] CoinData coinData;
    public int amountOfCoin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        amountOfCoin = coinData.amountOfCoin;
    }
    public void LoadCoin()
    {
        //CoinData data = SaveSystem.LoadCoin();

        //coinData
    }
}
