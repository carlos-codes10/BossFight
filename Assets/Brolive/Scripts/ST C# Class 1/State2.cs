using Unity.VisualScripting;
using UnityEngine;

namespace Carlos
{
    public abstract class State2
    {
        public StateMachine2 machine;   
        protected float elapsedTime;

        public State2(StateMachine2 machine)
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