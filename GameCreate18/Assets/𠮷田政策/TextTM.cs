using UnityEngine;
using System.Collections;

public class TextTM : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private float displayTime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Update());
    }

    // Update is called once per frame
    IEnumerator Update()
    {
        yield return new WaitForSeconds(displayTime);
        text.SetActive(false);
    }
}
