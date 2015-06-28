using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Enumerators
{
    /// <summary>
    /// This enumerates a tree of n leaves with depth d in O(nd) time and O(d) space, this is unlike all the
    /// other implementations, that are O(n) time and O(d) space.
    /// </summary>
    class LeafEnumerator : IEnumerator<int>
    {
        private Lazy<LeafEnumerator> _leftLeaves;
        private Lazy<LeafEnumerator> _rightLeaves;
        private bool _exhausted;
        private Node _here;

        public LeafEnumerator(Node root)
        {
            _here = root;
            _leftLeaves = new Lazy<LeafEnumerator>(() => new LeafEnumerator(root.Left), false);
            _rightLeaves = new Lazy<LeafEnumerator>(() => new LeafEnumerator(root.Right), false);
        }

        public int Current
        {
            get; private set;
        }

        public bool MoveNext()
        {
            if (_here.IsLeaf && !_exhausted)
            {
                Current = _here.Value;
                _exhausted = true;
                return true;
            }
            else
            {
                var left = _here.Left;
                var right = _here.Right;
                bool hasMore = Advance(left, _leftLeaves) || Advance(right, _rightLeaves);
                return hasMore;                
            }
        }

        private bool Advance(Node fromWhere, Lazy<LeafEnumerator> e)
        {
            if (fromWhere != null)
            {
                var v = e.Value;
                if (!v._exhausted && v.MoveNext())
                {
                    Current = v.Current;
                    return true;
                }
                v._exhausted = true;
            }
            return false;
        }

        public void Reset()
        {            
        }

        public void Dispose()
        {
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
