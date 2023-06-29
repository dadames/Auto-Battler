using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;
using System;

namespace AutoBattler
{
    public static class AStarSearch
    {
        static public int Heuristic(Vector2 a, Vector2 b)
        {
            return Math.Abs((int)a.x - (int)b.x) + Math.Abs((int)a.y - (int)b.y);
        }

        public static List<int> Search(IWeightedGraph<MapTile> graph, MapTile start, MapTile end)
        {
            SimplePriorityQueue<int, double> frontier = new();
            Dictionary<int, int> cameFrom = new();
            Dictionary<int, int> costSoFar = new();

            frontier.Enqueue(start.Id, 0);

            cameFrom[start.Id] = start.Id;
            costSoFar[start.Id] = 0;

            while (frontier.Count() > 0)
            {
                int current = frontier.Dequeue();
                if (current == end.Id) return _ConvertPathToList(cameFrom, start.Id, end.Id);

                foreach (MapTile next in graph.Neighbours(current))
                {
                    if (!next.IsBlocked) continue;
                    int newCost = costSoFar[current] + graph.Cost(current, next.Id);                    

                    if (!costSoFar.ContainsKey(next.Id) || newCost < costSoFar[next.Id])
                    {
                        costSoFar[next.Id] = newCost;
                        int priority = newCost + Heuristic(next.GridLocation, end.GridLocation);
                        frontier.Enqueue(next.Id, priority);
                        cameFrom[next.Id] = current;
                    }
                }
            }

            Debug.LogWarning("No Path Found");
            return _ConvertPathToList(cameFrom, start.Id, end.Id);
        }


        private static List<int> _ConvertPathToList(Dictionary<int, int> path, int start, int end)
        {
            List<int> output = new();
            output.Insert(0, end);
            int currentPosition = end;

            
            while (output[0] != start)
            {
                output.Insert(0, path[currentPosition]);
                currentPosition = path[currentPosition];
                MapManager.Instance.IntToTile[currentPosition].HighlightTile();
            }

            return output;
        }
    }
}
