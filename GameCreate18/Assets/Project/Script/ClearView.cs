using UnityEngine;
using TMPro;

public class ClearView : MonoBehaviour
{
    TextMeshProUGUI ClearText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClearText = GetComponent<TextMeshProUGUI>();
        SetCountText(0);
    }

    public void SetCountText(int currentCount)
    {
        ClearText.text = $"Game Clear";

    }

    // Update is called once per frame

}
