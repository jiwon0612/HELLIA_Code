using UnityEngine;

public class JumpTrap : FloorTrap
{
    [Header("JumpSetting")] 
    [SerializeField] private float jumpForce = 10f;


    protected override void FloorEnter(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rigid))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
