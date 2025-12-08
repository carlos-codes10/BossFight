using UnityEngine;

namespace Carlos
{
    public class EnemyPursueState : State
    {
        public EnemyPursueState(StateMachine m) : base(m)
        {
            machine = m;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //machine.enemy.NavEnable();
            machine.enemy.AttemptBeginPursue(elapsedTime);
            machine.enemy.MagnitudeWalk();

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            machine.enemy.UpdatePursue(elapsedTime);;

            if (machine.enemy.inMeleeRange)
            {
                machine.ChangeState(new EnemyMeleeState(machine));
                return;
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}