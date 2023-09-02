using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerRigidbody_;
    [SerializeField]
    float speed_;
    [SerializeField]
    GameObject playerGameObject_;
    [SerializeField]
    bool isShooting_;
    [SerializeField]
    GameObject shootPoint_;
    [SerializeField]
    GameObject bulletPrefab_;
    [SerializeField]
    ObjectPoolClass bulletObjectClass_;

    void Update()
    {
        playerMovement();
        backFireTest();
        mouseRotation();
    }
    private void FixedUpdate()
    {
    }
    void playerMovement()
    {
        if (isShooting_)
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            playerRigidbody_.velocity = new Vector3(horizontalMovement, 0, verticalMovement) * speed_;
        }
    }

    void backFireTest()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerRigidbody_.AddForce(Vector3.forward * 100, ForceMode.Impulse);
        }
    }
    void mouseRotation()
    {
        // 获取屏幕中心位置
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // 计算从屏幕中心到鼠标位置的向量
        Vector3 direction = Input.mousePosition - screenCenter;

        // 使用Mathf.Atan2计算向量的角度
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        // 将计算出的角度应用到角色的旋转
        playerGameObject_.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
    void OnFireOnce()
    {
        fireOnce(100);
    }
       
    void fireOnce(float force)
    {
            // 获取角色的面朝方向
            Vector3 facingDirection = playerGameObject_.transform.forward; // 假设角色是朝向x轴正方向的

            // 计算相反的方向
            Vector3 oppositeDirection = -facingDirection;

            // 施加相反方向的力
            playerRigidbody_.AddForce(oppositeDirection * force, ForceMode.Impulse);
        
    }
    void OnFireContinuous()
    {        
        isShooting_ = false;
        fireContinuous();
    }
    void backFireContinuous()
    {
        // 获取角色的面朝方向
        Vector3 facingDirection = playerGameObject_.transform.forward; // 假设角色是朝向x轴正方向的

        // 计算相反的方向
        Vector3 oppositeDirection = -facingDirection;

        // 施加相反方向的力
        playerRigidbody_.AddForce(oppositeDirection * 15, ForceMode.Acceleration);
    }
    void OnStopFire()
    {
        isShooting_ = true;
    }

    private async UniTaskVoid fireContinuous()
    {
        while (!isShooting_)
        {
            // 等待3帧
            await UniTask.Delay(300);

            // 在这里执行你的方法
            fireOnce(5);
        }
    }

    void shoot()
    {
        
    }
}
