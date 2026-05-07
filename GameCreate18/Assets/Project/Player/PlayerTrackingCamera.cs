using System;
using UnityEngine;

public class PlayerTrackingCamera : MonoBehaviour
{
    [Tooltip("追跡対象となるプレイヤーオブジェクトのTransform")]
    [SerializeField] private Transform playerTransform;

    private Vector3 _offset; 

    private void Start()
    {
        // 1. 初期状態のカメラ位置をオフセット（相対距離）として保存 
        _offset = transform.position;
    }
    // 2. すべての Update 処理が完了した後に実行されるLateUpdateを使用 
    private void LateUpdate()
    {
        if (playerTransform == null) return;
        // 3. プレイヤーの位置にオフセットを加えてカメラの位置を更新 
        transform.position = playerTransform.position + _offset;
        // 4. ターゲットを常に注視する 
        transform.LookAt(playerTransform);
    }
}
