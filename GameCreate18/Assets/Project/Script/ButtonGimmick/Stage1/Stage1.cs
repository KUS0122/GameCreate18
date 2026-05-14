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

    /// <summary>
    /// 次の床を表示
    /// </summary>
    private void ShowNextFloor()
    {
        if (currentIndex >= floors.Length)
            return;

        GameObject floor =
            floors[currentIndex];

        // 表示
        SetFloor(floor, true);

        activeFloors.Enqueue(floor);

        // 古い床削除
        if (activeFloors.Count > 3)
        {
            GameObject oldFloor =
                activeFloors.Dequeue();

            SetFloor(oldFloor, false);
        }

        currentIndex++;
    }

    /// <summary>
    /// 表示切替
    /// </summary>
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

    /// <summary>
    /// Button OFF時
    /// </summary>
    public override void StopStage()
    {
        ResetStage();
    }

    /// <summary>
    /// Respawn時
    /// </summary>
    public override void ResetStage()
    {
        // 全床非表示
        foreach (GameObject floor in floors)
        {
            SetFloor(floor, false);
        }

        // キュー初期化
        activeFloors.Clear();

        // 状態初期化
        currentIndex = 0;

        timer = 0f;

        isActive = false;

        Debug.Log("Stage1 Reset");
    }
}