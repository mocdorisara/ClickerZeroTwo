using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Framework
{
    public class State<T>
    {
        public State(T owner)
        {
            this.owner = owner;
        }

        protected T owner;
        public virtual void OnEntering() { }
        public virtual void OnUpdating(float dt) { }
        public virtual void OnExiting() { }
    }
}
