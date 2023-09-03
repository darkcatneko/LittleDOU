using Gamemanager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField]
    float healthPoint = 100f;
    [SerializeField]
    GameObject spark_;
    void Update()
    {
        if ((this.gameObject.transform.position - GameManager.Instance.PlayerObject.transform.position).magnitude>110)
        {
            var destroyer = this.gameObject.GetComponent<PoolObjectDestroyer>();
            destroyer.ReleaseThisObject();
            GameManager.Instance.GameEventPack.Send(new EnemyDestroyCommand());
        }
        if(healthPoint<=0)
        {
            GameManager.Instance.PlusScore();
            Instantiate(spark_,this.gameObject.transform.position,Quaternion.identity);
            var destroyer = this.gameObject.GetComponent<PoolObjectDestroyer>();
            destroyer.ReleaseThisObject();
            GameManager.Instance.GameEventPack.Send(new EnemyDestroyCommand());
            healthPoint=100f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<PoolObjectDestroyer>().ReleaseThisObject();
            healthPoint -= 50f;
        }
        if (other.gameObject.CompareTag("Butt"))
        {            
            healthPoint -= 100f;
        }
        if (other.gameObject.CompareTag("Hitbox"))
        {
            other.GetComponentInParent<PlayerController>().GetHurt();
            var destroyer = this.gameObject.GetComponent<PoolObjectDestroyer>();
            destroyer.ReleaseThisObject();
            GameManager.Instance.GameEventPack.Send(new EnemyDestroyCommand());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<PoolObjectDestroyer>().ReleaseThisObject();
            healthPoint -= 50f;
        }
    }
}
