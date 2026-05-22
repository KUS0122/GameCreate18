using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool _isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        // 既に有効なら無視
        if (_isActivated)
            return;

        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        // Respawn地点更新
        player.UpdateRespawnPoint(
            transform.position
        );

        _isActivated = true;

        //Debug.Log("Checkpoint Updated");
    }
}