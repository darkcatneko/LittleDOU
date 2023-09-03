using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

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
    [SerializeField]
    float bulletSpeed_ = 10;
    [SerializeField]
    VisualEffect flash_;
    [SerializeField]
    GameObject buttCollider_;
    [SerializeField]
    GameObject hitBoxOBJ_;
    [SerializeField]
    AudioSource audio_;
    [SerializeField]
    AudioClip oof_;

    [SerializeField]
    float HeatGage;
    [SerializeField]
    bool isOverHeat_ = false;

    [SerializeField]
    public float HealthPoint = 100;
    [SerializeField]
    GameObject bean_;
    [SerializeField]
    GameObject beeeeeen_;

    [SerializeField]
    GameObject onHitParticle_;

    [SerializeField]
    public Image Health;
    [SerializeField]
    public Image Heat;

    void Update()
    {
        playerMovement();
        backFireTest();
        mouseRotation();
        buttSwitch();
        heatCheck();
        updateUI();
    }
    void heatCheck()
    {
        if (HeatGage == 700) { OnOverHeat(); }
        if (HeatGage == 0)
        {
            isOverHeat_ = false;
            bean_.SetActive(true);
            beeeeeen_.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
    }
    void playerMovement()
    {
        if (isShooting_&&!isOverHeat_)
        {
            HeatGage = Mathf.Clamp(HeatGage - 25f * Time.deltaTime, 0, 700);
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            playerRigidbody_.velocity = new Vector3(horizontalMovement, 0, verticalMovement) * speed_;
        }
        if (isOverHeat_)
        {
            HeatGage = Mathf.Clamp(HeatGage - 100f * Time.deltaTime, 0, 700);
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            playerRigidbody_.velocity = new Vector3(horizontalMovement, 0, verticalMovement) * speed_*5;
        }
    }
    void OnOverHeat()
    {
        isOverHeat_ = true; 
        OnStopFire();
        bean_.SetActive(false);
        beeeeeen_.SetActive(true);
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
        if (!isOverHeat_)
        {
            fireOnce(100);
        }
    }

    void fireOnce(float force)
    {
        shoot();
        // 获取角色的面朝方向
        Vector3 facingDirection = playerGameObject_.transform.forward; // 假设角色是朝向x轴正方向的

        // 计算相反的方向
        Vector3 oppositeDirection = -facingDirection;

        // 施加相反方向的力
        playerRigidbody_.AddForce(oppositeDirection * force, ForceMode.Impulse);
        var playerDir = playerRigidbody_.velocity.normalized;
        var playerClampVelocity = playerDir * 200;
        if (playerRigidbody_.velocity.magnitude >= 200)
        {
            playerRigidbody_.velocity = playerClampVelocity;
        }
        flash_.Play();
        HeatGage = Mathf.Clamp(HeatGage + 10f, 0, 700);
    }
    void OnFireContinuous()
    {
        if (!isOverHeat_)
        {
            isShooting_ = false;
            fireContinuous();
        }      
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
            await UniTask.Delay(50);

            // 在这里执行你的方法
            fireOnce(5);
        }
    }

    void shoot()
    {
        var bulletOBJ = bulletObjectClass_.GetGameObject(bulletPrefab_, shootPoint_.transform.position, Quaternion.identity);
        bulletOBJ.GetComponent<PoolObjectDestroyer>().StartDestroyTimer(5f);
        // 計算子彈需要旋轉的角度以匹配玩家的面向
        Vector3 playerDirection = playerGameObject_.transform.position - shootPoint_.transform.position;
        Quaternion bulletRotation = Quaternion.LookRotation(playerDirection.normalized);
        Vector3 bulletDirection = -playerDirection.normalized;
        // 設置子彈的旋轉
        bulletOBJ.transform.rotation = bulletRotation;

        // 獲取子彈的Rigidbody（如果有的話）
        Rigidbody bulletRigidbody = bulletOBJ.GetComponent<Rigidbody>();

        // 設置子彈的速度，使其沿著子彈的前方（朝向玩家的方向）移動
        bulletRigidbody.velocity = bulletDirection * bulletSpeed_;
    }
    void buttSwitch()
    {
        if (playerRigidbody_.velocity.magnitude > 20&&!isOverHeat_)
        {
            buttCollider_.SetActive(true);
            hitBoxOBJ_.SetActive(false);
        }
        else
        {
            buttCollider_.SetActive(false);
            hitBoxOBJ_.SetActive(true);
        }
    }

    public void GetHurt()
    {
        audio_.PlayOneShot(oof_);
        HealthPoint -= 5;
        Instantiate(onHitParticle_, transform.position, Quaternion.identity);
    }
    void updateUI()
    {
        Health.fillAmount = HealthPoint / 100f;
        Heat.fillAmount = HeatGage / 700f;
    }
}
