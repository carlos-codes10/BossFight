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
            Debug.Log("start pursue");
            //machine.enemy.NavEnable();
            machine.enemy.AttemptBeginPursue(elapsedTime);

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            machine.enemy.UpdatePursue(elapsedTime);
            Debug.Log("pursing player");

            if (machine.enemy.inMeleeRange)
            {
                machine.ChangeState(new EnemyMeleeState(machine));
                return;
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("end pursue");
        }
    }
}