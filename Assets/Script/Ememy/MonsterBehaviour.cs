using Gamemanager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    
    void Update()
    {
        if ((this.gameObject.transform.position - GameManager.Instance.PlayerObject.transform.position).magnitude>110)
        {
            this.gameObject.GetComponent<PoolObjectDestroyer>().ReleaseThisObject();
            GameManager.Instance.GameEventPack.Send(new EnemyDestroyCommand());
        }
    }
}
