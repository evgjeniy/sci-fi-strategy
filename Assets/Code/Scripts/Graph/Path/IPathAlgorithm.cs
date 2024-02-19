namespace SustainTheStrain.Graph.Path
{
    public interface IPathAlgorithm<T>
    {
        public Path<T> FindPath(Node<T> sourceNode, Node<T> targetNode, Graph<T> graph);
    }
}
