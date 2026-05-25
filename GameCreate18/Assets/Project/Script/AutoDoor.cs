using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    // ドアのAnimator
    private Animator animator;

    // Animatorの"Open"パラメータHash
    private static readonly int OpenHash =
        Animator.StringToHash("Open");

    [Header("設定")]

    // trueならTriggerで自動開閉
    [SerializeField]
    private bool TriggerOpen = true;

    [Header("Door Collider")]

    // 通行止め用Collider
    [SerializeField]
    private BoxCollider doorBlockCollider;

    [Header("Collider Center")]

    // ドアが閉じている時のCollider位置
    [SerializeField]
    private Vector3 closedColliderCenter;

    // ドアが開いている時のCollider位置
    [SerializeField]
    private Vector3 openedColliderCenter;

    private void Awake()
    {
        // Animator取得
        animator = GetComponent<Animator>();

        // 開始時のCollider位置を保存
        // 閉じた時に戻す用
        if (doorBlockCollider != null)
        {
            closedColliderCenter =
                doorBlockCollider.center;
        }
    }
    public void OpenDoor()
    {
        // アニメーション再生
        if (animator != null)
        {
            animator.SetBool(OpenHash, true);
        }

        // Colliderを横へ移動
        MoveColliderOpen();
    }

    /// ドアを閉じる
    public void CloseDoor()
    {
        // アニメーション再生
        if (animator != null)
        {
            animator.SetBool(OpenHash, false);
        }

        // Colliderを元位置へ戻す
        MoveColliderClosed();
    }

    /// 開いた時のCollider位置へ移動
    private void MoveColliderOpen()
    {
        if (doorBlockCollider != null)
        {
            doorBlockCollider.center =
                openedColliderCenter;
        }
    }

    /// 閉じた時のCollider位置へ戻す
    private void MoveColliderClosed()
    {
        if (doorBlockCollider != null)
        {
            doorBlockCollider.center =
                closedColliderCenter;
        }
    }

    /// Triggerに入った時
    private void OnTriggerEnter(Collider other)
    {
        // 自動開閉OFFなら何もしない
        if (!TriggerOpen)
        {
            return;
        }

        // PlayerMovement取得
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        // Player以外なら無視
        if (player == null)
        {
            return;
        }

        // ドアを開く
        OpenDoor();
    }

    /// Triggerから出た時
    private void OnTriggerExit(Collider other)
    {
        // 自動開閉OFFなら何もしない
        if (!TriggerOpen)
        {
            return;
        }

        // PlayerMovement取得
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        // Player以外なら無視
        if (player == null)
        {
            return;
        }

        // ドアを閉じる
        CloseDoor();
    }
}