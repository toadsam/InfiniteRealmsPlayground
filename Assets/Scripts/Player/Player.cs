using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[Header("캐릭터 능력치")]
    public float MaxHp { get { return maxHP; } }
    public float CurentHp { get { return currentHP; } }
    public float Armor { get {return armor;} }
    public float MoveSpeed {get {return moveSpeed;}}
    public float DashCount { get { return dashCount; } }

    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float armor;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float dashCount;

    public void OnUpdateStart(float maxHP, float currentHP, float armor, float moveSpeed, float dashCount )
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.armor = armor;
        this.moveSpeed = moveSpeed;
        this.dashCount = dashCount;
    }
    


    
}
