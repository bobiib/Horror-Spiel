using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject mainMenuUI;

    void Start()
    {
        instructionPanel.SetActive(false);
    }

    public void OnPlayButtonPressed()
    {
        mainMenuUI.SetActive(false);
        instructionPanel.SetActive(true);
        Invoke("StartGame", 5f);
    }

    void StartGame()
    {
        instructionPanel.SetActive(false);
    }
}
