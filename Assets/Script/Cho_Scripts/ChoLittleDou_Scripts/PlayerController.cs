using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerRigidbody_;
    [SerializeField]
    float speed_;
    [SerializeField]
    GameObject playerGameObject_;
    

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
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        playerRigidbody_.velocity = new Vector3 (horizontalMovement,0, verticalMovement)*speed_;
    }

    void backFireTest()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerRigidbody_.AddForce(Vector3.forward * 100,ForceMode.Impulse);
        }
    }
    void mouseRotation()
    {
        // 获取屏幕中心位置
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // 计算从屏幕中心到鼠标位置的向量
        Vector3 direction = Input.mousePosition - screenCenter;

        // 使用Mathf.Atan2计算向量的角度
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg+90;
        // 将计算出的角度应用到角色的旋转
        playerGameObject_.transform.rotation = Quaternion.Euler(new Vector3(0,angle, 0));
    }
}
