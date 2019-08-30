using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonOnMouse : MonoBehaviour
{
    private Text text;
    private EventTrigger ET;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        ET = gameObject.GetComponent<EventTrigger>();
        //实例化委托列表
        ET.triggers = new List<EventTrigger.Entry>();
        //注册事件
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        //实例化eventID
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerExit.eventID = EventTriggerType.PointerExit;
        //实例化callback
        entryPointerEnter.callback = new EventTrigger.TriggerEvent();
        entryPointerExit.callback = new EventTrigger.TriggerEvent();
        //设置事件
        UnityAction<BaseEventData> pointerEnterCB = new UnityAction<BaseEventData>(OnPointerEnter);
        UnityAction<BaseEventData> pointerExitCB = new UnityAction<BaseEventData>(OnPointerExit);
        //绑定事件
        entryPointerEnter.callback.AddListener(pointerEnterCB);
        entryPointerExit.callback.AddListener(pointerExitCB);
        //添加到委托列表
        ET.triggers.Add(entryPointerEnter);
        ET.triggers.Add(entryPointerExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(BaseEventData baseEventData)
    {
        text.fontSize += 10;
        text.color = new Color(0, 0, 100);
    }
    public void OnPointerExit(BaseEventData baseEventData)
    {
        text.fontSize -= 10;
        text.color = new Color(255, 0, 0);
    }
}
