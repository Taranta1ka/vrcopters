using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
    public void MapRed()
    {
        SceneManager.LoadScene("MapRed");
    }
    
}
