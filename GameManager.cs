using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    public int countPlanet;
    public List<Planet> choicePlanets;
    public Planet planetPrefab;
    public List<PlanetObject> typePlanet;
    private List<Planet> allPlanets;
    private List<Planet> largePlanets;
    private List<Planet> playerPlantes;
    private int x;
    private int y;
    private void Awake()
    {
        allPlanets = new List<Planet>();
        largePlanets = new List<Planet>();
        x = Screen.width / 2 / 100 - 1;
        y = Screen.height / 2 / 100 - 1;
        SpawnPlanet();
        Planet player = largePlanets[Random.Range(0, largePlanets.Count)];
        player.Capture();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectPlanets();
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            AttackOnPlanet();
        }
    }
    private void AttackOnPlanet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Planet choicePlanet = hit.collider.GetComponent<Planet>();
        if (hit.collider && !choicePlanet.is—aptured)
        {
            hit.collider.GetComponent<Planet>().DisableObstacle();
            foreach (Planet planet in choicePlanets)
            {
                planet.Attack(choicePlanet);
            }
        }
    }
    private void SelectPlanets()
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
    private void SpawnPlanet()
    {
        for (int i = 0; i < countPlanet; i++)
        {
            planetPrefab.data = typePlanet[Random.Range(0, typePlanet.Count)];
            Vector3 spawnPoint = GetPosition(x, y, planetPrefab.GetComponent<NavMeshObstacle>().radius);
            planetPrefab = Instantiate(planetPrefab, spawnPoint, Quaternion.identity);
            switch (planetPrefab.data.type)
            {
                case PlanetObject.Type.Small:
                    allPlanets.Add(planetPrefab);
                    break;
                case PlanetObject.Type.Medium:
                    allPlanets.Add(planetPrefab);
                    break;
                case PlanetObject.Type.Large:
                    allPlanets.Add(planetPrefab);
                    largePlanets.Add(planetPrefab);
                    break;
            }
        }
    }
    private Vector3 GetPosition(int x, int y, float radius)
    {
        Vector3 position = new Vector3(Random.Range(-x, x), Random.Range(-y, y));
        bool isFreePosition = false;

        if (allPlanets.Count == 0)
        {
            return position;
        }

        while (isFreePosition == false)
        {
            isFreePosition = true;
            position = new Vector3(Random.Range(-x, x), Random.Range(-y, y));

            foreach (Planet planet in allPlanets)
            {
                if (Vector2.Distance(position, planet.transform.position) <= radius + planet.transform.localScale.x / 2)
                {
                    isFreePosition = false;
                    break;
                }
            }
        }

        return position;
    }
}
