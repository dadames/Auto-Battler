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

        public static Dictionary<int, int> Search(IWeightedGraph<MapTile> graph, MapTile start, MapTile end)
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
                if (current == end.Id) return cameFrom;

                foreach (MapTile next in graph.Neighbours())
                {
                    int newCost = costSoFar[current] + graph.Cost(next);


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
            return cameFrom;
        }
    }
}
