using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    private Rigidbody2D rb;
    private float horizozntal;
    private float vertical;
    void Start()
    {
        LoadComponents();
    }
    private void FixedUpdate()
    {
        this.rb.linearVelocity = new Vector2(this.horizozntal * this.playerStats.speed, this.vertical * this.playerStats.speed);
    }
#region PC_CONTROL
    public void Move(InputAction.CallbackContext input)
    {
        this.horizozntal = input.ReadValue<Vector2>().x;
        this.vertical = input.ReadValue<Vector2>().y;
    }
#endregion
    void LoadComponents()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.playerStats = GetComponent<PlayerStats>();
    }    
}
