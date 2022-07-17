using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour
{
    #region singleton
    public static SmartPool instance;
    void MakeInstance()
    {
        if (instance == null) instance = this;
    }
    private void Awake()
    {
        MakeInstance();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    void OnDisable()
    {
        instance = null;
    }
    #endregion
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private void Start()
    {
        //poolDictionary = new Dictionary<string, Queue<GameObject>>();
        //foreach (Pool pool in pools)
        //{
        //    Queue<GameObject> objectPool = new Queue<GameObject>();
        //    for (int i = 0; i < pool.size; i++)
        //    {
        //        GameObject obj = Instantiate(pool.prefab);
        //        obj.SetActive(false);
        //        obj.transform.SetParent(transform);
        //        objectPool.Enqueue(obj);
        //    }
        //    poolDictionary.Add(pool.tag, objectPool);
        //}
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Vector3 direction, Quaternion rotation, int damage, float timeExist)
    {
        if (!poolDictionary.ContainsKey(tag)) return null;
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.TryGetComponent(out BulletController bulletController);
        bulletController.SetDirection(direction);
        bulletController.damage = damage;
        bulletController.timeExist = timeExist;

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public GameObject SpawnObjectFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) return null;
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
