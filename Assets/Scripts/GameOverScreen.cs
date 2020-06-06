using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameoverWindow;
    void Start()
    {
        gameoverWindow.SetActive(false);
    }

    void Update()
    {
        if(Group.isGameOver == true)
        {
            gameoverWindow.SetActive(true);
        }
    }
}
