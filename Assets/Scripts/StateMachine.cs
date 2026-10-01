using UnityEngine;


//Handles initialisation of states, switching of states
//It handles calling Update and FixedUpdate for the currently active state

public class StateMachine
{
    public State currentState, lastState;

    //declare all states here
    public IdleState idleState;
    public JumpState jumpState;
    public RunState runState;
    public FallingState fallingState;
    public ThrowState throwState;




    //constructor
    public StateMachine( PlayerScript player )
    {
        //initialise all states here
        idleState = new IdleState(player, this);
        jumpState = new JumpState(player, this);
        runState = new RunState(player, this);
        fallingState = new FallingState(player, this);
        throwState = new ThrowState(player, this);



    }

    public void Init(State startingState)
    {
        currentState = startingState;
        lastState = null;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();

        lastState = currentState;
        currentState = newState;
        newState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

}
