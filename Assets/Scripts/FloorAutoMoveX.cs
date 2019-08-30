using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAutoMoveX : MonoBehaviour
{
    private Transform transform;
    public float MoveSpeed;//移动速度
    private bool MoveDirection = true;//true代表右移，false代表左移
    public float MoveRadius;//移动半径
    private float x;//获取最初x,y的坐标
    private float Move_X;//移动过程中的坐标x
    // Start is called before the first frame update
    void Start()
    {
        transform =GetComponent<Transform>();
        x = transform.position.x;
        Move_X = x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        AutoMoveX();
    }

    //X轴的自动移动
    private void AutoMoveX()
    {
        if (MoveDirection)
        {
            Move_X = Mathf.Min(transform.position.x + MoveSpeed, x + MoveRadius);
            if(Move_X==(x+MoveRadius))
            {
                MoveDirection = false;
            }
        }
        else
        {
            Move_X = Mathf.Max(transform.position.x - MoveSpeed, x - MoveRadius);
            if (Move_X == (x - MoveRadius))
            {
                MoveDirection = true;
            }
        }
        transform.position = new Vector2(Move_X, transform.position.y);
    }
}
