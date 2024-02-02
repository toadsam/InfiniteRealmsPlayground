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

    private const float RAY_DISTANCE = 2f;
    private float maxSlopeAngle;
    private RaycastHit slopeHit;
    private int groundLayer = 1 << LayerMask.NameToLayer("Ground"); //땅만 레이어를 체크한다.
    
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
    
    
    public bool IsOnSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out slopeHit, RAY_DISTANCE, groundLayer))
        {
            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle != 0f && angle < maxSlopeAngle;  //maxSlopeAngle의 정체를 아직도 모르곘다 계속해서 고민해보자
        }
        return false;
    }
}
