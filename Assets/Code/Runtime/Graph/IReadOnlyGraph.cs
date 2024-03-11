using System.Collections.Generic;

namespace SustainTheStrain.Graph
{
    public interface IReadOnlyGraph<T>
    {
        public List<Node<T>> Nodes { get; }

        public Node<T> Find(int id);
        public Node<T> Find(T value);
        public IEnumerable<Node<T>> GetAdjacents(Node<T> node);
        public IEnumerable<Node<T>> GetAdjacents(int id);
    }
}
