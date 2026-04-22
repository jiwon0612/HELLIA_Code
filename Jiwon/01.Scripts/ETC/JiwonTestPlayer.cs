using System;
using UnityEngine;

public class JiwonTestPlayer : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 10;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        playerInput.JumpEvent += Jump;
    }

    private void OnDisable()
    {
        playerInput.JumpEvent -= Jump;
    }

    private void Update()
    {
        _rigid.AddForce(new Vector2(playerInput.InputMovement.x,0) * moveSpeed);
        _rigid.velocity = new Vector2(Mathf.Clamp(_rigid.velocity.x, -moveSpeed, moveSpeed), _rigid.velocity.y);
    }

    public void Jump()
    {
        _rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

}   
