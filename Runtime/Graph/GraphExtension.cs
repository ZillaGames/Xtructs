using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Zilla.Xtructs.Ext;

namespace Zilla.Xtructs.Graph
{
    public static class GraphExtension
    {
        public static IEnumerable<Edge> InEdges<T>(this IGraph<T> graph, int node)
            => graph.Edges.Where(e => e.to == node);

        public static IEnumerable<Edge> OutEdges<T>(this IGraph<T> graph, int node)
            => graph.Edges.Where(e => e.from == node);

        public static int Count<TNode, TValue>(this IGraph<TValue> graph)
            => graph.Values.Count;

        public static void AddNode<T, TGraph>(this IEditGraph<T, TGraph> graph, T value = default(T)) where TGraph : IGraph<T>
            => graph.Values.Add(value);

        public static T RemoveNode<T, TGraph>(this IEditGraph<T, TGraph> graph, int nodeId) where TGraph : IGraph<T>
        {
            var node = graph[nodeId];
            if (!graph.Values.Remove(node)) throw new ArgumentException($"Could not remove item {node} from {graph}");

            foreach (var e in graph.Edges.Where(e => e.from == nodeId || e.to == nodeId))
                graph.Edges.Remove(e);

            if(nodeId != graph.Edges.Count)
                graph.Edges.ForEach(e =>
                    {
                        if (e.from > nodeId) e.from--;
                        if (e.to > nodeId) e.to--;
                    });

            return node;
        }
    }
}
