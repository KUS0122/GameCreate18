using UnityEngine;
using UnityEngine.UI;

public class Setumeichange : MonoBehaviour
{
    public GameObject Setumei;
    public GameObject SetteiON;
    public GameObject SetteiOFF;
    void Start()
    {
        SetteiON.SetActive(false);
        SetteiOFF.SetActive(true);
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
    public void OnClickSetteiOFF()
    {
        SetteiOFF.SetActive(false);
        SetteiON.SetActive(true);
    }
    public void OnClickSetteiON()
    {
        SetteiON.SetActive(false);
        SetteiOFF.SetActive(true);
    }
}
