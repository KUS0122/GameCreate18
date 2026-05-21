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

        if (canOpen)
        {
            animator.SetBool(OpenHash, true);
        }
        else
        {
            animator.SetBool(OpenHash, false);
        }
    }
}