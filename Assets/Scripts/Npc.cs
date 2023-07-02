using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject targetIndicator;
    public float range = 10.0f;
    
    private Vector3 _targetPosition;

    private GameObject _targetInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetNewDestination();
        Move();
    }

    void Move()
    {
        agent.SetDestination(_targetPosition);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    void RandomPosition()
    {
        Vector3 targetPos;
        RandomPoint(transform.position, range, out targetPos);
        _targetPosition = targetPos;
        DrawTargetPosition();
    }

    void SetNewDestination()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        { 
            RandomPosition();
        }
    }

    void DrawTargetPosition()
    {
        // Make the targetIndicator appear at the target position
        if (_targetInstance)
        {
            Destroy(_targetInstance);
        }
        _targetInstance = Instantiate(targetIndicator, _targetPosition,Quaternion.identity);
    }
}
