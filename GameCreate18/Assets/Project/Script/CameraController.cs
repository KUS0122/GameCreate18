using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    public float rotateSpeed = 3.0f;

    private float currentVerticalAngle = 30f;  // 初期上下角度
    private float currentHorizontalAngle = 0f; // 初期左右角度
    private float distance = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 offset = transform.position - target.position;
        distance = offset.magnitude;

        Vector3 angles = transform.eulerAngles;
        currentHorizontalAngle = angles.y;
        currentVerticalAngle = angles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 矢印キー入力
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;

        float vertical = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) vertical = -1f;

        // 角度更新
        currentHorizontalAngle += horizontal * rotateSpeed;
        currentVerticalAngle += vertical * rotateSpeed;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, 0f, 90f);

        // 新しい回転をQuaternionで作成
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0);

        // ターゲットから回転方向に距離だけ離れた位置を計算
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = target.position + rotation * negDistance;

        // カメラの位置・回転設定
        transform.position = position;
        transform.LookAt(target.position);
    }

    private void OnTransEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
