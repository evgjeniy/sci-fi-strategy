using System.Collections.Generic;

namespace SustainTheStrain.Graph.Path
{
    public class Path<T>
    {
        public List<Node<T>> nodes
        { get; private set; }

        public Path(List<Node<T>> nodes) => this.nodes = nodes;
    }
}
