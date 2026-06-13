using UnityEngine;

public class Rat_IdleState : IState
{
    private RatController rat_Controller;
    public Rat_IdleState(RatController rat_Controller) => this.rat_Controller = rat_Controller;
    public void Enter()
    {
        this.rat_Controller.Rb.linearVelocity = Vector2.zero;
    }
    public void Excute()
    {
        
    }
    public void Exit()
    {

    }
}
