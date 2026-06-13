using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected IState currentState;
    protected virtual void Start()
    {
        LoadComponents();
    }
    protected virtual void Update()
    {
        if (this.currentState != null)
            this.currentState.Excute();
    }
    protected virtual void ChangeState(IState state)
    {
        if (this.currentState != null )
        {
            if(this.currentState.GetType() == state.GetType()) return;
            this.currentState.Exit();
        }  
        this.currentState = state;
        this.currentState.Enter();
    }    
    protected abstract void LoadComponents();   
}
