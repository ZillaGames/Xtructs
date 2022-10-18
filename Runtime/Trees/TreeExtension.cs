using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zilla.Xtructs.Trees;

namespace Zilla.Xtructs.TreeExt
{
    public static class TreeExtension
    {
        public static int Level<TNode>(this TNode node) where TNode : ITree<TNode>
            => node.Parents().Count();

        public static IEnumerable<TNode> Parents<TNode>(this TNode node)
            where TNode : ITree<TNode>
        {
            for (var p = node.Parent; p != null; p = p.Parent)
                yield return p;
        }

        public static bool IsLeaf<TNode>(this TNode node) where TNode : ITree<TNode>
            => node.Children.Count() == 0;

        public static int Degree<TNode>(this TNode node) where TNode :  ITree<TNode>
            => node.DFS().Count();

        public static IEnumerable<TNode> DFS<TNode>(this TNode node)
            where TNode : ITree<TNode>
        {
            yield return node;

            foreach (var child in node.Children)
                yield return child;
        }

        public static IEnumerable<TNode> Leaves<TNode>(this TNode node)
            where TNode : ITree<TNode>
        {
            if (node.IsLeaf())
                yield return node;

            else foreach (var child in node.Children)
                    foreach (var leaf in child.Leaves())
                        yield return leaf;
        }

        public static IEnumerable<TNode> Siblings<TNode>(this TNode node)
            where TNode : ITree<TNode>
        {
            foreach (var n in node.Parent?.Children)
                yield return n;
        }

    }
}
