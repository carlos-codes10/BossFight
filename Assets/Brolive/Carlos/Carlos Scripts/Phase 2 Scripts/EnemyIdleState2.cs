using UnityEngine;

namespace Carlos
{
    public class EnemyIdleState2 : State
    {
        int health; 
        public EnemyIdleState2(StateMachine m) : base(m)
        {
            machine = m;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.enemy.MagnitudeIdle();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (elapsedTime > 1.0f)
            {
                if (machine.enemy.inMeleeRange)
                {
                    machine.enemy.TurnToPlayer();
                    Debug.Log("in range attacking");
                    machine.ChangeState(new EnemyMeleeState2(machine));  
                }

                else
                {
                    machine.ChangeState(new EnemyPursueState2(machine));  
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}