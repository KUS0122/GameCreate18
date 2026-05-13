using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool IsActive = false;

    private Collider _collider;
    private Renderer[] _renderers;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderers =
            GetComponentsInChildren<Renderer>();
    }

    public void OnPressed(PlayerMovement player)
    {
        // 既に押されていたら無視
        if (!IsActive) return;

        Debug.Log("Button Pressed");

        IsActive = false;

        // 非表示
        _collider.enabled = false; 
        foreach (var r in _renderers)
        {
            r.enabled = false;
        }

        // プレイヤーへ通知
        player.SetButtonActive(true);
    }

    public void ResetButton()
    {
        Debug.Log("Button Reset");

        IsActive = true;

        // 再表示
        _collider.enabled = true;
        foreach (var r in _renderers)
        {
            r.enabled = true;
        }
    }
}