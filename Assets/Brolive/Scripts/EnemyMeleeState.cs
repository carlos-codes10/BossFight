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
            Debug.Log("start melee");
            machine.enemy.EnterMelee();
            //machine.enemy.SwordSwing();
            
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (elapsedTime > 2.0f)
            {
                machine.ChangeState(new EnemyIdleState(machine));
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("end melee");
        }
    }
}