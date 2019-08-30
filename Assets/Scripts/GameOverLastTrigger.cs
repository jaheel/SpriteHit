using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLastTrigger : MonoBehaviour
{
    public GameObject WinAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinAnimation.SetActive(true);
        GameObject.FindGameObjectWithTag("UserPlayer").GetComponent<Player>().canMove = false;
    }
}
