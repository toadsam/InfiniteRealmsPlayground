using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent((typeof(Player)))]

public class PlayerController : MonoBehaviour
{
    protected Animator animator;
    protected const float DEFAULT_CONVERT_MOVESPEED = 3f;
    protected const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    protected Player player;
    public Vector3 direction { get; private set; }

    public Rigidbody rigidbody;

    protected const float CONVERT_UNIT_VALUE = 0.01f;
    void Start()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        player = GetComponent<Player>();
        //rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
        //Debug.Log("열어봐");
    }

    protected void Move()
    {
        float currentMoveSpeed = player.MoveSpeed * CONVERT_UNIT_VALUE;
        float animationPlaySpeed = DEFAULT_ANIMATION_PLAYSPEED + GetAnimationSyncWithMovement(currentMoveSpeed);
        LookAt();
        rigidbody.velocity = direction * currentMoveSpeed + Vector3.up * rigidbody.velocity.y;
        animator.SetFloat("Velocity" , animationPlaySpeed);
        //Debug.Log(direction);
        // Debug.Log(Vector3.up);
        // Debug.Log(rigidbody.velocity.y);
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        if (direction == Vector3.zero)
        {
            return -DEFAULT_ANIMATION_PLAYSPEED;
        }

        return (changedMoveSpeed - DEFAULT_CONVERT_MOVESPEED) * 0.1f;
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
