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