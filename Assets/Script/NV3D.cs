using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class NV3D : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CharacterController characterController;
    private float horizontal,vertical;
    [SerializeField] private float speed = 3f;//hướng di chuyển
    private Vector3 movement;// tọa độ của player
    [SerializeField]
    Animator animator;
    private bool isAttack;
    AnimatorStateInfo stateInfo;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    public AudioSource audio;
    public AudioClip clip;
    public enum CharacterState
    {
        Normal, Attack,Die
    }
    //trạng thái hiện tại của player
    public CharacterState currentState;
    public DameZone damezone;
    void Start()
    {
        currentHp = maxHp;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()//Chạy theo từng Frame
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (!isAttack&&Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            ChangeState(CharacterState.Attack);
            audio.PlayOneShot(clip);
        }
        Debug.Log("trang thai hien tai: " + currentState);
        if (currentState == CharacterState.Normal)
        {
            Calculater();
        }
        else if (currentState == CharacterState.Attack)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1 )
            {
                EndAttack();
            }
            //return;
        }
        characterController.Move(movement);
    }
    void Calculater()
    {
        if (isAttack) return;
       
        movement.Set(horizontal, 0, vertical);
        movement.Normalize(); // Chuẩn hóa Vector
        movement = Quaternion.Euler(0, -45, 0) * movement;
        movement *= speed * Time.deltaTime;

        animator.SetFloat("Run", movement.magnitude);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            //xoay theo góc di chuyển của nhân vật
        }
    }
    //Hàm thay đổi trạng thái hiện tại của player
    public void ChangeState(CharacterState newState)
    {
        //clear cache giúp clear hết các state
        //Chuyển qua state mới
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
            case CharacterState.Die:
                animator.SetTrigger("Dead");break;
        }
        currentState = newState;
    }
    public void TakeDame(int dame)
    {
        currentHp -= dame;
        currentHp = Mathf.Max(0, currentHp);
        if (currentHp <= 0)
        {
            ChangeState(CharacterState.Die);
        }
    }
    private void OnDisable()
    {
        horizontal = 0;
        vertical = 0;
        isAttack = false;
    }
    public void EndAttack()//Event
    {
        if(stateInfo.normalizedTime >= 1)
        {
            Debug.Log("Đã chạy Event");
            ChangeState(CharacterState.Normal);
            isAttack = false;
        }
    }
    public void BeginDame()
    {
        damezone.BeginDame();
    }
    public void EndDame()
    {
        damezone.EndDame();
    }
}
