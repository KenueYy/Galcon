using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Planet> choicePlanets;

    [SerializeField]
    private Camera _mainCamera;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            Planet planet = hit.collider.GetComponent<Planet>();
            if (!choicePlanets.Contains(planet))
            {
                choicePlanets.Add(planet);
            }
            else if(choicePlanets.Contains(planet))
            {
                choicePlanets.Remove(planet);
            }
        }
    }
}
