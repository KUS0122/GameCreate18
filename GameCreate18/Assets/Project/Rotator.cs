using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    Vector3 rotatorSpeed = new Vector3(15, 30, 45);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotatorSpeed * Time.deltaTime);
    }
}
