using UnityEngine;

public class PlayerTrackingCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    private Transform playerTransform;

    [Header("Camera Position")]
    [SerializeField]
    private Vector3 offset =
        new Vector3(0f, 5f, -7f);

    [Header("Collision")]
    [SerializeField]
    private LayerMask wallLayer;

    [SerializeField]
    private float collisionRadius = 0.3f;

    [SerializeField]
    private float wallPadding = 0.2f;

    private void LateUpdate()
    {
        if (playerTransform == null)
            return;

        // プレイヤー中心
        Vector3 targetCenter =
            playerTransform.position + Vector3.up * 1.5f;

        // プレイヤー後方位置
        Vector3 desiredPosition =
            targetCenter +
            playerTransform.rotation * offset;

        // 壁判定方向
        Vector3 direction =
            desiredPosition - targetCenter;

        float distance = direction.magnitude;

        direction.Normalize();

        Vector3 finalPosition = desiredPosition;

        // 壁めり込み防止
        if (Physics.SphereCast(
                targetCenter,
                collisionRadius,
                direction,
                out RaycastHit hit,
                distance,
                wallLayer))
        {
            finalPosition =
                hit.point -
                direction * wallPadding;
        }

        // 即座に移動
        transform.position = finalPosition;

        // プレイヤー前方を見る
        Vector3 lookTarget =
            playerTransform.position +
            playerTransform.forward * 3f +
            Vector3.up * 1.5f;

        Quaternion targetRotation =
            Quaternion.LookRotation(
                lookTarget - transform.position
            );

        // 即座に回転
        transform.rotation = targetRotation;
    }
}