using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Planet> choicePlanets;
    public Ship shipPrefab;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider)
            {
                Planet planet = hit.collider.GetComponent<Planet>();
                if (!choicePlanets.Contains(planet))
                {
                    choicePlanets.Add(planet);
                }
                else if (choicePlanets.Contains(planet))
                {
                    choicePlanets.Remove(planet);
                }
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            foreach(Planet planet in choicePlanets)
            {
                for(int i = 0; i < planet.countShips; ++i)
                {
                    var ship = Instantiate(shipPrefab, planet.transform.parent);
                    //Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    ship.setDir(new Vector2(5,5));
                }
            }
        }

    }
}
