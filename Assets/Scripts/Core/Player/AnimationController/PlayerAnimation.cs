using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
    private PlayerStats stats;
    private Vector2 moveDirection;
    private Vector2 lastDirection = Vector2.down;
    void Start()
    {
        LoadCommponents();
    }
    void LoadCommponents()
    {
        this.ani = GetComponentInChildren<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
        this.stats = GetComponent<PlayerStats>();
    }
    public void PlayerAnimationDeath() => this.ani.SetTrigger("die");
    public void AnimationController(InputAction.CallbackContext input)
    {
        this.moveDirection = input.ReadValue<Vector2>();
        if (this.stats.health <= 0)
            return;
        if (this.moveDirection != Vector2.zero)
        {
            lastDirection = this.moveDirection.normalized;
            this.ani.SetBool("IsMoving", true);
        }
        else
        {
            this.ani.SetBool("IsMoving", false);
        }

        this.ani.SetFloat("moveX", lastDirection.x);
        this.ani.SetFloat("moveY", lastDirection.y);
    }    
}
