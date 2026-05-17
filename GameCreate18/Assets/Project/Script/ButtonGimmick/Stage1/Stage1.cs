using UnityEngine;
using System.Collections.Generic;

public class Stage1 : StageBase
{
    [Header("References")]
    [SerializeField]
    private PlayerMovement player;

    [Header("Floor Settings")]
    [SerializeField]
    private GameObject[] floors;

    [SerializeField]
    private float interval = 1.0f;

    private float timer = 0f;

    private int currentIndex = 0;

    private bool isActive = false;

    // 表示中床
    private Queue<GameObject> activeFloors =
        new Queue<GameObject>();

    private void Start()
    {
        // 初期状態
        foreach (GameObject floor in floors)
        {
            SetFloor(floor, false);
        }
    }

    private void Update()
    {
        // Null対策
        if (player == null)
            return;

        // Button ON
        if (player.IsButtonActive &&
            !isActive)
        {
            isActive = true;
        }

        // Button OFF
        if (!player.IsButtonActive &&
            isActive)
        {
            StopStage();
            return;
        }

        if (!isActive)
            return;

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;

            ShowNextFloor();
        }
    }

    private void ShowNextFloor()
    {
        if (currentIndex >= floors.Length)
            return;

        GameObject floor =
            floors[currentIndex];

        SetFloor(floor, true);

        activeFloors.Enqueue(floor);

        if (activeFloors.Count > 5)
        {
            GameObject oldFloor =
                activeFloors.Dequeue();

            SetFloor(oldFloor, false);
        }

        currentIndex++;
    }

    private void SetFloor(
        GameObject floor,
        bool state)
    {
        if (floor == null)
            return;

        Renderer rend =
            floor.GetComponent<Renderer>();

        Collider col =
            floor.GetComponent<Collider>();

        if (rend != null)
        {
            rend.enabled = state;
        }

        if (col != null)
        {
            col.enabled = state;
        }
    }

    public override void StopStage()
    {
        ResetStage();
    }

    public override void ResetStage()
    {
        foreach (GameObject floor in floors)
        {
            SetFloor(floor, false);
        }

        activeFloors.Clear();

        currentIndex = 0;

        timer = 0f;

        isActive = false;

    }
}