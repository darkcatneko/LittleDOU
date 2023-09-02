using Gamemanager;
using UnityEngine;
using UniRx;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    ObjectPoolClass enemyObjectPool_;
    [SerializeField]
    int monsterCount_;
    [SerializeField]
    GameObject enemyPrefab_;

    private void Start()
    {
        GameManager.Instance.GameEventPack.OnEnemyDestroy.Subscribe(enemyDied);
    }
    private void Update()
    {
        spawnMonster();
    }
    void spawnMonster()
    {
        if (monsterCount_<=100)
        {
            monsterCount_++;
            var spawnedMonster = enemyObjectPool_.GetGameObject(enemyPrefab_, getRandomSpawnPosition(), Quaternion.identity);//加入物件池
        }
        
    }

    Vector3 getRandomSpawnPosition()
    {
        var minRadius = 50;
        var maxRadius = 100;
        // 生成随机的半径和角度
        float radius = Random.Range(minRadius, maxRadius);
        float angle = Random.Range(0f, 360f); // 角度范围从0到360度

        // 将极坐标转换为笛卡尔坐标，使用游戏对象的位置作为圆心
        float x = GameManager.Instance.PlayerObject.transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float z = GameManager.Instance.PlayerObject.transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

        // 创建随机点的Vector3坐标，保持Y坐标为0以确保在XZ平面上
        Vector3 randomPoint = new Vector3(x, 1.5f, z);

        return randomPoint;
    }

    void enemyDied(EnemyDestroyCommand cmd)
    {
        monsterCount_--;
    }
}
