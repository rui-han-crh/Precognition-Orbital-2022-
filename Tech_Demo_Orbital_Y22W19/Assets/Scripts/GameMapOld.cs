using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// An immutable data structure that comprises of map data, such as the unit positions,
/// cover positions and other systems pertaining to the map
/// </summary>
public struct GameMapOld
{

    public static readonly Vector3 UNIT_GRID_OFFSET = Vector2.one / 2;

    private readonly UnitCombat unitCombat;
    private readonly UnitMovement unitMovement;
    private readonly GameMapDataOld mapData;

    private readonly UnitOld currentTurnUnit;

    private readonly MapActionRequestOld lastAction;

    public GameMapDataOld MapData => mapData;

    public MapActionRequestOld LastAction => lastAction;

    public UnitOld CurrentUnit => currentTurnUnit;
    public Vector3Int CurrentUnitPosition => mapData.UnitPositionMapping[currentTurnUnit];

    public HashSet<Vector3Int> AllUnitPositions => new HashSet<Vector3Int>(mapData.PositionUnitMapping.Keys);
    public HashSet<UnitOld> AllUnits => new HashSet<UnitOld>(mapData.UnitPositionMapping.Keys);

    // CONSTRUCTORS

    // Creates a new map from scratch
    public GameMapOld(Dictionary<Vector3Int, UnitOld> positionUnitMap,
                IEnumerable<Vector3Int> fullCoverPositions,
                IEnumerable<Vector3Int> halfCoverPositions,
                IEnumerable<Vector3Int> groundPositions,
                Dictionary<Vector3Int, int> groundTileCosts)
    {
        this.currentTurnUnit = GetUnitWithLeastExhaustion(positionUnitMap.Values);
        this.lastAction = null;

        mapData = new GameMapDataOld(positionUnitMap, fullCoverPositions, halfCoverPositions, groundPositions, groundTileCosts);
        unitCombat = new UnitCombat(currentTurnUnit, mapData);
        unitMovement = new UnitMovement(currentTurnUnit, mapData);
    }

    // Clones a new map from the oldmap with new updates units
    private GameMapOld(GameMapOld oldMap,
                Dictionary<Vector3Int, UnitOld> positionUnitMap,
                MapActionRequestOld lastAction)
    {
        this.currentTurnUnit = GetUnitWithLeastExhaustion(positionUnitMap.Values);
        this.lastAction = lastAction;

        mapData = new GameMapDataOld(positionUnitMap, oldMap.mapData);
        unitCombat = new UnitCombat(currentTurnUnit, mapData);
        unitMovement = new UnitMovement(currentTurnUnit, mapData);

    }

    // STATIC METHODS
    private static UnitOld GetUnitWithLeastExhaustion(IEnumerable<UnitOld> units)
    {
        if (units.Count() == 0)
        {
            throw new System.Exception("There are no units");
        }

        UnitOld leastExhaustedUnit = units.First();
        foreach (UnitOld unit in units)
        {
            if (unit.Health > 0 && (unit.Time < leastExhaustedUnit.Time))
            {
                leastExhaustedUnit = unit;
            }
        }
        return leastExhaustedUnit;
    }

    // PUBLIC METHODS

    public Dictionary<Vector3Int, int> GetReachableTiles()
    {
        Pathfinder2D.ShortestPathTree shortestPathTree = unitMovement.GetShortestPaths();
        return shortestPathTree.Costs;
    }

    public Pathfinder2D.ShortestPathTree GetShortestPaths()
    {
        return unitMovement.GetShortestPaths();
    }

    public HashSet<Vector3Int> GetAttackablePositions()
    {
        return unitCombat.GetAllPositionsAttackable(this);
    }

    public IEnumerable<AttackRequestOld> GetAllAttackRequestsPossible()
    {
        return unitCombat.GetAllPossibleAttacks(this);
    }

    public IEnumerable<MovementRequestOld> GetAllMovementRequestsPossible()
    {
        return unitMovement.GetAllMovementsPossible(this);
    }

