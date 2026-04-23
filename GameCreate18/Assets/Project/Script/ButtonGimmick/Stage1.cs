using UnityEngine;
using System.Collections.Generic;

public class Stage1 : MonoBehaviour
{
    [SerializeField] PlayerController1 player;
    [SerializeField] GameObject[] floors;

    [SerializeField] float interval = 1.0f;

    float timer = 0f;
    int currentIndex = 0;

    bool isActive = false;

    // 表示中の床を管理（最大3つ）
    Queue<GameObject> activeFloors = new Queue<GameObject>();

    void Start()
    {
        foreach (var f in floors)
        {
            SetFloor(f, false);
        }
    }

    void Update()
    {
        if (player.IsButtonActive && !isActive)
        {
            isActive = true;
        }

        if (!isActive) return;

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;

            ShowNextFloor();
        }
    }

    void ShowNextFloor()
    {
        if (currentIndex >= floors.Length) return;

        GameObject floor = floors[currentIndex];

        // 表示
        SetFloor(floor, true);
        activeFloors.Enqueue(floor);

        // 3つ超えたら一番古いの消す
        if (activeFloors.Count > 3)
        {
            GameObject old = activeFloors.Dequeue();
            SetFloor(old, false);
        }

        currentIndex++;
    }

    void SetFloor(GameObject floor, bool state)
    {
        var rend = floor.GetComponent<Renderer>();
        var col = floor.GetComponent<Collider>();

        rend.enabled = state;
        col.enabled = state;
    }

    public void ResetStage()
    {
        // 全床を非表示
        foreach (var f in floors)
        {
            SetFloor(f, false);
        }

        // 状態リセット
        activeFloors.Clear();
        currentIndex = 0;
        timer = 0f;
        isActive = false;
    }
}