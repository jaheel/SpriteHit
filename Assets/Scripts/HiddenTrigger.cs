using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTrigger : MonoBehaviour
{
    public GameObject HiddenObject;//隐藏的物体
    private bool ObjectShow = false;//判断物体是否隐藏
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        ObjectShowMethod();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "UserPlayer")
        {
            ObjectShow = !ObjectShow;
        }
    }

    private void ObjectShowMethod()
    {
        if(ObjectShow)
        {
            HiddenObject.SetActive(true);
        }
        else
        {
            HiddenObject.SetActive(false);
        }
    }
}