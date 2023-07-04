using System;
using System.Collections.Generic;
using UnityEngine;


namespace AutoBattler
{
    public static class TilemapUtilities
    {
        public static int CompareDistance(Vector2Int position1, Vector2Int position2)
        {
            return (int)MathF.Abs(position1.x - position2.x) + (int)Mathf.Abs(position1.y - position2.y);
        }


        //Unit Checks
        public static Unit FindClosestUnitOnMapByOwnerId(MapTile start, List<int> unitOwnerIds)
        {
            Unit closestUnit = null;
            int closestUnitDistance = int.MaxValue;

            foreach (Unit unit in UnitCoordinatorManager.Instance.UnitsOnMap)
            {
                if (closestUnit == null) { closestUnit = unit; }
                int distance = CompareDistance(start.GridLocation, unit.ParentTile.GridLocation);

                foreach (int id in unitOwnerIds)
                {
                    if (id == unit.OwnerId && distance < closestUnitDistance)
                    {
                        closestUnit = unit;
                    }
                }
            }

            return closestUnit;
        }

        public static List<Unit> FindUnitsOnMapByOwnerId(List<int> unitOwnerIds)
        {
            List<Unit> unitsOnMap = new();

            foreach (Unit unit in UnitCoordinatorManager.Instance.UnitsOnMap)
            {
                
                foreach (int id in unitOwnerIds)
                {
                    if (id == unit.OwnerId)
                    {
                        unitsOnMap.Add(unit);
                    }
                }
            }

            return unitsOnMap;
        }

        public static UnitManager FindClosestUnitByOwnerId(MapTile start, int range, List<int> unitOwnerIds)
        {
            List<UnitManager> unitsInRange = FindUnitsInRange(MapManager.Instance.PathfindingGrid, start, range);
            UnitManager closestUnit = null;
            int closestUnitDistance = int.MaxValue;

            foreach (UnitManager unitManager in unitsInRange)
            {
                if (closestUnit == null) { closestUnit = unitManager; }
                int distance = CompareDistance(start.GridLocation, unitManager.Unit.ParentTile.GridLocation);

                foreach (int id in unitOwnerIds)
                {
                    if (id == unitManager.Unit.OwnerId && distance < closestUnitDistance)
                    {
                        closestUnit = unitManager;
                    }
                }
            }

            return closestUnit;
        }

        public static List<UnitManager> FindUnitsInRangeByOwnerId(MapTile start, int range, List<int> unitOwnerIds)
        {
            List<UnitManager> unitsInRange = FindUnitsInRange(MapManager.Instance.PathfindingGrid, start, range);
            List<UnitManager> targetUnitsInRange = new();
            foreach (UnitManager unitManager in unitsInRange)
            {
                foreach (int id in unitOwnerIds)
                {
                    if (id == unitManager.Unit.OwnerId)
                    {
                        targetUnitsInRange.Add(unitManager);
                    }
                }
            }

            return targetUnitsInRange;
        }

        public static List<UnitManager> FindUnitsInRange(IWeightedGraph<MapTile> graph, MapTile start, int range)
        {
            List<UnitManager> unitsInRange = new();
            Queue<int> frontier = new();
            frontier.Enqueue(start.Id);

            List<int> reached = new();
            reached.Add(start.Id);


            while (frontier.Count > 0)
            {
                int current = frontier.Dequeue();

                foreach (MapTile next in graph.Neighbours(current))
                {
                    if (next.IsBlocker || reached.Contains(next.Id)) { continue; }
                    if (next.ContainsUnit)
                    {
                        unitsInRange.Add(next.OccupyingUnit);
                    }
                    if (CompareDistance(next.GridLocation, start.GridLocation) < range)
                    {
                        frontier.Enqueue(next.Id);
                        reached.Add(next.Id);
                    }
                }
            }

            return unitsInRange;
        }
    }
}
