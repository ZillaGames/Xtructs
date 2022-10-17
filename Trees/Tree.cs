using System.Collections;
using System.Collections.Generic;

namespace Zilla.Xtructs.Trees
{
    /// <summary>
    /// Tree generic interface
    /// </summary>
    /// <typeparam name="TNode">Self reference</typeparam>
    public interface ITree<TNode> where TNode : ITree<TNode>
    {
        public TNode Parent { get; set; }
        public IEnumerable<TNode> Children { get; }
    }
}
