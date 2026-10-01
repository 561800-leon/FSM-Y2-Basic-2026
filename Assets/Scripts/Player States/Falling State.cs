using UnityEngine;

public class FallingState : State
{
    public FallingState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering falling state");
        player.animator.Play("Fall");
    }

    public override void Exit()
    {
        Debug.Log("Exiting falling state");
    }

    public override void Update()
    {
        ReadInput();

        
        Vector2 input = player.moveAction.ReadValue<Vector2>();

        player.rb.linearVelocity = new Vector2(
            input.x * 3f,
            player.rb.linearVelocity.y
        );

        //player.rb.linearVelocityX = input.x * 3;

        UIscript.ui.DrawText("*** This is the falling state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move");
    }

    public override void FixedUpdate()
    {
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f)
            {
                sm.ChangeState(sm.runState);
            }
            else
            {
                sm.ChangeState(sm.idleState);
            }
        }
    }
}
