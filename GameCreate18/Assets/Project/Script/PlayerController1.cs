using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float fallThreshold = -10f;

    [SerializeField] Stage1 stage;
    [SerializeField] GameObject[] buttons; // ←配列に変更

    Vector3 respawnPosition;
    Rigidbody rb;
    bool isGround;
    bool ButtonActive = false;
    public bool IsButtonActive => ButtonActive;

    void Start()
    {
        moveInput.Enable();
        jumpInput.Enable();
        rb = GetComponent<Rigidbody>();

        respawnPosition = transform.position;
    }

    void Update()
    {
        // ジャンプ
        if (jumpInput.WasPerformedThisFrame() && isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }

        // 落下チェック（1つでOK）
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        Vector2 input = moveInput.ReadValue<Vector2>();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * input.y + right * input.x;

        Vector3 velocity = rb.linearVelocity;
        Vector3 targetVelocity = moveDir * moveSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, velocity.y, targetVelocity.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            other.gameObject.SetActive(false);
            ButtonActive = true;
        }

        if (other.CompareTag("RespawnPoint"))
        {
            respawnPosition = other.transform.position;
        }
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = respawnPosition;

        ButtonActive = false;

        // 全ボタン復活
        foreach (var b in buttons)
        {
            b.SetActive(true);
        }

        stage.ResetStage();
    }
}