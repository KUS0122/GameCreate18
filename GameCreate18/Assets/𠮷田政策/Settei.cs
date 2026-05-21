using UnityEngine;
using UnityEngine.UI;

public class Settei : MonoBehaviour
{
    public GameObject SetteiPanel;
    public Slider musicSlider;
    public Slider SESlider;
    public AudioSource music;
    public AudioSource SE;

    public AudioClip buttonClick;
    public AudioClip jumpSound;
    public AudioClip sliderSound;
    public Button[] allButtons;
    void Start()
    {
        allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        Debug.Log($"Œ©‚Â‚©‚Á‚½ƒ{ƒ^ƒ“‚Ì”: {allButtons.Length}ŒÂ");
        foreach (Button btn in allButtons)
        {
            if(btn == null)
            {
                continue;
            }
            btn.onClick.AddListener(() => { PlaySE(buttonClick); });
        }

        musicSlider.value = music.volume;
        musicSlider.onValueChanged.AddListener((value) => { music.volume = value; PlaySE(sliderSound); });

        SESlider.value = SE.volume;
        SESlider.onValueChanged.AddListener((value) => { SE.volume = value; PlaySE(sliderSound); });

        SetteiPanel.SetActive(false);

    }
   public void JumpSound()
    {
        if(jumpSound != null && SE != null)
        {
            SE.PlayOneShot(jumpSound);
        }
    }
    private void PlaySE(AudioClip clip)
    {
        if(clip != null && SE != null)
        {
            SE.PlayOneShot(clip);
        }
    }
}
