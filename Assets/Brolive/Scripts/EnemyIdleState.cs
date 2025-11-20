using UnityEngine;

namespace Carlos
{
    public class EnemyIdleState : State
    {
        public EnemyIdleState(StateMachine m) : base(m)
        {
            machine = m;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entered idle");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Debug.Log("We are in idle");

            if (elapsedTime > 2.0f)
            {
                if (machine.enemy.inMeleeRange)
                {
                    machine.enemy.TurnToPlayer();
                    Debug.Log("in range attacking");
                    machine.ChangeState(new EnemyMeleeState(machine));  
                }

                else
                {
                    machine.ChangeState(new EnemyPursueState(machine));  
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exited idle");
        }
    }
}