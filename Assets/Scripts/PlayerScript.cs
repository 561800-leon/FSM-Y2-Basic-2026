// player.cs is the Monobehaviour and owns the Unity components
// It passes control to the statemachine

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public bool isGrounded;
    public Animator animator;

    StateMachine sm;

    // Define the actions
    public InputAction moveAction;
    public InputAction crouchAction;
    public InputAction jumpAction;
    public InputAction interactAction;
    public InputAction throwAction;



    private void Start()
    {
        // Pass a reference of this PlayerScript to the StateMachine
        sm = new StateMachine(this);

        
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // This will be the first state to run
        sm.Init(sm.idleState);

        // Initialise the actions
        moveAction = InputSystem.actions.FindAction("Move");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        interactAction = InputSystem.actions.FindAction("Interact");
        jumpAction = InputSystem.actions.FindAction("Jump");
        throwAction = InputSystem.actions.FindAction("Throw");
    }

    private void Update()
    {
        // State handles the player's behaviour
        sm.Update();

        UIscript.ui.DrawText(
            "Current state= " + sm.currentState +
            "  Last state= " + sm.lastState
        );
    }

    private void FixedUpdate()
    {
        // State handles physics behaviour
        sm.FixedUpdate();
    }

    // Collision handling
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Pass collision to the current state
        sm.currentState.OnCollisionEnter2D(collision);

        // Check if the player has landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Player has left the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        sm.currentState.OnTriggerEnter2D(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        sm.currentState.OnTriggerExit2D(collision);
    }
}
