using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBegin : MonoBehaviour
{
    private Animator anim;
    private int CurrentLevel_int;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        CurrentLevel_int = SceneManager.GetActiveScene().name[5] - 49 + 1;//获取当前关卡
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginAnimEnd()
    {
        Destroy(GameObject.Find("GameBegin"));
    }
}
