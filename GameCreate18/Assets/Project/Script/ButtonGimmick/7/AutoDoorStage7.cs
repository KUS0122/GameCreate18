using UnityEngine;

public class AutoDoorStage7 : MonoBehaviour
{
    private Animator animator;

    private bool canOpen = false;

    private static readonly int OpenHash =
        Animator.StringToHash("Open");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCanOpen(bool value)
    {
        canOpen = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        if (!canOpen)
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