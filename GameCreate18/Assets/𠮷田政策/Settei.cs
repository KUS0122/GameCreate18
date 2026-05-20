using UnityEngine;
using UnityEngine.UI;

public class Settei : MonoBehaviour
{
    public GameObject SetteiPanel;
    public Slider musicSlider;
    public Slider SESlider;
    public AudioSource music;
    public AudioSource SE;
    void Start()
    {
        SetteiPanel.SetActive(false);

        musicSlider.value = music.volume;
        musicSlider.onValueChanged.AddListener((value) => { music.volume = value; });

        SESlider.value = SE.volume;
        SESlider.onValueChanged.AddListener((value) => { SE.volume = value; });
    }
}
