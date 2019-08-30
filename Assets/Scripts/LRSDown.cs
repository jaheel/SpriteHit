using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LRSDown : MonoBehaviour
{
    private GameObject Left;
    private GameObject Right;
    private GameObject Space;

    private void Awake()
    {
        Left = GameObject.Find("Left");
        Right = GameObject.Find("Right");
        Space = GameObject.Find("Space");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MonitorMove();
    }

    private void FixedUpdate()
    {
        Left.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        Right.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        Space.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    private void MonitorMove()
    {
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            Left.GetComponent<Image>().color =new Color32(255,255,255,70);
        }
        else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            Right.GetComponent<Image>().color = new Color32(255, 255, 255, 70);
        }


        if(Input.GetKey(KeyCode.Space))
        {
            Space.GetComponent<Image>().color = new Color32(255, 255, 255, 70);
        }
    }
}
