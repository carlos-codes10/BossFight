using UnityEngine;

namespace Carlos
{
    public class EnemyMeleeState : State
    {
        public EnemyMeleeState(StateMachine m) : base(m)
        {
            machine = m;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.enemy.Punch();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            machine.ChangeState(new EnemyIdleState(machine));

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}