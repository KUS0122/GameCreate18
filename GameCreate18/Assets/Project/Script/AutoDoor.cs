using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator animator;

    private static readonly int OpenHash =
        Animator.StringToHash("Open");

    private void Reset()
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