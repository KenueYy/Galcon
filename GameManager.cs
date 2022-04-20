using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    public List<Planet> choicePlanets;
    public Ship shipPrefab;
    [SerializeField]
    private List<Planet> allPlantes;
    [SerializeField]
    private List<Planet> playerPlantes;
    private void Start()
    {
        //Planet player = allPlantes[Random.Range(0, allPlantes.Count)];
        //player.Capture();
        //playerPlantes.Add(player);
        playerPlantes[0].Capture();
    }
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
                if (planet.is—aptured)
                {
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
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            Planet ChoicePlanet = hit.collider.GetComponent<Planet>();
            if (hit.collider && !ChoicePlanet.is—aptured)
            {
                hit.collider.GetComponent<Planet>().DisableObstacle();
                foreach (Planet planet in choicePlanets)
                {
                    planet.Attack(hit.collider.transform.position);
                }
            }
        }

    }
}
