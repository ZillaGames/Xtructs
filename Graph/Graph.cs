using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Zilla.Xtructs.Graph
{
    public interface IGraph<T>
    {
        public IReadOnlyCollection<T> Values { get; }
        public IReadOnlyCollection<Edge> Edges { get; }
        public T this[int i] { get; }
    }

    public interface IEditGraph<T, TGraph>
        where TGraph : IGraph<T>
    {
        public List<T> Values { get; set; }
        public List<Edge> Edges { get; set; }
        public void Save(ref TGraph graph);
        public T this[int i] { get; }
    }

    public struct Edge
    {
        public int from, to;

        public Edge(int from, int to)
            => (this.from, this.to) = (from, to);
    }

    public struct Walk
    {
        private int[] _walk;

        public IEnumerator<int> Nodes
        {
            get
            {
                for (int i = 0; i < _walk.Length; i += 2)
                    yield return _walk[i];
            }
        }
        public IEnumerator<int> Edges
        {
            get
            {
                for(int i = 1; i < _walk.Length; i += 2)
                    yield return _walk[i];
            }
        }

        public override string ToString() => string.Concat( _walk);
    }
}
