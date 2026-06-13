using UnityEngine;

public class Rat_MoveState : IState
{
    private RatController rat_Controller;
    public Rat_MoveState(RatController rat_Controller) => this.rat_Controller = rat_Controller;
    public void Enter()
    {
        SelectDirection();
        this.rat_Controller.Ani.SetBool("move", true);
    }
    public void Excute()
    {
        Move();
    }
    public void Exit()
    {
        this.rat_Controller.Ani.SetBool("move", false);
        this.rat_Controller.Rb.linearVelocity =  Vector2.zero;
    }
    public void SelectDirection()
    {
        int index = Random.Range(0, this.rat_Controller.Direction.Length);
        this.rat_Controller.MoveDirection = this.rat_Controller.Direction[index];
    }
    public void Move()
    {
        this.rat_Controller.Rb.linearVelocity = this.rat_Controller.Stats.speed * this.rat_Controller.MoveDirection;
    }
}
