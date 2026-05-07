using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 20;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateNewObject();
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : CreateNewObject();

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        if (obj.TryGetComponent(out IPoolable poolable))
        {
            poolable.OnSpawned();
        }

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj.TryGetComponent(out IPoolable poolable))
        {
            poolable.OnDespawned();
        }

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        return obj;
    }
}