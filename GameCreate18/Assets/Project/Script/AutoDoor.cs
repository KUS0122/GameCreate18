using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator animator;

    private static readonly int OpenHash =
        Animator.StringToHash("Open");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetBool(OpenHash, true);
        }
    }
    public void CloseDoor()
    {
        if (animator != null)
        {
            animator.SetBool(OpenHash, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        animator.SetBool(OpenHash, true);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        animator.SetBool(OpenHash, false);
    }
}