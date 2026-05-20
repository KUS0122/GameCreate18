//using Unity.VisualScripting;
//using UnityEngine;

//public class stage5 : MonoBehaviour
//{
//    [SerializeField]
//    private PlayerMovement player;
//    [SerializeField]
//    private AutoDoor targetDoor;

//    [SerializeField] private GameObject RotationObject;
//    [SerializeField] private GameObject targetObject;
//    [SerializeField] private Vector3 rotationAmount = new Vector3(0, 90f,0 );
//    [SerializeField] private float Distance = 10f;

//    // Button押下待機
//    private bool _canRotation = true;
//    void Start()
//    {
//        if(player != null)
//        {
//            _canRotation = player.IsButtonActive;
//        }
//    }

//    void Update()
//    {
//        if(player == null)
//        {
//            return;
//        }
//        if(player.IsButtonActive != _canRotation)
//        {
//            RotateTarget();
//            _canRotation = player.IsButtonActive;
//        }
//    }
//    private void RotateTarget()
//    {
//        if(RotationObject != null)
//        {
//            RotationObject.transform.Rotate(rotationAmount);
//            CheckFrontObject();
//        }
//    }
//    private void CheckFrontObject()
//    {
//        if(targetDoor == null)
//        {
//            return;
//        }
//        bool hitsomething = Physics.Raycast(
//            RotationObject.transform.position,
//            RotationObject.transform.forward,
//            out RaycastHit hit, Distance);
//        if(hitsomething && (hit.transform == targetObject.transform || hit.transform.IsChildOf(targetObject.transform)))
//        {
//            targetDoor.OpenDoor();
//        }
//        else
//        {
//            targetDoor.CloseDoor();
//        }
//    }
//    private void OnDrawGizmos()
//    {
//        if (RotationObject != null)
//        {
//            // 線の色を赤に設定
//            Gizmos.color = Color.red;

//            // RotationObjectの位置から、正面（forward）に向かってDistance分の長さの線を引く
//            Gizmos.DrawLine(
//                RotationObject.transform.position,
//                RotationObject.transform.position + RotationObject.transform.forward * Distance
//            );
//        }
//    }
//}
using Unity.VisualScripting;
using UnityEngine;

public class stage5 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private AutoDoor targetDoor;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private GameObject RotationObject;
    [SerializeField] private Vector3 rotationAmount = new Vector3(0, 90f, 0);
    [SerializeField] private float Distance = 10f;

    private bool _canRotation = true;

    // 【デバッグ用】実際にレイが当たった位置を記憶する変数
    private Vector3 _debugHitPosition;
    private bool _debugIsHitting = false;

    void Start()
    {
        if (player != null)
        {
            _canRotation = player.IsButtonActive;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (player.IsButtonActive != _canRotation)
        {
            RotateTarget();
            _canRotation = player.IsButtonActive;
        }
    }

    private void RotateTarget()
    {
        if (RotationObject != null)
        {
            RotationObject.transform.Rotate(rotationAmount, Space.World);
            CheckFrontObject();
        }
    }

    private void CheckFrontObject()
    {
        if (targetDoor == null || targetObject == null) return;

        bool hitsomething = Physics.Raycast(
            RotationObject.transform.position,
            RotationObject.transform.forward,
            out RaycastHit hit, Distance);

        if (hitsomething)
        {
            // デバッグ情報：何かに当たったらその名前をコンソールに表示
            Debug.Log($"【レイキャスト】「{hit.transform.name}」に当たりました！");

            _debugHitPosition = hit.point; // 当たった場所（座標）を記録
            _debugIsHitting = true;

            // 指定したオブジェクト（またはその子）に当たったか
            if (hit.transform == targetObject.transform || hit.transform.IsChildOf(targetObject.transform))
            {
                // 【大成功ログ】
                Debug.Log($"<color=cyan>【判定成功】ターゲット「{targetObject.name}」に正面衝突しました！扉を開きます。</color>");
                targetDoor.OpenDoor();
            }
            else
            {
                Debug.Log($"【判定失敗】何かに当たりましたが、ターゲットではありません。扉を閉じます。");
                targetDoor.CloseDoor();
            }
        }
        else
        {
            // 何にも当たらなかった場合
            Debug.Log("【レイキャスト】射程圏内には何もありませんでした。");
            _debugIsHitting = false;
            targetDoor.CloseDoor();
        }
    }

    // デバッグ用の描画処理
    private void OnDrawGizmos()
    {
        if (RotationObject == null) return;

        // 1. 基本となる赤い線を引く（何も当たっていない時の射程距離）
        Gizmos.color = Color.red;
        Vector3 endPoint = RotationObject.transform.position + RotationObject.transform.forward * Distance;
        Gizmos.DrawLine(RotationObject.transform.position, endPoint);

        // 2. もしゲーム中に「何かに当たっている」なら、当たった場所に黄色い球と緑の線を表示する
        if (Application.isPlaying && _debugIsHitting)
        {
            // 発射地点から当たった場所までを「緑色」の線にする
            Gizmos.color = Color.green;
            Gizmos.DrawLine(RotationObject.transform.position, _debugHitPosition);

            // 当たったピンポイントの場所に「黄色い球体」を表示する
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_debugHitPosition, 0.2f);
        }
    }
}