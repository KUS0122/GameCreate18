using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static string gameState = "playing";
    [SerializeField]
    InputAction moveInput;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveInput.Enable();
        rb = GetComponent<Rigidbody>();
        gameState = "playing";
    }

    void Update()
    {
        if (gameState != "playing")
        {
            return;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        var move = moveInput.ReadValue<Vector2>();
        rb.AddForce(move.x, 0.0f, move.y);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }
    public void Goal()
    {
        gameState = "gameclear";
        GameStop();
    }
    public void GameOver()
    {
        gameState = "gameover";
        GameStop();
        //ÉvÉåÉCÉÑÅ[ÇÃColliderÇ…çáÇÌÇπÇÈ
        GetComponent<SphereCollider>().enabled = false;
    }
    void GameStop()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0, 0, 0);
    }
}
