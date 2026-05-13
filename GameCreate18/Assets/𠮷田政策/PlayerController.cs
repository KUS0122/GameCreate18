using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static string gameState = "playing";
    [SerializeField]
    InputAction moveInput;
    Rigidbody rb;
    public int score = 0;
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
        if (collision.gameObject.tag == "Clear")
        {
            Clear();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
    }
    public void Clear()
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
    public void Goal()
    {
        gameState = "gameGoal";
        GameStop();
    }
    void GameStop()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0, 0, 0);
        rb.isKinematic = true;
    }
}
