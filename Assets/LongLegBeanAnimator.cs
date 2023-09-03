using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongLegBeanAnimator : MonoBehaviour
{
    Animator animator;
    Rigidbody player_rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player_rb = GameManager.Instance.PlayerObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("moveSpeed", player_rb.velocity.magnitude);
        
    }
}
