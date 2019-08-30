using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject PauseButton;
    
    private int CurrentScene_int;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        CurrentScene_int = SceneManager.GetActiveScene().name[5]-49+1;//获取当前游戏场景
    }

    public void Retry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentScene_int);
    }

    /// <summary>
    /// 点击了pause按钮
    /// </summary>
    public void Pause()
    {
        anim.SetBool("isPause", true);
        PauseButton.SetActive(false);
        GameObject.FindGameObjectWithTag("UserPlayer").GetComponent<Player>().canMove = false;
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyGhost>().canMove = false;
    }

    /// <summary>
    /// 点击了继续按钮
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void Home()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    /// <summary>
    /// pause动画播放完调用
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        PauseButton.SetActive(true);
        //设置人物能够运动
        GameObject.FindGameObjectWithTag("UserPlayer").GetComponent<Player>().canMove = true;
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyGhost>().canMove = true;
    }
}