    /// <summary>
    /// Determines if the current active unit is able to issue an attack
    /// towards the specified tile position.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>An AttackRequest describing the issued attack</returns>
    public AttackRequestOld IsAttackableAt(Vector3Int position)
    {
        if (!mapData.PositionUnitMapping.ContainsKey(position))
        {
            return new AttackRequestOld(this,
                                    CurrentUnit,
                                    CurrentUnitPosition,
                                    position,
                                    AttackStatus.IllegalTarget,
                                    new Vector3Int[] { CurrentUnitPosition, position },
                                    UnitCombat.ATTACK_COST);
        }
        return unitCombat.QueryTargetAttackable(this, GetUnitByPosition(position));
    }

    public GameMapOld RemoveStatusEffectFromUnitAtPosition(Vector3Int position, UnitStatusEffects.Status status)
    {
        if (!AllUnitPositions.Contains(position))
        {
            return this;
        }

        UnitOld unit = GetUnitByPosition(position);

        Dictionary<Vector3Int, UnitOld> newPositionUnitMap = new Dictionary<Vector3Int, UnitOld>();
        foreach (UnitOld copiedUnit in mapData.UnitPositionMapping.Keys)
        {
            UnitOld clonedUnit = copiedUnit.Clone();
            newPositionUnitMap[mapData.UnitPositionMapping[copiedUnit]] = clonedUnit;
        }

        newPositionUnitMap[mapData.UnitPositionMapping[unit]] = unit.RemoveStatus(status);
        return new GameMapOld(this, newPositionUnitMap, LastAction);
    }


    /// <summary>
    /// Finds all the positions of tiles in range of the current active unit.
    /// </summary>
    /// <returns>A hash set of all positions within the range of the current active unit</returns>
    public HashSet<Vector3Int> GetRange()
    {
        return unitCombat.GetAllPositionsInRange();
    }


    /// <summary>
    /// Retrives the position of a given unit from the map data.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns>The position of this unit</returns>
    public Vector3Int GetPositionByUnit(UnitOld unit)
    {
        return mapData.GetPositionByUnit(unit);
    }


    /// <summary>
    /// Retrieves the unit at the given position from the map data.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>The unit at this position</returns>
    public UnitOld GetUnitByPosition(Vector3Int position)
    {
        return mapData.GetUnitByPosition(position);
    }


    public bool ExistsUnitAt(Vector3Int position)
    {
        return mapData.ExistsUnitAt(position);
    }


    public bool HasFullCoverAt(Vector3Int position)
    {
        return mapData.HasFullCoverAt(position);
    }


    public bool HasHalfCoverAt(Vector3Int position)
    {
        return mapData.HasHalfCoverAt(position);
    }


    public bool IsWalkableOn(Vector3Int position)
    {
        return mapData.IsWalkableOn(position);
    }


    public int GetTileCost(Vector3Int position)
    {
        return mapData.GetTileCost(position);
    }


    /// <summary>
    /// Find the shortest path from the position of the acting unit to a specified destination
    /// in this current map.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="path">A reference variable that stores the path</param>
    /// <returns>An integer value describing the cost of the path taken</returns>
    public int FindShortestPathTo(Vector3Int destination, out IEnumerable<Vector3Int> path)
    {
        Pathfinder2D pathfinder = new Pathfinder2D(currentTurnUnit, GetPositionByUnit(currentTurnUnit), destination, mapData);
        path = pathfinder.FindDirectedPath().Select(x => x.Coordinates);
        return pathfinder.PathCost;
    }


    /// <summary>
    /// Determines if one side has won in this current state of the game map.
    /// </summary>
    /// <returns>A boolean describing if the game is over</returns>
    public bool IsGameOver()
    {
        // Either there is no unit who is an enemy, or there is no unit who is a friendly
        return mapData.UnitPositionMapping.Keys.Where(unit => unit.Faction == Faction.Enemy).All(unit => unit.Health == 0)
            || mapData.UnitPositionMapping.Keys.Where(unit => unit.Faction == Faction.Friendly).All(unit => unit.Health == 0);
    }


