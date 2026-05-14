using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage3 : StageBase
{
    [Header("References")]
    [SerializeField]
    private PlayerMovement player;

    [Header("Invisible Floors")]
    [SerializeField]
    private GameObject[] floors;

    [SerializeField]
    private float visibleTime = 3f;

    // Button押下待機
    private bool _canActivate = true;

    private Coroutine _floorCoroutine;

    private void Start()
    {
        HideAllFloors();
    }

    private void Update()
    {
        if (player == null)
            return;

        // Button ONで1回だけ発動
        if (player.IsButtonActive &&
            _canActivate)
        {
            _canActivate = false;

            _floorCoroutine =
                StartCoroutine(
                    FloorRoutine()
                );
        }

        // Button OFFで再発動可能
        if (!player.IsButtonActive)
        {
            _canActivate = true;
        }
    }

    private IEnumerator FloorRoutine()
    {
        // 床表示
        ShowAllFloors();

        yield return new WaitForSeconds(
            visibleTime
        );

        // 床非表示
        HideAllFloors();

        // Button OFFへ戻す
        player.ResetButtonState();
    }

    private void ShowAllFloors()
    {
        SetFloorVisible(true);
    }

    private void HideAllFloors()
    {
        SetFloorVisible(false);
    }

    private void SetFloorVisible(bool state)
    {
        foreach (GameObject floor in floors)
        {
            if (floor == null)
                continue;

            Renderer[] renderers =
                floor.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer rend in renderers)
            {
                rend.enabled = state;
            }
        }
    }

    public override void StopStage()
    {
        if (_floorCoroutine != null)
        {
            StopCoroutine(_floorCoroutine);
        }

        HideAllFloors();
    }

    public override void ResetStage()
    {
        StopStage();

        _canActivate = true;
    }
}