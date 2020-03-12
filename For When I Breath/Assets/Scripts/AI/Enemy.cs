using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;

    private float _attackRange = 3f;
    private float _rayDistance = 5.0f;
    private float _stoppingDistance = 1.5f;

    private Vector3 _direction;
    private PlayerManager _target;
    private EnemyState _currentState;

    public float aggro;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    // Use this for initialization --------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Wander: //Wander ----------------------------------------------------------------------------------------------------------------------------------------------------------
                {
                    timer += Time.deltaTime;

                    if (timer >= wanderTimer)
                    {
                        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                        agent.SetDestination(newPos);
                        timer = 0;
                    }

                    var targetToAggro = CheckForAggro();
                    if (targetToAggro != null)
                    {
                        _target = targetToAggro.GetComponent<PlayerManager>();
                        _currentState = EnemyState.Chase;
                    }

                    break;
                }
            case EnemyState.Chase: //Chase ------------------------------------------------------------------------------------------------------------------------------------------------------------
                {
                    if (_target == null)
                    {
                        _currentState = EnemyState.Wander;
                        return;
                    }

                    transform.LookAt(_target.transform);
                    agent.SetDestination(_target.transform.position);

                    if (Vector3.Distance(transform.position, _target.transform.position) < _attackRange)
                    {
                        _currentState = EnemyState.Attack;
                    }
                    else if (Vector3.Distance(transform.position, _target.transform.position) > aggro)
                    {
                        _currentState = EnemyState.Wander;
                    }
                    break;
                }
            case EnemyState.Attack: //Attack ----------------------------------------------------------------------------------------------------------------------------------------------------------
                {
                    if (_target != null)
                    {
                        _target.GetComponent<PlayerManager>().curHealth -= 1;
                    }

                    // play laser beam

                    _currentState = EnemyState.Wander;
                    break;
                }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //private bool IsPathBlocked()
    //{
    //    Ray ray = new Ray(transform.position, _direction);
    //    var hitSomething = Physics.RaycastAll(ray, _rayDistance, _layerMask);
    //    return hitSomething.Any();
    //}

    Quaternion startingAngle = Quaternion.AngleAxis(-90, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);


    // Determine the target ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private Transform CheckForAggro()
    {
        float aggroRadius = aggro;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                var player = hit.collider.GetComponent<PlayerManager>();
                if (player != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    Debug.Log("Player!");
                    return player.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * aggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }

    //Visual for aggro + wander distance --------------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}

public enum EnemyState
{
    Wander,
    Chase,
    Attack
}
