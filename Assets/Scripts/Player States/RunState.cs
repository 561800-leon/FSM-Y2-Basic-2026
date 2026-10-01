
//This is a derived class of State
//This means it inherits fields and methods from State.cs

using UnityEngine;

public class RunState : State
{
    protected float speed;
    protected float rotationSpeed;
    protected Vector2 horizontalSpeed;
    protected SpriteRenderer spriteRenderer;

    public RunState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        speed = 5;
        base.Enter();

        horizontalInput = verticalInput = 0.0f;
        horizontalSpeed.x = 5f;

        spriteRenderer = player.GetComponent<SpriteRenderer>();

        player.animator.Play("Run");

        Debug.Log("entering running state");
    }


    public override void Exit()
    {
        base.Exit();
    }



    public override void Update()
    {

        TestMethod("hello");

        

        ReadInput();

        if (player.interactAction.IsPressed())
        {
            sm.ChangeState(sm.idleState);
        }

        if (player.jumpAction.WasPressedThisFrame() && player.isGrounded)
        {
            sm.ChangeState(sm.jumpState);
        }

        if (player.throwAction.WasPressedThisFrame())
        {
            sm.ChangeState(sm.throwState);
            return;
        }




        if (player.rb.linearVelocityX < 0.1f && player.rb.linearVelocityX > -0.1f)
        {
            sm.ChangeState(sm.idleState);
        }

        Vector2 input = player.moveAction.ReadValue<Vector2>();


        if (input.x > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (input.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        player.rb.linearVelocity = new Vector2(
            input.x * speed,
            player.rb.linearVelocity.y
        );




        UIscript.ui.DrawText("*** This is the running state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move Sprite");
        UIscript.ui.DrawText("E = Idle State");
        UIscript.ui.DrawText("Space = Jump state");



    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided in runstate");

        if( collision.tag == "enemy")
        {
            collision.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit collision in runstate");

        if (collision.tag == "enemy")
        {
            collision.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f);
        }
    }



    public override void FixedUpdate()
    {
    }
}
