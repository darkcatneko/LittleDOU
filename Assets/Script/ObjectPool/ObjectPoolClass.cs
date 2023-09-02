using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPoolClass : MonoBehaviour
{
    private ObjectPool<GameObject> pool_;
    public GameObject Prefab { get; private set; }
    void Awake()
    {
        pool_ = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }
    GameObject OnCreatePooledObject()
    {
        return Instantiate(Prefab);
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }
    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Prefab = prefab;
        GameObject obj = pool_.Get();
        Transform transform = obj.transform;
        transform.position = position;
        transform.rotation = rotation;
        var destroyer = obj.GetComponent<PoolObjectDestroyer>();
        destroyer.Pool = this;
        return obj;
    }

    public void ReleaseGameObject(GameObject obj)
    {
        pool_.Release(obj);
    }
}
