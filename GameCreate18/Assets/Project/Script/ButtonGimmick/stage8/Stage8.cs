using UnityEngine;

public class Stage8 : StageBase
{
    [Header("References")]
    [SerializeField]
    private PlayerMovement player;

    [Header("Player Speed")]
    [SerializeField] private float normalPlayerSpeed = 5.0f;
    [SerializeField] private float stage8PlayerSpeed = 8.0f;

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

    private bool _speedSet = false;

    private void Start()
    {
        if (movingFloor != null)
        {
            _startPosition = movingFloor.position;
        }
    }

    private void Update()
    {
        if (player == null || movingFloor == null)
            return;

        // Button押す前は何もしない
        if (!player.IsButtonActive)
            return;

        // Button押した瞬間だけ速度UP
        if (!_speedSet)
        {
            player.SetMoveSpeed(stage8PlayerSpeed);
            _speedSet = true;
        }

        if (wayPoints == null || wayPoints.Length == 0)
            return;

        MoveFloor();
    }

    private void MoveFloor()
    {
        Transform targetPoint = wayPoints[_currentIndex];

        if (targetPoint == null)
            return;

        movingFloor.position =
            Vector3.MoveTowards(
                movingFloor.position,
                targetPoint.position,
                moveSpeed * Time.deltaTime
            );

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
    }

    public override void ResetStage()
    {
        if (movingFloor != null)
        {
            movingFloor.position = _startPosition;
        }

        _currentIndex = 0;

        // リスポーン時に再度Button待機
        _speedSet = false;

        // プレイヤー速度を通常へ戻す
        if (player != null)
        {
            player.SetMoveSpeed(normalPlayerSpeed);
        }
    }
}