using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField]
    InputAction moveInput;
    [SerializeField]
    InputAction jumpInput;
    [SerializeField]
    float moveSpeed=1.0f;
    [SerializeField]
    UnityEvent<int> pickupCountChaged;
    bool isGround=true;
    int pickupCount = 0;
    bool isClear = false;

    new Rigidbody rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveInput.Enable();
        jumpInput.Enable();
        rigidbody= GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((jumpInput.WasPerformedThisFrame())&&(isGround==true))
        {
            rigidbody.AddForce(0.0f, 300.0f, 0.0f);
            isGround = false;
        }


    }
private void OnCollisionEnter(Collision collision)
{
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
        /*else if (collision.gameObject.CompareTag("Damage"))
        {
            isClear = true;
        }*/

}

    // Update is called once per frame
    void FixedUpdate()
    {
        var move = moveInput.ReadValue<Vector2>()*moveSpeed;
        rigidbody.AddForce(move.x, 0.0f, move.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            pickupCount++;
            pickupCountChaged.Invoke(pickupCount);
        }
    }
}
