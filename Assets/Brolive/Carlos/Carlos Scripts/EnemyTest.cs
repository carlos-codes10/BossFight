using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Carlos;

namespace brolive
{
    public enum EnemyStates
    {
        idle, pursue, melee, ranged, dead
    }

    public class EnemyTest : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] GameObject meleeWeapon;
        [SerializeField] Animator myAnimator;
        [SerializeField] Damageable damageable;
        [SerializeField] GameObject spellPosition;
        [SerializeField] GameObject spellPositionPhase3;
        [SerializeField] GameObject rockProjectile;
        [SerializeField] GameObject rockProjectilePhase3;
        [SerializeField] ParticleSystem myParticleSys;

        Navigator navigator;
        Transform _transform;
        Transform player;
        Rigidbody _rigidbody;

        EnemyStates state = EnemyStates.idle;
        float currentStateElapsed = 0;
        Vector3 currentTargetNodePosition;
        int pathNodeIndex = 0;
        Vector3 targetVelocity;
        public bool inMeleeRange = false;
        bool canMove;
        bool phase2 = false;
        bool phase3 = false;    


        StateMachine myStateMachine;
        StateMachine myStateMachinePhase2;
        StateMachine myStateMachinePhase3;
        Vector3 magDirection;

        // Start is called before the first frame update
        void Start()
        {
            navigator = GetComponent<Navigator>();
            player = FindObjectOfType<PlayerLogic>().transform;
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            myStateMachine = new StateMachine(this);
            myStateMachinePhase2 = new StateMachine(this);
            myStateMachinePhase3 = new StateMachine(this); 
            
            myStateMachine.ChangeState(new EnemyIdleState(myStateMachine));
            myStateMachinePhase2.ChangeState(new EnemyIdleState2(myStateMachinePhase2));
            myStateMachinePhase3.ChangeState(new EnemyPursueState3(myStateMachinePhase3));

            myParticleSys.Stop();
            //GetComponent<Phase3Helper>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            currentStateElapsed += Time.deltaTime;


            if ( damageable.currentHealth > 31)
            {
                myStateMachine.Update();

            }
            else if (damageable.currentHealth <= 30 && damageable.currentHealth > 14)
            {
                if (!phase2)
                {
                    InitializePhase2();
                    phase2 = true;
                }

                myStateMachinePhase2.Update();
            }
            else
            {
                if(!phase3)
                {
                    InitializePhase3();
                    phase3 = true;
                }

                myStateMachinePhase3.Update();
            }

                myAnimator.SetFloat("walkSpeed", magDirection.magnitude);

        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = targetVelocity;
        }

        /*void UpdateIdle()
        {
            //Debug.Log("in idle");

            if (currentStateElapsed > 2.0f)
            {
                if (inMeleeRange)
                    EnterMelee();
                else
                    //AttemptBeginPursue();
            }
        }*/

        public bool AttemptBeginPursue(float elapsed)
        {
            //Debug.Log("attempting to pursue");

            if (AttemptMakePathToPlayer())
            {
                pathNodeIndex = 0;
                //state = EnemyStates.pursue;
                elapsed = 0;

                return true;
            }

            Debug.Log("failed attempt to pursue");

            return false;
        }

        public void UpdatePursue(float elapsed)
        {
            //Debug.Log("in pursue");
                currentTargetNodePosition = navigator.PathNodes[pathNodeIndex];

                //Debug.Log("current target position is " + currentTargetNodePosition + " at index " + pathNodeIndex);

                Vector3 dirToNode = (currentTargetNodePosition - _transform.position);//.normalized;
                dirToNode.y = 0;
                dirToNode.Normalize();

            _transform.forward = dirToNode;

                float distToNode = Vector3.Distance(currentTargetNodePosition, _transform.position);

                //Debug.Log("distance to node: " + distToNode);

                if (distToNode < 3f)
                {
                    //Debug.Log("close to node");
                    pathNodeIndex++;

                    if (pathNodeIndex >= navigator.PathNodes.Count)
                    {
                        pathNodeIndex = 0;
                        AttemptMakePathToPlayer();
                        return;
                    }

                }

                /*if (inMeleeRange)
                {
                    // do melee attack
                    EnterMelee();
                    return;
                }*/

                targetVelocity = _transform.forward * speed;
                targetVelocity.y = _rigidbody.linearVelocity.y;

                if (elapsed > 1) // rebuild path every half second
                {
                    pathNodeIndex = 1;
                    AttemptMakePathToPlayer();
                }
            
        }
        void InitializePhase2()
        {
            speed = speed * 1.75f;

        }
        void InitializePhase3()
        {
            speed = speed * 3f;
            myParticleSys.Play();

        }

        public void TurnToPlayer()
        {
            var dirToPlayer = (player.transform.position - transform.position).normalized;
            dirToPlayer.y = 0;
            transform.forward = dirToPlayer;
        }

        public void MagnitudeWalk()
        {
            magDirection = myAnimator.transform.forward;
        }

        public void MagnitudeIdle()
        {
            magDirection = Vector3.zero;
        }
        public void EnterMelee()
        {
            //Debug.Log("Enter melee");
            // animator.setTrigger("melee");
            var dirToPlayer = (player.transform.position - transform.position).normalized;
            dirToPlayer.y = 0;
            transform.forward = dirToPlayer;
            targetVelocity = Vector3.zero;
            state = EnemyStates.melee;
            currentStateElapsed = 0;

            StartCoroutine(HandleMelee());
        }

        public void Punch()
        {
            targetVelocity = Vector3.zero;
            StartCoroutine(HandleMelee());
        }

   
        IEnumerator HandleMelee()
        {
            
            myAnimator.SetTrigger("punch");
            yield return new WaitForSeconds(0.2f);
            meleeWeapon.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            meleeWeapon.SetActive(false);
        }

        public void CastRockSpell()
        {
            StartCoroutine(HandleRockSpell());
        }

        IEnumerator HandleRockSpell()
        {
            myAnimator.SetTrigger("spell");
            yield return new WaitForSeconds(0.2f);
            Instantiate(rockProjectile, spellPosition.transform.position, Quaternion.identity);

        }

        public void CastRockSpellPhase3()
        {
            StartCoroutine(HandleRockSpellPhase3());
            Debug.LogWarning("CASTED ROCK PHASE 3!!!!!");
        }

        IEnumerator HandleRockSpellPhase3()
        {
            Instantiate(rockProjectilePhase3, spellPositionPhase3.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);

        }

        void UpdateMelee()
        {
            //Debug.Log("in melee");
            if (currentStateElapsed >= 2.0f)
            {
                state = EnemyStates.idle;
            }
        }

        public void Death()
        {
            navigator.enabled = false;
            targetVelocity = Vector3.zero;
            GameManager.instance.GoToNextLevel();
            state = EnemyStates.dead;
        }

        public int GetCase()
        {
            int rndNum = Random.Range(1, 3);
            return rndNum;
        }

        void UpdateDead()
        {
            //Debug.Log("in dead");
        }

        bool AttemptMakePathToPlayer()
        {
            return (navigator.CalculatePathToPosition(player.position));
        }

        float DistanceToPlayer()
        {
            return Vector3.Distance(_transform.position, player.position);
        }

        public void SetInMeleeRange(bool inMeleeRange)
        {
            this.inMeleeRange = inMeleeRange;
        }

        
    }
}