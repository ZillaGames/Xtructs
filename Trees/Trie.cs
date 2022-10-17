using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zilla.Xtructs.Ext;

namespace Zilla.Xtructs.Trees
{

    public interface ITrie<TNode, T> : ITree<TNode>, IValue<T>, ICollection<IEnumerable<T>>, IComparable<TNode>
        where TNode : ITrie<TNode, T> { }

    public class CharTrieNode : ITree<CharTrieNode>, IValue<char>, IComparable<CharTrieNode>
    {
        public List<CharTrieNode> Children;

        public void Add(ReadOnlySpan<char> str)
        {
            if (str.IsEmpty) return;

            if (!Children.TryFindIndex(str[0].Equals, out int index))
                CreateChildNode(str);
            else Children[index].Add(str[1..]);
        }

        public void CreateChildNode(ReadOnlySpan<char> str)
        {
            var node = this;
            for(int i = 0; i < str.Length; i++)
                Children.AddSorted(node = new CharTrieNode(str[i], node));
        }

        public bool Contains(ReadOnlySpan<char> str, out string suggestion)
        {
            if(str.IsEmpty)
            {
                suggestion = string.Empty;
                return true;
            }

            if (Children.TryFindIndex(str[0].Equals, out int index))
                return Children[index].Contains(str[1..], out suggestion);

            suggestion = string.Concat(TopSuggestion);
            return false;
        }

        IEnumerable<char> TopSuggestionEnumerable
        {
            get
            {
                if (Children.Count == 0) yield break;

                for (var node = Children[0]; node.Children.Count > 0; node = node.Children[0])
                    yield return node.Value;
            }
        }

        public string TopSuggestion => string.Concat(TopSuggestionEnumerable);

        public char Value { get; set; }
        public int priority;

        public CharTrieNode(char c, CharTrieNode parent)
        {
            Children = new List<CharTrieNode>();
            Value = c;
            priority = 0;
            Parent = parent;
        }
        public CharTrieNode Parent { get; set; }

        IEnumerable<CharTrieNode> ITree<CharTrieNode>.Children => Children;

        public int CompareTo(CharTrieNode other) => other.priority.CompareTo(priority);
    }
}