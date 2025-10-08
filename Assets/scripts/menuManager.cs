using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject optionsInstruction;

    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void backtoMenuFunc()
    {
        optionsInstruction.SetActive(false);
           mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    public void toInstructions()
    {
        mainMenuPanel.SetActive(false);
        optionsInstruction.SetActive(true);
    }

}

