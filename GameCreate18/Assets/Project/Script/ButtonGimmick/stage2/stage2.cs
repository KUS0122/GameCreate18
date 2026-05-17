using UnityEngine;

public class Stage2 : StageBase
{
    [Header("References")]
    [SerializeField]
    private PlayerMovement player;

    [Header("Moving Floor")]
    [SerializeField]
    private Transform movingFloor;

    [Header("Waypoints")]
    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float reachDistance = 0.05f;

    private int _currentIndex = 0;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition =
            movingFloor.position;
    }

    private void Update()
    {
        if (player == null ||
            movingFloor == null)
        {
            return;
        }

        // Button OFF
        if (!player.IsButtonActive)
        {
            StopStage();
            return;
        }

        if (wayPoints == null ||
            wayPoints.Length == 0)
        {
            return;
        }

        MoveFloor();
    }

    private void MoveFloor()
    {
        Transform targetPoint =
            wayPoints[_currentIndex];

        if (targetPoint == null)
            return;

        movingFloor.position =
            Vector3.MoveTowards(
                movingFloor.position,
                targetPoint.position,
                moveSpeed * Time.deltaTime
            );

        // “ž’…”»’è
        if (Vector3.Distance(
                movingFloor.position,
                targetPoint.position)
            <= reachDistance)
        {
            _currentIndex++;

            if (_currentIndex >= wayPoints.Length)
            {
                _currentIndex = 0;
            }
        }
    }

    public override void StopStage()
    {
        // ‰½‚à‚µ‚È‚¢
    }

    public override void ResetStage()
    {
        movingFloor.position =
            _startPosition;

        _currentIndex = 0;
    }
}