using UnityEngine;

public class ThrowState : State
{
    private float throwTimer;
    private float throwDuration = 0.5f;

    public ThrowState(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering throw state");

      
        throwTimer = throwDuration;

       
        player.rb.linearVelocity = new Vector2(
            0f,
            player.rb.linearVelocity.y
        );

       
        player.animator.Play("Throw");

       
        ThrowObject();
    }

    public override void Exit()
    {
        Debug.Log("Exiting throw state");
    }

    public override void Update()
    {
        throwTimer -= Time.deltaTime;

        
        if (throwTimer <= 0)
        {
            Vector2 input = player.moveAction.ReadValue<Vector2>();

           
            if (Mathf.Abs(input.x) > 0.1f)
            {
                sm.ChangeState(sm.runState);
            }
            
            else
            {
                sm.ChangeState(sm.idleState);
            }

            return;
        }

        UIscript.ui.DrawText("*** This is the throwing state ***\n");
    }

    public override void FixedUpdate()
    {
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void ThrowObject()
    {
        Debug.Log("Throwing object!");
    }
}
