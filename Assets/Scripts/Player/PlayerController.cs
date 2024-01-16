using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent((typeof(Player)))]

public class PlayerController : MonoBehaviour
{
    protected Player player;
    public Vector3 direction { get; private set; }

    public Rigidbody rigidbody;

    protected const float CONVERT_UNIT_VALUE = 0.01f;
    void Start()
    {
        player = GetComponent<Player>();
        //rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
        Debug.Log("열어봐");
    }

    protected void Move()
    {
        float currentMoveSpeed = player.MoveSpeed * CONVERT_UNIT_VALUE;
        LookAt();
        rigidbody.velocity = direction * currentMoveSpeed + Vector3.up * rigidbody.velocity.y;
        // Debug.Log(direction);
        // Debug.Log(Vector3.up);
        // Debug.Log(rigidbody.velocity.y);
    }

    protected void LookAt()
    {
        if(direction != Vector3.zero){
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rigidbody.rotation = targetAngle;
        }}
    }

    private void FixedUpdate()
    {
        Move();
    }
}
