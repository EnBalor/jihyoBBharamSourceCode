using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPool : MonoBehaviour
{
    private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> PoolDictionary;
    private Dictionary<string, int> PoolIndexDictionary;

    private GameObject obj;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        PoolIndexDictionary = new Dictionary<string, int>();

        for (int i = 0; i < pools.Count; ++i)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; ++j)
            {
                obj = Instantiate(pools[i].prefab, this.transform);

                obj.SetActive(false);

                queue.Enqueue(obj);
            }

            PoolDictionary.Add(pools[i].tag, queue);
            PoolIndexDictionary.Add(pools[i].tag, i);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        obj = PoolDictionary[tag].Peek().gameObject;

        if (!obj.activeInHierarchy)
        {
            obj = PoolDictionary[tag].Dequeue();
            obj.SetActive(true);
        }
        else
            obj = Instantiate(pools[PoolIndexDictionary[tag]].prefab, this.transform);

        PoolDictionary[tag].Enqueue(obj);

        return obj;
    }
}
