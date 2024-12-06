using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Transform target;
    [SerializeField] private float radius = 5f;//Ban kinh bao quanh Enemy
    [SerializeField] private float MaxDistance;
    [SerializeField] private Vector3 originalPosition;
    Animator animator;
    public int maxHp,currentHP;
    public DameZone dameZone;
    NPC npc;
    bool isEnemyDead =false;
    void Start()
    {
        currentHP = maxHp;
        originalPosition = transform.position;
        animator = GetComponent<Animator>();
        npc = FindObjectOfType<NPC>();
    }
    public enum EnemyState
    {
        Normal, Attack, Die
    }
    public EnemyState EnemyCurrentstate;
    // Update is called once per frame
    void Update()
    {
        //if(EnemyCurrentstate == EnemyState.Die) { return; }
        var distance = Vector3.Distance(target.position, transform.position);
        float distanceOriganal = Vector3.Distance(originalPosition, transform.position);
        //Tinh khoang cach cua player den enemy
        if(distance <= radius&&distance<=MaxDistance)
        {
            Debug.Log("Con trong pham vi");
            animator.SetBool("Run", true);
            //navMesh.SetDestination(target.position);
            if(distance <= 1.0f)
            {
                Debug.Log("Attack");
                animator.SetBool("Attack", true);
            }
            else if(distance > 1.0f)
            {
                animator.SetBool("Attack", false);
            }
        }
        else if(distance > MaxDistance)
        {
           Debug.Log("Da vuot qua pham vi");
           //navMesh.SetDestination(originalPosition);
            if (transform.position.x == originalPosition.x)
            {
                Debug.Log("Đã vào vị trí cũ");
               animator.SetBool("Run", false);
                
            }
        }
    }
    public void ChangeState(EnemyState newstate) 
    {
        switch (EnemyCurrentstate)
        {
            case EnemyState.Normal:;break;
            case EnemyState.Attack:;break;
            case EnemyState.Die: break;
        }
        switch (newstate)
        {
            case EnemyState.Normal:;break;
            case EnemyState.Attack:; break;
            case EnemyState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 1f);
                break;

        }
        EnemyCurrentstate = newstate;
    }
    public void TakeDame(int dame)
    {
        currentHP -= dame;
        currentHP = Mathf.Max(0, currentHP);
        if(currentHP <= 0&&!isEnemyDead) 
        {
            isEnemyDead = true;
            npc.TienDoNv();
            ChangeState(EnemyState.Die);
        }
        Debug.Log("Hp Enemy: " + currentHP);
    }
    public void BeginDame()
    {
        dameZone.BeginDame();
    }
    public void EndDame()
    {
        dameZone.EndDame();
    }
}
