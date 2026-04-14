using UnityEngine;
using TMPro;

public class PickupVeiw : MonoBehaviour
{
    [SerializeField]
    GameObject clearObject;

    TextMeshProUGUI countText;

    int maxCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
        maxCount=GameObject.FindGameObjectsWithTag("pickup").Length;
        SetCountText(0);
    }

    public void SetCountText(int currentCount)
    {
        countText.text = $"Pickup:{currentCount}/{maxCount}";
        clearObject.SetActive(currentCount>=maxCount);
    }

    // Update is called once per frame
  
}