    /// <summary>
    /// Determines if an attack initiated from an attacking position towards 
    /// a defending position is valid, regardless of whether there exists any 
    /// unit at either position. The cost of the returned request is always zero.
    /// </summary>
    /// <param name="offensivePosition"></param>
    /// <param name="defensivePosition"></param>
    /// <param name="range"></param>
    /// <returns>An AttackRequest with zero cost</returns>
    public AttackRequestOld QueryAttackability(Vector3Int offensivePosition, Vector3Int defensivePosition, int range)
    {
        return UnitCombat.QueryTargetAttackable(this, offensivePosition, defensivePosition, range);
    }


    /// <summary>
    /// Determines if an attack initiated by the attacker unit towards a position is valid,
    /// regardless of whether there exists any unit at the position. The cost of the returned
    /// request is always zero.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defensivePosition"></param>
    /// <param name="range"></param>
    /// <returns>An AttackRequest with zero cost</returns>
    public AttackRequestOld QueryAttackability(UnitOld attacker, Vector3Int defensivePosition, int range)
    {
        AttackRequestOld request = UnitCombat.QueryTargetAttackable(this, mapData.GetPositionByUnit(attacker), defensivePosition, range);
        return new AttackRequestOld(request.PreviousMap,
                                attacker,
                                request.ShootFromPosition,
                                request.TargetPosition,
                                request.Status,
                                request.TilesHit,
                                0);
    }


    /// <summary>
    /// Returns the positions that overwatching units will shoot at when an enemy moves along a given path.
    /// Each overwatching unit will only fire once at the first time when the enemy is attackable, but will
    /// not fire subsequently even if the enemy is still in view.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>An enumerable collection of attack requests that have zero cost.</returns>
    public IEnumerable<AttackRequestOld> OverwatchersCanAttackAny(IEnumerable<Vector3Int> path)
    {
        IEnumerable<UnitOld> overwatchingUnits = AllUnits.Where(x => x.UnitStatusEffects.OnOverwatch);
        List<AttackRequestOld> result = new List<AttackRequestOld>();

        foreach (UnitOld unit in overwatchingUnits)
        {
            foreach (Vector3Int position in path)
            {
                AttackRequestOld request = QueryAttackability(unit, position, unit.Range);
                if (request.Successful)
                {
                    result.Add(request);
                    break;
                }
            }
        }

        return result;
    } 


    [Obsolete("Evaluate is no longer supported in GameMaps, do not use.")]
    public int Evaluate()
    {
        IEnumerable<UnitOld> friendlyUnits = mapData.UnitPositionMapping
                                            .Keys
                                            .Where(x => x.Faction == Faction.Friendly);
        IEnumerable<UnitOld> enemyUnits = mapData.UnitPositionMapping
                                            .Keys
                                            .Where(x => x.Faction == Faction.Enemy);
        if (IsGameOver())
        {
            return friendlyUnits.Where(x => x.Health > 0).Count() == 0 ? int.MinValue : int.MaxValue;
        }
        return friendlyUnits.Sum(x => x.Health) / friendlyUnits.Count()
                - enemyUnits.Sum(x => x.Health) / enemyUnits.Count();
    }


