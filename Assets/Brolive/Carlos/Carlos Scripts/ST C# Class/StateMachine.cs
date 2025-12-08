using brolive;
using JetBrains.Annotations;
using UnityEngine;

namespace Carlos
{
    public class StateMachine
    {
        State currentState;
        public EnemyTest enemy;

        public StateMachine(EnemyTest enemy)
        {
            this.enemy = enemy; 
        }

        public void Update()
        {
            currentState?.OnUpdate();
        }

        public void ChangeState(State newState)
        {
            currentState?.OnExit();

            currentState = newState;

            currentState.OnEnter();
        }
    }
}