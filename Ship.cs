using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void setDir(Vector2 vector2)
    {
        _agent.SetDestination(vector2);
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
