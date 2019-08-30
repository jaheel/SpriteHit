using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    public Slider slider;
    private Text BloodShowText;
    private Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
        //找到血量显示的文本
        BloodShowText = transform.Find("BloodShow").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("UserPlayer").GetComponent<Player>();
    }
    void Start()
    {
        //添加slider的监听器，实现血量文本的动态显示
        slider.onValueChanged.AddListener(onSliderBloodText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        SliderHPShow();//实时检测血量的动态变化
    }

    //HP血量动态显示实验
    //定时将HP的值减少
    public void SliderHPShow()
    {
        if (slider.value > player.GetCurrentBlood())
        {
            slider.value = Mathf.Max(slider.value - 0.2f, player.GetCurrentBlood());
        }
        if(player.GetCurrentBlood()<=0)
        {
            slider.value = 0;
        }
    } 

    //血量显示
    void onSliderBloodText(float value)
    {
        BloodShowText.text = value.ToString() + "/100";
    }
}
