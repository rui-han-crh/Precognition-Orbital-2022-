﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class AttackRequest : MapActionRequest
{
    private readonly Unit offensiveUnit;
    private readonly Vector3Int shootFromPosition;

    private readonly Unit defendingUnit;
    private readonly Vector3Int shootToPosition;

    private readonly float chanceToHit;
    private readonly AttackStatus status;
    private readonly Vector3Int[] tilesHit;
    private readonly bool hasTilesHitData;

    public bool Successful => status == AttackStatus.Success;

    public AttackStatus Status => status;

    public Vector3Int[] TilesHit => tilesHit;

    public Vector3Int ShootFromPosition => shootFromPosition;

    public Unit OffensiveUnit => offensiveUnit;

    public float ChanceToHit => chanceToHit;

    public Vector3Int TargetPosition => shootToPosition;

    public Unit TargetUnit => defendingUnit;

    public AttackRequest(GameMap previousMap,
                        Vector3Int shootFromPosition, 
                        Vector3Int shootToPosition,
                        AttackStatus status,
                        Vector3Int[] tilesHit,
                        int actionPoints) 
        : base(previousMap, 
            status == AttackStatus.Success ? MapActionType.Attack : MapActionType.Failed, 
            previousMap.CurrentUnitPosition, 
            actionPoints)
    {
        this.offensiveUnit = previousMap.CurrentUnit;
        this.shootFromPosition = shootFromPosition;
        this.defendingUnit = previousMap.ExistsUnitAt(shootToPosition) ? previousMap.GetUnitByPosition(shootToPosition) : new Unit();
        this.tilesHit = tilesHit;
        this.shootToPosition = shootToPosition;
        this.status = status;
        this.chanceToHit = CalculateHitChance();
    }

    private float CalculateHitChance()
    {
        float totalSegmentsOfTiles = 0;
        float totalSegmentsOfCover = 0;

        for (int i = 1; i < tilesHit.Length - 1; i++)
        {
            float segmentWeight = Mathf.Pow(i, 2);

            if (PreviousMap.HasHalfCoverAt(tilesHit[i]))
            {
                totalSegmentsOfCover += segmentWeight;
            }
            totalSegmentsOfTiles += segmentWeight;
        }

        if (totalSegmentsOfTiles == 0)
        {
            return 1;
        }
        return 1 - totalSegmentsOfCover / totalSegmentsOfTiles;
    }

    public override string ToString()
    {
        return $"{OffensiveUnit.Name} wants to attack {shootToPosition} from {shootFromPosition}: {status} {chanceToHit}";
    }

    public override float GetUtility()
    {
        // Should I do this?
        if (defendingUnit.Equals(default(Unit)))
        {
            Debug.LogWarning("Illegal query on an invalid attack selection");
            return float.NegativeInfinity;
        }

        return (chanceToHit * PreviousMap.CurrentUnit.Attack) - defendingUnit.Defence;
    }
}