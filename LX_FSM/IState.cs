using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX_FSM
{
    public abstract class IState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IState CurrentState { get; set; }
        public IState PreviousState { get; set; }
        public IState NextState { get; set; }
        protected FSMStatus status = FSMStatus.Enter;
        public FSMStatus Status { get { return this.status; } private set { this.status = value; }}

        public void SetNextState(IState nextState)
        {
            this.NextState = nextState;
        }

        public abstract int Enter();
        public abstract int Execute();
        public abstract int Exit();
    }
}
