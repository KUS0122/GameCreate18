using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject SetteiPanel;
    public GameObject TitleButton;
    public GameObject ResetButton;
    public GameObject SetteiButton;

    private bool isPaused = false;
    void Start()
    {
        MenuPanel.SetActive(false);
        SetteiPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        MenuPanel.SetActive(true);
        isPaused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        MenuPanel.SetActive(false);
        isPaused = false;
    }

    //public void OpenSettei()
    //{
    //    MenuPanel.SetActive(false);
    //    SetteiPanel.SetActive(true);
    //}
    //public void BackSettei()
    //{
    //    SetteiPanel.SetActive(false);
    //    MenuPanel.SetActive(true);
    //}
    public void RemoveTitle()
    {
        Time.timeScale = 1.0f;
    }

}
