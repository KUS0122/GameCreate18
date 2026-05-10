using UnityEngine.SceneManagement;
using UnityEngine;

public class SetteiPanels : MonoBehaviour
{
    public GameObject SetteiPanel;
    public GameObject MenuPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackSettei();
        }
    }
    public void OpenSettei()
    {
        MenuPanel.SetActive(false);
        SetteiPanel.SetActive(true);
    }
    public void BackSettei()
    {
        SetteiPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
}
