//This is a derived class of State
//This means it inherits fields and methods from State.cs


using UnityEngine;

public class JumpState : State
{
    float rotationSpeed;

    
    public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering jumping state");
        player.animator.Play("Jump");

        player.rb.linearVelocity = new Vector2(
            player.rb.linearVelocity.x,
            7f
        );
    }


    public override void Exit()
    {
        //exit the jump state
    }

    public override void Update()
    {
        ReadInput();

        if (player.interactAction.IsPressed())
        {
            sm.ChangeState(sm.idleState);

        }

        if (player.moveAction.ReadValue<Vector2>().magnitude > 0.1f)
        {
            sm.ChangeState(sm.runState);
        }

        if (player.rb.linearVelocity.y < 0)
        {
            sm.ChangeState(sm.fallingState);
        }

        UIscript.ui.DrawText("*** This is the jumping state ***\n");
        UIscript.ui.DrawText("*** This is the jumping state ***\n");
        UIscript.ui.DrawText("Left/Right arrows = Move State");
        UIscript.ui.DrawText("E = Idle State");


    }

    public override void FixedUpdate()
    {
        //Fixed Update 
    }
}
