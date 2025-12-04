using UnityEngine;

namespace Carlos
{
    public class EnemyPursueState2 : State
    {
        int CASE;

        public EnemyPursueState2(StateMachine m) : base(m) 
        { 
            machine = m; 
        }

        public override void OnEnter()
        {
            CASE = machine.enemy.GetCase();
            Debug.Log("CASE NUMBER: " + CASE);
            machine.enemy.AttemptBeginPursue(elapsedTime);
            machine.enemy.MagnitudeWalk();
        }

        public override void OnUpdate()
        {
            if (CASE == 1)
            {
                machine.enemy.UpdatePursue(elapsedTime);

                if (machine.enemy.inMeleeRange)
                {
                    machine.ChangeState(new EnemyMeleeState2(machine));
                    return;
                }
            }
            else
            {
                machine.ChangeState(new EnemySpellState(machine));
                return;
            }
        }
    }

}