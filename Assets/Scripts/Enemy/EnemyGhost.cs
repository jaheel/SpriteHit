using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    public     float   en_speed            =            5f;//初始速度
    public     float   en_speed_max        =           15f;//最高速度
    public     float   en_inc              =           .2f;//Ghost每秒加速
    public     bool    facing_right        =          true;//人物方向朝向
    public     int     isHPChange          =             0;//血量减少次数

    public     GameObject    player;
    private Animator Anim;

    public bool canMove=true;//暂停的时候无法移动
    
    
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("UserPlayer");
        Anim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (canMove)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 0 && !facing_right) Flip();

            if (GetComponent<Rigidbody2D>().velocity.x < 0 && facing_right) Flip();

            en_speed += en_inc * Time.deltaTime;

            en_speed = Mathf.Min(en_speed, en_speed_max);

            Vector3 test_vect = player.transform.position - transform.position;
            test_vect.Normalize();
            GetComponent<Rigidbody2D>().AddForce(test_vect * en_speed);
        }

    }

    private void FixedUpdate()
    {
        
    }

    //人物方向转换
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facing_right = !facing_right;
    }

    //碰撞检测，碰撞一次，血量减少5点
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="UserPlayer")
        {
            isHPChange++;
            Anim.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UserPlayer")
        {
            Anim.SetBool("IsAttacking", false);
        }
    }
}
