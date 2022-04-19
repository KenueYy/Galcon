using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            setDir(vector2);
        }
    }
    public void setDir(Vector2 vector2)
    {
        _agent.SetDestination(vector2);
    }
}
