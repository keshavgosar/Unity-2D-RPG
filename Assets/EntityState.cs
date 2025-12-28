using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        // whenever the state is changed the enter will be called!

        Debug.Log(stateName + "is the new state! Enter Called!");
    }

    public virtual void Update()
    {
        // Logic of the state will be here!

        Debug.Log(stateName + "is the new state! Update Called!");
    }

    public virtual void Exit()
    {
        // when we exit the state!

        Debug.Log(stateName + "is the new state! Exit Called!");
    }
}
