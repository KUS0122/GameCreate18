using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    [SerializeField]
    InputAction spaceKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spaceKey.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var input=spaceKey.IsPressed();
        Debug.Log(input);
    }
}
