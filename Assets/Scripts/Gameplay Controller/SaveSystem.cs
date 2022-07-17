using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveCoin(CoinData coin)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/shootcoin.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        CoinDataManager data = new CoinDataManager(coin);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveHighScore(CoinData coin)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/shootcoin.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        CoinDataManager data = new CoinDataManager(coin);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static CoinDataManager LoadCoin()
    {
        string path = Application.persistentDataPath + "/shootcoin.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CoinDataManager data = formatter.Deserialize(stream) as CoinDataManager;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
