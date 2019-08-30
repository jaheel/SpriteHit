using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNameDynamicChange : MonoBehaviour
{

    private Text GameNameText;
    private int MinSize = 40;//文本最小Size
    private int MaxSize = 60;//文本最大Size
    private bool Size_Grow = true;//文本Size增长方向
    // Start is called before the first frame update
    void Start()
    {
        GameNameText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameNameChange();
    }

    private void GameNameChange()
    {
        if (Size_Grow)
        {
            GameNameText.fontSize = Mathf.Min(GameNameText.fontSize + 1, MaxSize);
            if (GameNameText.fontSize == MaxSize)
                Size_Grow = false;
        }
        else
        {
            GameNameText.fontSize = Mathf.Max(GameNameText.fontSize - 1, MinSize);
            if (GameNameText.fontSize == MinSize)
                Size_Grow = true;
        }
    }
}