    /// <summary>
    /// Propagates to the next iteration from this GameMap when supplied with a MapAction.
    /// </summary>
    /// <param name="action"></param>
    /// <returns>The resultant propagation of the GameMap</returns>
    public GameMapOld DoAction(MapActionRequestOld action)
    {
        if (action.ActionType == MapActionType.Failed)
        {
            return this;
        }

        Dictionary<Vector3Int, UnitOld> newPositionUnitMap = new Dictionary<Vector3Int, UnitOld>();
        foreach (UnitOld unit in mapData.UnitPositionMapping.Keys)
        {
            UnitOld clonedUnit = unit.Clone();
            newPositionUnitMap[mapData.UnitPositionMapping[unit]] = clonedUnit;
        }

        switch (action.ActionType)
        {
            case MapActionType.Attack:
                AttackRequestOld attackRequest = (AttackRequestOld)action;

                UnitOld unitAttacked = newPositionUnitMap[attackRequest.TargetPosition];

                if (UnityEngine.Random.Range(0, 1.0f) <= attackRequest.ChanceToHit)
                {
                    unitAttacked = unitAttacked.DecreaseHealth(
                        Mathf.Max(UnitCombat.MINIMUM_DAMAGE_DEALT, attackRequest.ActingUnit.Attack - unitAttacked.Defence));

                    if (unitAttacked.Health == 0)
                    {
                        newPositionUnitMap.Remove(attackRequest.TargetPosition);
                    }
                    else
                    {
                        newPositionUnitMap[attackRequest.TargetPosition] = unitAttacked;
                    }
                }

                UnitOld newMapAttackingUnit = newPositionUnitMap[attackRequest.ActingUnitPosition];

                newMapAttackingUnit = newMapAttackingUnit.UseActionPoints(attackRequest.ActionPointCost);

                newPositionUnitMap[attackRequest.ActingUnitPosition] = newMapAttackingUnit;

                break;

            case MapActionType.Movement:
                MovementRequestOld movementRequest = (MovementRequestOld)action;
                UnitOld newMapMovingUnit = newPositionUnitMap[movementRequest.ActingUnitPosition];

                newMapMovingUnit = newMapMovingUnit.UseActionPoints(movementRequest.ActionPointCost);

                newPositionUnitMap.Remove(movementRequest.ActingUnitPosition);
                newPositionUnitMap[movementRequest.DestinationPosition] = newMapMovingUnit;
                break;

            case MapActionType.Wait:
                WaitRequestOld waitRequest = (WaitRequestOld)action;

                UnitOld recoveringUnit = newPositionUnitMap[waitRequest.ActingUnitPosition];

                int apReplenished = recoveringUnit.Speed * waitRequest.WaitTime;

                recoveringUnit = recoveringUnit.ReplenishActionPoints(apReplenished).AddTime(waitRequest.WaitTime);

                newPositionUnitMap.Remove(waitRequest.ActingUnitPosition);
                newPositionUnitMap[waitRequest.ActingUnitPosition] = recoveringUnit;
                break;

            case MapActionType.Overwatch:
                OverwatchRequestOld overwatchRequest = (OverwatchRequestOld)action;
                UnitOld overwatchingUnit = newPositionUnitMap[overwatchRequest.ActingUnitPosition];

                overwatchingUnit = overwatchingUnit.AddTime(OverwatchRequestOld.TIME_CONSUMED).ApplyStatus(UnitStatusEffects.Status.Overwatch);

                newPositionUnitMap.Remove(overwatchRequest.ActingUnitPosition);
                newPositionUnitMap[overwatchRequest.ActingUnitPosition] = overwatchingUnit;
                break;
        }

        return new GameMapOld(this, newPositionUnitMap, action);
    }


