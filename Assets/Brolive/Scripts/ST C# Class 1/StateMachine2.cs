using brolive;
using JetBrains.Annotations;
using UnityEngine;

namespace Carlos
{
    public class StateMachine2
    {
        State2 currentState;
        public EnemyTest enemy;

        public StateMachine2(EnemyTest enemy)
        {
            this.enemy = enemy; 
        }

        public void Update()
        {
            currentState?.OnUpdate();
        }

        public void ChangeState(State2 newState)
        {
            currentState?.OnExit();

            currentState = newState;

            currentState.OnEnter();
        }
    }
}