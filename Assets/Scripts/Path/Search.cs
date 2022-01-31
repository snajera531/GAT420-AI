using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;

public static class Search
{
    public delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps);

    static public bool BuildPath(SearchAlgorithm searchAlgorithm, GraphNode source, GraphNode destination, ref List<GraphNode> path, int steps = int.MaxValue)
    {
        if (source == null || destination == null) return false;

        // reset graph nodes
        GraphNode.ResetNodes();

        // search for path from source to destination nodes        
        bool found = searchAlgorithm(source, destination, ref path, steps);
        return found;
    }

    public static void CreatePathFromParents(GraphNode node, ref List<GraphNode> path)
    {
        // while node not null
        while (node != null)
        {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.parent;
        }
        // reverse path
        path.Reverse();
    }

    public static bool DFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;
        Stack<GraphNode> nodes = new Stack<GraphNode>();
        nodes.Push(source);

        // set current number of steps
        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            // get top node of stack node (peek)
            GraphNode node = nodes.Peek();
            node.visited = true;

            bool forward = false;
            // search neighbors for unvisited node
            foreach (var neighbor in node.Neighbors)
            {
                // if node is unvisited then push on stack
                if (neighbor.visited == false)
                {
                    nodes.Push(neighbor);
                    forward = true;

                    if (neighbor == destination)
                    {
                        found = true;
                    }

                    break;
                }
            }

            // if not moving forward, pop current node off stack
            if (forward == false)
            {
                nodes.Pop();
            }
        }

        // convert stack path nodes to list
        path = new List<GraphNode>(nodes);
        path.Reverse();

        return found;
    }

    public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;
        Stack<GraphNode> nodes = new Stack<GraphNode>();
        source.visited = true;
        nodes.Push(source);

        // set current number of steps
        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            // get top node of stack node (peek)
            var node = nodes.Peek();
            node.visited = true;

            // search neighbors for unvisited node
            foreach (var neighbor in node.Neighbors)
            {
                // if node is unvisited then push on stack
                if (!neighbor.visited)
                {
                    neighbor.visited = true;
                    neighbor.parent = node;
                    nodes.Push(neighbor);
                }
                if (neighbor == destination)
                {
                    found = true;
                    break;
                }
            }
        }

        // convert stack path nodes to list
        if (found)
        {
            path = new List<GraphNode>(nodes);
            CreatePathFromParents(destination, ref path);
        }
        else
        {
            path = nodes.ToList();
        }

        return found;
    }

    public static bool Dijkstra(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new SimplePriorityQueue<GraphNode>();
        source.cost = 0;
        nodes.Enqueue(source, source.cost);

        int steps = 0;
        while(!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Dequeue();
            if(node == destination)
            {
                found = true;
                break;
            }

            foreach(var neighbor in node.Neighbors)
            {
                neighbor.visited = true;
                float cost = node.cost + node.DistanceTo(neighbor);

                if(cost < neighbor.cost)
                {
                    neighbor.cost = cost;
                    neighbor.parent = node;
                    nodes.EnqueueWithoutDuplicates(neighbor, cost);
                }
            }
        }

        if (found)
        {
            path = new List<GraphNode>();
            CreatePathFromParents(destination, ref path);
        }
        else
        {
            path = nodes.ToList();
        }

        return found;
    }

    public static bool AStar(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new SimplePriorityQueue<GraphNode>();
        source.cost = 0;
        float heuristic = Vector3.Distance(source.transform.position, destination.transform.position);
        nodes.Enqueue(source, source.cost + heuristic);

        int steps = 0;
        while(!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Dequeue();
            if(node == destination)
            {
                found = true;
                break;
            }

            foreach(var neighbor in node.Neighbors)
            {
                neighbor.visited = true;
                float cost = node.cost + node.DistanceTo(neighbor);
                if(cost < neighbor.cost)
                {
                    neighbor.cost = cost;
                    neighbor.parent = node;
                    heuristic = Vector3.Distance(neighbor.transform.position, destination.transform.position);
                    nodes.EnqueueWithoutDuplicates(neighbor, cost + heuristic);
                }
            }
        }

        if (found)
        {
            path = new List<GraphNode>();
            CreatePathFromParents(destination, ref path);
        }
        else
        {
            path = nodes.ToList();
        }

        return found;
    }
}