using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// This class controls the movement of the player
/// </summary>
public class PlayerMovement : MonoBehaviour {

    Vector2 move;

    [SerializeField]
    private CharacterController playerController;
    private float moveSpeedmax = 20f;
    private float moveSpeed = 20f;
    [SerializeField]
    private Transform playerPerspectiveTransform;
    
    public float gravity = -9.81f;
    public float jumpHeight;

    [SerializeField]
    private Transform groundCheck;
    private float groundDistance = 0.1f;
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private Animator animator;

    Vector3 velocity;
    bool isGrounded;

    private bool knockedBack = false;

    /// <summary>
    /// in update the player is moved and the animator parameters are set
    /// </summary>
    void Update(){
        //creates a sphere in which it will check if it is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //checking if the player is on the ground and if they have a negative vertical velocity
        //it will then set the velocity to -2 so that it doesnt continue to increase forever
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        // moves the player
        Vector3 movePlayer = transform.right * move.x + transform.forward * move.y;
        playerController.Move(movePlayer * moveSpeed * Time.deltaTime);

        //applies the vertical velocity to the player whilst ensuring gravity is ever present
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

        //animator.SetFloat("Velocity", Input.GetAxis("Vertical"));

        if(isGrounded == true)
        {
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }

        animator.SetFloat("Velocity X", move.x * 20f);

        animator.SetFloat("Velocity Z", move.y * 20f);

    }

    /// <summary>
    /// when the jump button is pressed, the player will be given a positive y velocity to move up,
    /// and the animation will be set to play
    /// </summary>
    void OnJump(){
        if (isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsGrounded", false);
        }
    }

    /// <summary>
    /// when left stick is moved, the value of the movement is stored in a Vector2
    /// </summary>
    void OnMove(InputValue value){
        if (!knockedBack) {
            move = value.Get<Vector2>();
        }
        
    }

    public IEnumerator KnockBack(Vector2 knock, float duration) {
        knockedBack = true;
        move = new Vector2(knock.x, knock.y);
        OnJump();

        yield return new WaitForSeconds(2);

        move = new Vector2(0, 0);
        knockedBack = false;
    }

    /// <summary>
    /// when called this function will slow the player's speed for a certain amount of time
    /// </summary>
    public IEnumerator SlowMovement(float slowness, float time) {
        moveSpeed = moveSpeedmax * slowness;
        yield return new WaitForSeconds(time);
        moveSpeed = moveSpeedmax;
    }

    public void SlowMovement(float slowness) {
        moveSpeed = moveSpeedmax * slowness;
    }
}