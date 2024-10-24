using System;
using System.Collections;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private GameObject GroundCheck;
    [SerializeField] private float MovementSpeed = 1f;
    [SerializeField] private float JumpPower = 5.0f;
    [SerializeField] private Slider jetpackGUI;


    private Rigidbody2D playerRigidBody;

    private float HorizontalMovement;
    private float VerticalMovement;

    private bool isLadder;
    private bool isClimbing;

    private bool PlayerFacingRight = true;
    private bool ZeroFuel = false;

    readonly private int MaxJetpackFuel = 100;
    private int CurrentJetpackFuel = 100;

    public bool BoomerangAcquired = false;
    public bool JetpackAcquired = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        jetpackGUI.value = CurrentJetpackFuel;

    }

    // Update is called once per frame
    private void Update()
    {
        CharacterMovement();
        if(CurrentJetpackFuel == 0 && ZeroFuel == false && JetpackAcquired == true)
        {
            ZeroFuel = true;
            StartCoroutine(JetpackFuelIncrease());
        }

        VerticalMovement = Input.GetAxisRaw("Vertical");
        if (isLadder && Mathf.Abs(VerticalMovement) > 0f)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            playerRigidBody.gravityScale = 0f;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, VerticalMovement * MovementSpeed / 2);
        }
        else
        {
            playerRigidBody.gravityScale = 4f;
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.transform.position, 0.2f, GroundLayer); // Checks to see if there's any overlap between the ground check and ground to detect whether or not the player is grounded
    }

    private void CharacterMovement()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal"); // Gets the horizontal movement of the player

        if (Input.GetButtonDown("Jump") && IsGrounded()) // Checks to see if the player is grounded
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpPower * 3f);
            if (Input.GetButton("Jump") && CurrentJetpackFuel != 0 && JetpackAcquired == true)
            {
                StartCoroutine(JetpackFuelDecrease());
                StartCoroutine(JetpackFlight());
            }
        }
        else if (Input.GetButtonDown("Jump") && IsGrounded() == false && CurrentJetpackFuel != 0 && JetpackAcquired == true)
        {
            StartCoroutine(JetpackFuelDecrease());
            StartCoroutine(JetpackFlight());
        }
        playerRigidBody.velocity = new Vector2(((HorizontalMovement * MovementSpeed) / 2), playerRigidBody.velocity.y); // Sets the players movement velocity whilst ensuring their Y velocity remains the same.
        FlipCharacter(); // Calls the check to see if the player has changed directions 
    }

    private void FlipCharacter()
    {
        if (PlayerFacingRight && HorizontalMovement < 0 || !PlayerFacingRight && HorizontalMovement > 0f) // Checks to see if the player has changed directions
        {
            PlayerFacingRight = !PlayerFacingRight; // Reverts the direction the player is facing within the boolean
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f; // Reverses the transforms local scale
            transform.localScale = localScale; // Applies the reversing to the transform
        }
    }

    IEnumerator JetpackFlight()
    {
        yield return new WaitForSeconds(0.1f);
        while (Input.GetButton("Jump") && IsGrounded() == false && CurrentJetpackFuel != 0)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpPower);
            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine(JetpackFlight());
    }
    IEnumerator JetpackFuelDecrease()
    {
        yield return new WaitForSeconds(0.1f);
        while (Input.GetButton("Jump") && IsGrounded() == false && CurrentJetpackFuel != 0)
        {
            CurrentJetpackFuel -= 1;
            jetpackGUI.value = CurrentJetpackFuel;
            yield return new WaitForSeconds(0.02f);
            if (CurrentJetpackFuel == 1)
            {
                StopCoroutine(JetpackFlight());
                StopCoroutine(JetpackFuelDecrease());
                StartCoroutine(JetpackFuelIncrease());
            }
        }
        StopCoroutine(JetpackFuelDecrease());
        StartCoroutine(JetpackFuelIncrease());
    }

    IEnumerator JetpackFuelIncrease()
    {
        yield return new WaitForSeconds(0.1f);
        ZeroFuel = false;
        while (CurrentJetpackFuel <= MaxJetpackFuel)
        {
            if(Input.GetButton("Jump"))
            {
                break;
            }
            CurrentJetpackFuel += 1;
            jetpackGUI.value = CurrentJetpackFuel;
            yield return new WaitForSeconds(0.02f);
        }
        StopCoroutine(JetpackFuelIncrease());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}

