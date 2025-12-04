using Unity.VisualScripting;
using UnityEngine;

namespace Carlos
{
    public abstract class State
    {
        public StateMachine machine;   
        protected float elapsedTime;

        public State(StateMachine machine)
        {
            this.machine = machine;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnUpdate()
        {
            elapsedTime += Time.deltaTime;
        }

        public virtual void OnExit()
        {

        }
    }
}