using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.ExplicitState
{
    class State
    {
        public static readonly State NoMoreLeaves = new State();

        public readonly State Parent;

        private Node _where;
        public int LeafValue { get { return _where.Value; } }
        public bool IsLeaf { get { return _where.IsLeaf; } }

        private IEnumerator<State> _childrenToProcess;        
        public IEnumerator<State> ChildrenToProcess
        {
            get
            {
                if (_childrenToProcess == null)
                {
                    var childStates = from c in _where.Children
                                      select new State(c, this);
                    _childrenToProcess = childStates.GetEnumerator();
                }
                return _childrenToProcess;

            }
        }

        public State()
        {
        }

        public State(Node n, State parent)
        {
            _where = n;
            Parent = parent;
        }

        public State(Node n)
        {
            _where = n;
        }

        public State Continue(Func<State, State> getNextState)
        {
            return Parent == null ? NoMoreLeaves : getNextState(Parent);
        }
    }
}
