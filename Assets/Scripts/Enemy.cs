using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    private enum EnemyState
    {
        NonAlert,
        Alert,
        Engaged, 
        Dead
    }

    private Animator enemyAnim;
    private NavMeshAgent _agent;

    [SerializeField]
    private EnemyState _currentState;
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _enemyEyeRange = 8.5f;
    [SerializeField]
    private float _enemyAlertTime = 2.0f;
    [SerializeField]
    private float _enemyAttackTime = 1.0f;
    [SerializeField]
    private float _enemyAttackRange = 2.0f;
    [SerializeField]
    private float _timeStamp;
    private float _reset = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        _agent = GetComponentInChildren<NavMeshAgent>();
        _agent.enabled = false;

        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyAI();
    }

    void EnemyAI()
    {
        switch (_currentState)
        {
            case EnemyState.NonAlert:
                Debug.Log("Enemy in Non Alert State");

                float distanceToTarget = CalculateDistance();
                //Has enemy seen the player?
                if (distanceToTarget < _enemyEyeRange)
                {
                    //Enemy has seen the player
                    _currentState = EnemyState.Alert;
                    _timeStamp = _reset;
                }

                //Else do nothing
                break;

            case EnemyState.Alert:
                Debug.Log("Enemy in Alert State");

                distanceToTarget = CalculateDistance();

                //Has enemy seen the player?
                if (distanceToTarget <_enemyEyeRange)
                {
                    _timeStamp += Time.deltaTime;

                    //Enemy has seen the player for x seconds
                    if (_timeStamp >= _enemyAlertTime)
                    {
                        _timeStamp = _enemyAlertTime;
                        //Move to target position
                        _agent.SetDestination(_target.transform.position);

                        //Is target in enemy range to attack?
                        if (distanceToTarget < _enemyAttackRange)
                        {
                            _currentState = EnemyState.Engaged;
                        }
                    }
                }
                //Enemy doesn't see the target
                else if (distanceToTarget > _enemyEyeRange)
                {
                    _timeStamp -= Time.deltaTime;

                    //Enemy hasn't seen the target for x seconds
                    if (_timeStamp <= _reset)
                    {
                        _timeStamp = _reset;
                        _currentState = EnemyState.NonAlert;
                    }
                }
                break;

            case EnemyState.Engaged:
                Debug.Log("Enemy in Engaged State");
                
                distanceToTarget = CalculateDistance();
                
                //Check if enemy is in range to attack target
                if(distanceToTarget < _enemyAttackRange)
                {
                    //Enemy attacks target
                    EnemyAttack();
                    _timeStamp += Time.deltaTime;

                    //Has enemy seen the target for x seconds?
                    if (_timeStamp >= _enemyAttackTime)
                    {
                        _timeStamp = _enemyAttackTime;
                        
                        //Move to target
                        _agent.SetDestination(_target.transform.position);

                        //Is target in enemy range?
                        if (distanceToTarget < _enemyAttackRange)
                        {
                            EnemyAttack();
                        }
                    }
                }

                //If target is out of attack range
                if (distanceToTarget > _enemyAttackRange)
                {
                    _timeStamp -= Time.deltaTime;

                    //Enemy hasn't seen the target for x seconds
                    if (_timeStamp <= _reset)
                    {
                        _timeStamp = _reset;
                        _currentState = EnemyState.Alert;
                    }
                }

                break;

            case EnemyState.Dead:
                Debug.Log("Enemy is in Dead State");
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ninja Stars"))
        {
            Debug.Log("Hit with ninja star");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            enemyAnim.SetBool("Moving", false);
            enemyAnim.SetBool("Grounded", true);
            StartCoroutine(EnableNavMesh());
        }
    }

    IEnumerator EnableNavMesh()
    {
        yield return new WaitForSeconds(1f);
        _agent.enabled = true;
    }

    float CalculateDistance()
    {
        float distance = Vector3.Distance(_target.transform.position, transform.position);
        return distance;
    }

    void EnemyAttack()
    {
        Debug.Log("Enemy throws a kick to target");
    }
}
