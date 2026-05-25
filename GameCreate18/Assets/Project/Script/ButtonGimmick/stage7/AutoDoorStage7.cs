using UnityEngine;

public class AutoDoorStage7 : MonoBehaviour
{
    private Animator animator;

    private bool canOpen = false;

    private static readonly int OpenHash =
        Animator.StringToHash("Open");

    [SerializeField]
    private BoxCollider doorBlockCollider;

    [SerializeField]
    private Vector3 closedColliderCenter;

    [SerializeField]
    private Vector3 openedColliderCenter;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (doorBlockCollider != null)
        {
            closedColliderCenter =
                doorBlockCollider.center;
        }
    }

    public void SetCanOpen(bool value)
    {
        canOpen = value;

        if (canOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetBool(OpenHash, true);
        }

        MoveColliderOpen();
    }

    private void CloseDoor()
    {
        if (animator != null)
        {
            animator.SetBool(OpenHash, false);
        }

        MoveColliderClosed();
    }

    private void MoveColliderOpen()
    {
        if (doorBlockCollider != null)
        {
            doorBlockCollider.center =
                openedColliderCenter;
        }
    }

    private void MoveColliderClosed()
    {
        if (doorBlockCollider != null)
        {
            doorBlockCollider.center =
                closedColliderCenter;
        }
    }
}