using UnityEngine;

namespace Carlos
{
    public class EnemySpellState3 : State
    {
        bool phase3move = false;
        public EnemySpellState3(StateMachine m) : base(m)
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

            if (elapsedTime > 1f)
            {
                machine.enemy.CastRockSpellPhase3();
                machine.ChangeState(new EnemyPursueState3(machine));
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}