using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    private int CurrentLevel;//当前关卡

    //public static GameManager _instance;
    private Vector2 originPos;//初始位置
    public Player player;
    private Scene scene;
    private GameObject Level_x;
    public GameObject lose;//失败场景动画

    private void Awake()
    {
        //_instance = this;
        //originPos = player.transform.position;//初始位置赋值
    }
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();//获取当前场景从而获得当前场景名称
        CurrentLevel = (int)scene.name[5]-49+1;//char型转int型减49,场景Scene再+1
        Level_x = GameObject.FindGameObjectWithTag("level");
    }

    // Update is called once per frame
    void Update()
    {
        NextLevel();
        UserBloodDecend();
        LoseJudge();
    }

    //下一回合跳转
    public void NextLevel()
    {
        if(Level_x.GetComponentInChildren<GameOverTrigger>().isTriggerToNext)
        {
            SceneManager.LoadScene(CurrentLevel+1);//加载下一回合
        }
    }

    //用户和敌人碰撞以后血量减少
    public void UserBloodDecend()
    {
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyGhost>().isHPChange>0)
        {
            player.BloodDecend();
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyGhost>().isHPChange--;
        }
    }

    public void LoseJudge()
    {
        if(player.GetCurrentBlood()<=0)
        {
            lose.SetActive(true);
            GameObject.FindGameObjectWithTag("UserPlayer").GetComponent<Player>().canMove = false;
        }
    }
}
