using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        MainGame.isGameStarted = true;
        MainGame.isRubikShuffled = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShuffleRubik()
    {
        MainGame.isRubikShuffled = true;
    }
}
