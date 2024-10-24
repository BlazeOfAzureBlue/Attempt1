using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator AnimController;

    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private GameObject GroundCheck;

    // Start is called before the first frame update
    void Start()
    {
        AnimController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && Input.GetAxisRaw("Horizontal") != 0)
        {
            AnimController.Play("Movement");
        }
        else if (IsGrounded() && Input.GetAxisRaw("Horizontal") == 0)
        {
            AnimController.Play("IdleAnimStart");
        }
        else if(!IsGrounded())
        {
            AnimController.Play("TestAnim");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.transform.position, 0.4f, GroundLayer); // Checks to see if there's any overlap between the ground check and ground to detect whether or not the player is grounded
    }
}
