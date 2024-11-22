//Abdullah Shahir bin Zulmajdi 24000112
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LinkedList()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BinaryTree()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void NetworkGraph()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
