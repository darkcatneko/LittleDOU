using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectDestroyer : MonoBehaviour
{
    public ObjectPoolClass Pool { get; set; }

    public void ReleaseThisObject()
    {
        if (Pool != null) Pool.ReleaseGameObject(gameObject);
        else { Destroy(gameObject); }
    }
    public void StartDestroyTimer(float time)
    {
        StartCoroutine(DestroyTimer(time));
    }

    IEnumerator DestroyTimer(float time)
    {
        yield return new WaitForSeconds(time);

        if (Pool != null)
        {
            Pool.ReleaseGameObject(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
