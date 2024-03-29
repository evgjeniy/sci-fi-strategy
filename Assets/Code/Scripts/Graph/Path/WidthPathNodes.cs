using System.Collections.Generic;
using Dreamteck.Splines;

namespace SustainTheStrain.Graph.Path
{
    using SplineNode = Node;

    public class WidthPathNodes
    {
        public WidthPathNodes()
        {

        }

        public Path<SplineNode> FindPath(Node<SplineNode> sourceNode, Node<SplineNode> targetNode, Graph<SplineNode> graph)
        {
            if (sourceNode == null || targetNode == null) return null;

            List<Node<SplineNode>> visited = new List<Node<SplineNode>>();

            var queue = new Queue<List<Node<SplineNode>>>();
            queue.Enqueue(new List<Node<SplineNode>>());
            queue.Peek().Add(sourceNode);
            visited.Add(sourceNode);

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var lastNode = path[path.Count - 1];

                if (lastNode == targetNode)
                    return new Path<SplineNode>(path);

                foreach (var adjacent in lastNode.Adjacents)
                {
                    var newPath = new List<Node<SplineNode>>(path);
                    Node<SplineNode> adj = graph.Find(adjacent);
                    if (adj == null || visited.Contains(adj)) continue;
                    visited.Add(adj);
                    newPath.Add(adj);
                    queue.Enqueue(newPath);
                }
            }

            return null;
        }
    }
}
