using UnityEngine;

namespace Carlos
{
    public class EnemyMeleeState2 : State
    {
        public EnemyMeleeState2(StateMachine m) : base(m)
        {
            machine = m;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("start melee");
            machine.enemy.Punch();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            machine.ChangeState(new EnemyIdleState2(machine));

        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("end melee");
        }
    }
}