    /// <summary>
    /// Gets a collection of MapActions that are ordered by their utilities, 
    /// based on the map data of this current game map
    /// </summary>
    /// <returns>A collection of MapActions, ordered by worse utility first</returns>
    public IEnumerable<MapActionRequestOld> GetOrderedMapActions()
    {
        List<MapActionRequestOld> requests = new List<MapActionRequestOld>();

        if (currentTurnUnit.ActionPointsLeft >= UnitCombat.ATTACK_COST)
        {
            IEnumerable<MovementRequestOld> movementRequests = GetAllMovementRequestsPossible();
            IEnumerable<AttackRequestOld> attacks = GetAllAttackRequestsPossible();

            requests = movementRequests.Concat<MapActionRequestOld>(attacks).ToList();
        }

        requests.Add(new WaitRequestOld(this, GetPositionByUnit(currentTurnUnit), 
            new System.Random().Next(0, currentTurnUnit.ActionPointsUsed)));

        requests.Add(new OverwatchRequestOld(this, GetPositionByUnit(currentTurnUnit)));

        // wtf
        requests.Sort((x, y) =>
        {
            Debug.Assert(x != null, $"{x} is null");
            float xUtility = x.GetUtility();
            float yUtility = y.GetUtility();

            if (    (x.ActionType == y.ActionType)
                ||  (x.ActionType == MapActionType.Overwatch && y.ActionType == MapActionType.Movement)
                ||  (x.ActionType == MapActionType.Movement && y.ActionType == MapActionType.Overwatch))
            {
                if (xUtility != yUtility)
                {
                    return (int)Mathf.Sign(xUtility - yUtility);
                }
                
                return y.ActionPointCost - x.ActionPointCost;
            }
            else
            {
                if (x.ActionType == MapActionType.Attack || y.ActionType == MapActionType.Attack)
                {
                    if (x.ActionType == MapActionType.Attack && -x.PreviousMap.EvaluateCurrentPositionSafety() <= x.ActingUnit.Risk)
                    {
                        return 1;
                    }
                    else if (y.ActionType == MapActionType.Attack && -y.PreviousMap.EvaluateCurrentPositionSafety() <= y.ActingUnit.Risk)
                    {
                        return -1;
                    }
                }
                else if (x.ActingUnit.ActionPointsLeft < UnitCombat.ATTACK_COST)
                {
                    return x.ActionType == MapActionType.Wait ? 1 : -1;
                }

                return x.ActionType - y.ActionType;
            }
        });
        Debug.Assert(requests.Count > 0, "There are no actions");
        return requests;
    }


    public override string ToString()
    {
        return $"GameMap | {currentTurnUnit.Name}'s turn | AP: {currentTurnUnit.ActionPointsLeft} " +
            $"\n{string.Join("\n", mapData.UnitPositionMapping.ToArray())}";
    }


    public override bool Equals(object obj)
    {
        if (!(obj is GameMapOld))
        {
            return false;
        }
        else
        {
            GameMapOld otherMap = (GameMapOld)obj;
            return mapData.Equals(otherMap.mapData);
        }
    }

    
    public override int GetHashCode()
    {
        // Using factored sum
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + unitCombat.GetHashCode();
            hash = hash * 23 + unitMovement.GetHashCode();
            hash = hash * 23 + mapData.GetHashCode();
            hash = hash * 23 + currentTurnUnit.GetHashCode();
            hash = hash * 23 + lastAction.GetHashCode();
            return hash;
        }
    }

    public float EuclideanDistanceToNearestRival(Vector3Int position, bool printArray = false)
    {
        GameMapOld map = this;
        float[] array = map.AllUnitPositions
                            .Where(x => !map.GetUnitByPosition(x).Faction.Equals(map.CurrentUnit.Faction))
                            .Select(x => Vector3.Distance(x, position))
                            .ToArray();

        if (printArray)
        {
            Debug.Log($"Current Unit: {map.CurrentUnit.Name}, {string.Join(", ", array)}");
        }
        
        return Mathf.Min(array);
    }


    /// <summary>
    /// Evaluates to a signed utility of the current active unit's position tile
    /// </summary>
    /// <returns>A NON-POSITIVE float that specifies the safety of the current turn unit's position</returns>
    public float EvaluateCurrentPositionSafety()
    {
        // must be non-positive

        List<Vector3Int> allRivalPositions = new List<Vector3Int>();
        foreach (Vector3Int rivalPosition in AllUnitPositions)
        {
            if (GetUnitByPosition(rivalPosition).Faction != currentTurnUnit.Faction)
            {
                allRivalPositions.Add(rivalPosition);
            }
        }

        float safety = 0;

        foreach (Vector3Int rivalPosition in allRivalPositions)
        {
            UnitOld rivalUnit = GetUnitByPosition(rivalPosition);

            AttackRequestOld hypotheticalRequest = QueryAttackability(rivalPosition, CurrentUnitPosition, rivalUnit.Range);
            if (hypotheticalRequest.Successful)
            {
                safety += Mathf.Min(-UnitCombat.MINIMUM_DAMAGE_DEALT,
                    currentTurnUnit.Defence - hypotheticalRequest.ChanceToHit * rivalUnit.Attack);
            }
        }

        return safety;
    }

    // PRIVATE METHODS
}