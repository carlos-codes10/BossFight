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

        StateMachine myStateMachine;

        // Start is called before the first frame update
        void Start()
        {
            navigator = GetComponent<Navigator>();
            player = FindObjectOfType<PlayerLogic>().transform;
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            myStateMachine = new StateMachine(this);
            myStateMachine.ChangeState(new EnemyIdleState(myStateMachine));
        }

        // Update is called once per frame
        void Update()
        {
            currentStateElapsed += Time.deltaTime;

            /*switch (state)
            {
                case EnemyStates.idle:
                    UpdateIdle();
                    break;
                case EnemyStates.melee:
                    UpdateMelee();
                    break;
                case EnemyStates.pursue:
                    UpdatePursue();
                    break;
                case EnemyStates.ranged:
                    break;
                case EnemyStates.dead:
                    UpdateDead();
                    break;
            }*/

            myStateMachine.Update();
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

        public void TurnToPlayer()
        {
            var dirToPlayer = (player.transform.position - transform.position).normalized;
            dirToPlayer.y = 0;
            transform.forward = dirToPlayer;
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

        public void SwordSwing()
        {
            StartCoroutine(HandleMelee());
        }

        IEnumerator HandleMelee()
        {
            //timeSinceLastMelee = 0;
            meleeWeapon.SetActive(true);
            meleeWeapon.GetComponent<Animator>().SetTrigger("swing");
            yield return new WaitForSeconds(0.25f);
            meleeWeapon.SetActive(false);
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