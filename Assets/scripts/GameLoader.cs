using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public void loadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
