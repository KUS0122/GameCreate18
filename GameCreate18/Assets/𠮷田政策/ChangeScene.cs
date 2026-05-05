using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;

    void Start()
    {

    }
    void Update()
    {

    }
    public void Load()
    {
        SceneManager.LoadScene(SceneName);
    }
}
