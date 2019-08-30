using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAutoMoveY : MonoBehaviour
{
    public float MoveSpeed;//移动速度
    private bool MoveDirection = true;//true代表上移，false代表下移
    public float MoveRadius;//移动半径
    private float y;//获取最初y的坐标
    private float Move_Y;//移动过程中的坐标y
    // Start is called before the first frame update
    void Start()
    {
        y = transform.position.y;
        Move_Y = y;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        AutoMoveY();
    }

    //Y轴的自动移动
    private void AutoMoveY()
    {
        if (MoveDirection)
        {
            Move_Y = Mathf.Min(transform.position.y + MoveSpeed, y + MoveRadius);
            if (Move_Y == (y + MoveRadius))
            {
                MoveDirection = false;
            }
        }
        else
        {
            Move_Y = Mathf.Max(transform.position.y - MoveSpeed, y - MoveRadius);
            if (Move_Y == (y - MoveRadius))
            {
                MoveDirection = true;
            }
        }
        transform.position = new Vector2(transform.position.x, Move_Y);
    }
}
