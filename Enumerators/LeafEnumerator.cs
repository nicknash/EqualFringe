using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Enumerators
{
    class LeafEnumerator : IEnumerator<int>
    {
        private LeafEnumerator _leftLeaves;
        private LeafEnumerator _rightLeaves;
        private bool _exhausted;
        private Node _here;

        public LeafEnumerator(Node root)
        {
            _here = root;
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
                bool hasMore = Advance(left, () => _leftLeaves = new LeafEnumerator(left), () => _leftLeaves)
                            || Advance(right, () => _rightLeaves = new LeafEnumerator(right), () => _rightLeaves);
                return hasMore;                
            }
        }

        private bool Advance(Node fromWhere, Action initialize, Func<LeafEnumerator> get)
        {
            if (fromWhere != null)
            {
                if (get() == null)
                {
                    initialize();
                }
                var e = get();
                if (!e._exhausted && e.MoveNext())
                {
                    Current = e.Current;
                    return true;
                }
                e._exhausted = true;
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
