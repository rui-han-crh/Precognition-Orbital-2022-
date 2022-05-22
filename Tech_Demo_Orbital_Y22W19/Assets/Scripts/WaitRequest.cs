﻿using UnityEngine;

public class WaitRequest :MapActionRequest
{
    public static readonly int TIME_CONSUMED = 500;
    public WaitRequest(GameMap previousMap, Vector3Int sourcePosition) :
        base (previousMap, MapActionType.Wait, sourcePosition, 0)
    {

    }

    public override float GetUtility()
    {
        return PreviousMap.EvaluateCurrentPositionSafety();
    }
}