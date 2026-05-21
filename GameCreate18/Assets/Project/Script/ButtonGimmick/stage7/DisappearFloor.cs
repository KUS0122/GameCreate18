using UnityEngine;
using System.Collections;

public class DisappearFloor : MonoBehaviour
{
    [Header("óéâ∫ëŒè€")]
    [SerializeField]
    private Transform targetFloor;

    [Header("óéâ∫ë¨ìx")]
    [SerializeField]
    private float fallSpeed = 10f;

    [Header("óéâ∫ãóó£")]
    [SerializeField]
    private float fallDistance = 20f;

    private bool isFalling = false;

    private Vector3 startPosition;

    private void Awake()
    {
        if (targetFloor == null)
        {
            targetFloor = transform.parent;
        }

        startPosition = targetFloor.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovementStage7 player =
            other.GetComponent<PlayerMovementStage7>();

        if (player == null)
            return;

        if (isFalling)
            return;

        StartCoroutine(FallFloor());
    }

    private IEnumerator FallFloor()
    {
        isFalling = true;

        Vector3 targetPosition =
            startPosition +
            Vector3.down * fallDistance;

        while (
            Vector3.Distance(
                targetFloor.position,
                targetPosition
            ) > 0.05f
        )
        {
            targetFloor.position =
                Vector3.MoveTowards(
                    targetFloor.position,
                    targetPosition,
                    fallSpeed * Time.deltaTime
                );

            yield return null;
        }
    }

    public void ResetFloor()
    {
        StopAllCoroutines();

        targetFloor.position = startPosition;

        isFalling = false;
    }
}