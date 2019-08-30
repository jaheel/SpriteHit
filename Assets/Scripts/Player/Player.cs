using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private      Rigidbody2D                 rig;   //刚体
    private      Animator                   Anim;
    public       Slider        PlayerSliderBlood;//用户血条
    protected    TestMyTrail         PlayerTrail;//用户拖尾
    public AudioClip JumpAudio;//跳跃
    public AudioClip Land;//落地

    //=== 人物运动条件检测 ======================================
    [HideInInspector]
    public     bool       canMove              =    true;//是否能够移动

    // === 初始数值 ======================================
    public     float      MaxSpeed              =       10;//最高速度
    private    float      MinSpeed              =        0;//最低速度
    public     float      MoveSpeed             =        0; //水平移动速度
    public     float      MoveAccelerate        =     0.5f; //水平移动加速度
    private    int        HorizontalDirection   =        1;//水平方向，1代表向右，-1代表向左
    private    float      JumpSpeed             =      10f;//跳跃的垂直速度
    public     float      MaxBlood              =     100f;//最高血量
    private    float      CurrentBlood          =     100f;//当前血量
    


    // === 跳跃条件检测判断 ================================
    public     Transform       CheckPoint;//设置一个跳跃监测点
    public     float           CheckRadius; //跳跃检测半径 
    public     LayerMask       WhatIsGround; //跳跃检测层—————角色与地面的检测
    public     bool            isGround           =      true;//角色是否着地
    public     bool            canPlayGroundAudio =     false;//落地以后才能播放落地音效一次
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    // === 角色攻击状态 ======================================
    private    bool            isAttack         =       false;//攻击状态A
    private    bool            isAttackB        =       false;//攻击状态B

    // === 相关系数 ======================================
    public static GameObject GetGameObject()
    {
        return GameObject.FindGameObjectWithTag("UserPlayer");
    }
    public static Transform GetTransform()
    {
        return GameObject.FindGameObjectWithTag("UserPlayer").transform;
    }
    public static Animator GetAnimator()
    {
        return GameObject.FindGameObjectWithTag("UserPlayer").GetComponent<Animator>();
    }

    private void Awake()
    {
        PlayerTrail = GetComponent<TestMyTrail>();
        rig = GetComponent<Rigidbody2D>();   //获取主角刚体组件
        Anim = gameObject.GetComponent<Animator>();
        PlayerSliderBlood = gameObject.GetComponent<Slider>();
    }

    void Start()
    {

       
    }

    void Update()
    {
        if (canMove)
        {            
            Attack();//角色攻击判断
            Move(); //角色移动判断
            Jump();
        }
    }

    private void FixedUpdate()
    {
        IsGrounded();
        AttackReturnToCommon();//攻击以后状态回复
        TrailShow();
        //BloodDecend();
    }

    //角色方向移动、跳跃判断
    private void Move()
    {
        /* 
         * 角色水平速度计算
         * 如果有按键，就计算加速度，达到峰值及以上，就按最高值计算
        */
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            MoveSpeed = Mathf.Min(MoveSpeed + MoveAccelerate, MaxSpeed);
        }
        else
        {
            MoveSpeed = Mathf.Max(MoveSpeed - MoveAccelerate, MinSpeed);
        }

        //角色方向判断
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            HorizontalDirection = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            HorizontalDirection = -1;
        }

        rig.velocity = new Vector2(HorizontalDirection * MoveSpeed, rig.velocity.y);
        transform.localScale = new Vector2(HorizontalDirection * 1f, 1f);

        


        Anim.SetFloat("speed", Mathf.Abs(rig.velocity.x));
       

    }

    private void Jump()
    {
        if (isGround && canPlayGroundAudio)//落地播放音效
        {
            AudioPlay(Land);
            canPlayGroundAudio = false;
        }
        //角色按下空格实现跳跃
        //禁止二连跳
        //要先判断角色是否在地面上，在地面上可以跳，不在地面上则不能跳
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            isGround = false;
            //rig.velocity = new Vector2(rig.velocity.x, JumpSpeed);
            rig.velocity = Vector2.up * JumpSpeed;
            AudioPlay(JumpAudio);
            canPlayGroundAudio = true;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rig.velocity = Vector2.up * JumpSpeed;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        Anim.SetBool("grounded", isGround);
    }



    /// <summary>
    /// 角色攻击状态判断
    /// </summary>
    private void Attack()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isAttack = true;
            isAttackB = false;
            Anim.SetBool("IsAttack", isAttack);
            Anim.SetBool("IsAttackB", isAttackB);
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            isAttack = false;
            isAttackB = true;
            Anim.SetBool("IsAttack", isAttack);
            Anim.SetBool("IsAttackB", isAttackB);
        }
    }

    /// <summary>
    /// 角色攻击判断逻辑重置
    /// </summary>
    private void AttackReturnToCommon()
    {
        Anim.SetBool("IsAttack", false);
        Anim.SetBool("IsAttackB", false);
    }

    /// <summary>
    /// 判断是否着地,设置变量
    /// 变量: bool isGround
    /// </summary>
    private void IsGrounded()
    {
        isGround = Physics2D.OverlapCircle(CheckPoint.position, CheckRadius, WhatIsGround);
    }


    private void TrailShow()
    {
        if(MoveSpeed>0)
        {
            PlayerTrail.StartTrails();
        }
    }

    public void SetInitializePosition()
    {
        transform.Translate(new Vector2(0,-3.53f),Space.World);

    }

    //每次攻击后血量减少10点
    public void BloodDecend()
    {
        CurrentBlood = Mathf.Max(CurrentBlood - 10, 0);
    }

    //返回当前血量
    public float GetCurrentBlood()
    {
        return CurrentBlood;
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
