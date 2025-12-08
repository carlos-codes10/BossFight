using UnityEngine;

namespace Carlos
{
    public class EnemySpellState : State
    {
        public EnemySpellState(StateMachine m) : base(m)
        {
            machine = m;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.enemy.TurnToPlayer();
            machine.enemy.CastRockSpell();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            machine.ChangeState(new EnemyIdleState2(machine));

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}