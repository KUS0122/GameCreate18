using UnityEngine;
using UnityEngine.UI;

public class Setumeichange : MonoBehaviour
{
    public GameObject Setumei;
    void Start()
    {

    }

    void Update()
    {

    }
    public void ShowImage()
    {
        Setumei.SetActive(true);
    }
    public void HideImage()
    {
        Setumei.SetActive(false);
    }
}
