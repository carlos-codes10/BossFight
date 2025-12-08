using UnityEngine;

namespace Carlos
{
    public class EnemyMeleeState3 : State
    {
        public EnemyMeleeState3(StateMachine m) : base(m)
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