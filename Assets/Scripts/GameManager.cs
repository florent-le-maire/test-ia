using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public GameObject npc;

    public float range;
    // Start is called before the first frame update
    void Start()
    {
        GenerateIas(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateIas(int number)
    {
        GameObject[] objects = new GameObject[number];
        for (int i = 0; i < number; i++)
        {
            var center = new Vector3(0, 0, 0);
            Vector3 position;
            RandomPoint(center, range, out position);
            objects[i] = Instantiate(npc, position,Quaternion.identity);
        }
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
    
}
