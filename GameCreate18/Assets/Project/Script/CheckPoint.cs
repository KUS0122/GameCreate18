using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        player.UpdateRespawnPoint(
            transform.position
        );

        Debug.Log("Checkpoint Updated");
    }
}