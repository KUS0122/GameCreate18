using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float reachDistance = 0.05f;

    private int currentIndex = 0;
    private Vector3 startPosition;
    private bool isActive = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive) return;

        MoveFloor();
    }

    private void MoveFloor()
    {
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        Transform targetPoint = wayPoints[currentIndex];

        if (targetPoint == null)
            return;

        transform.position =
            Vector3.MoveTowards(
                transform.position,
                targetPoint.position,
                moveSpeed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, targetPoint.position)
            <= reachDistance)
        {
            currentIndex++;

            if (currentIndex >= wayPoints.Length)
            {
                currentIndex = 0;
            }
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void ResetFloor()
    {
        isActive = false;
        transform.position = startPosition;
        currentIndex = 0;
    }
}