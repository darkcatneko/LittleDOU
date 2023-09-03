using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanAnimator : MonoBehaviour
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
        if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0)) 
        {
            animator.SetFloat("moveSpeed", 0);
        }
        else
        {
            animator.SetFloat("moveSpeed", player_rb.velocity.magnitude);
        }
        animator.SetFloat("backFireSpeed", player_rb.velocity.magnitude);

        if (Input.GetMouseButtonDown(0)) 
        {
            animator.SetBool("Fire", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Fire", false);
        }
        

    }
}
