using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.AI;

public class Planet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public PlanetUI planetUI;
    public PlanetObject data;
    public Ship shipPrefab;
    public Transform shipSpawnPoint;
    public float percentShips;
    public bool is?aptured;
    private int countShips;
    private SpriteRenderer _sr;
    private Outline _outline;
    private NavMeshObstacle _obstacle;
    public int _countAttackers = 0;
    private Vector2 _mousePos;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left && is?aptured)
            OutlineEnable();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnNotHover();
    }
    public void Attack(Planet planet)
    {
        int countAttackShips = Convert.ToInt32(Mathf.Round(countShips * percentShips));
        countShips -= countAttackShips;
        planetUI.UpdateCountShips(countShips);
        planet.SetCountAttakers(countAttackShips);
        StartCoroutine(routine: SpawnShipsCoroutine(planet, countAttackShips));
    }
    public IEnumerator SpawnShipsCoroutine(Planet planet, int countAttackShips)
    {
        for (int ship = 0; ship < countAttackShips; ship++)
        {
            Ship activeShip = Instantiate(shipPrefab, shipSpawnPoint);
            activeShip.setDir(planet.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void TakeDamage()
    {
        if (countShips >= 0 && !is?aptured)
        {
            countShips--;
            planetUI.UpdateCountShips(countShips);
        }
        else
        {
            Capture();
            countShips++;
            planetUI.UpdateCountShips(countShips);
        }
    }
    public void Capture()
    {
        _sr.color = Color.red;
        is?aptured = true;
    }
    public void EnableObstacle()
    {
        _obstacle.enabled = true;
    }
    public void DisableObstacle()
    {
        _obstacle.enabled = false;
    }
    public void SetCountAttakers(int count)
    {
        _countAttackers += count;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ship ship = collision.GetComponent<Ship>();
        if (ship)
        {
            ship.Delete();
            TakeDamage();
            _countAttackers--;
            if (_countAttackers <= 0)
                EnableObstacle();
        }
    }
    private void Awake()
    {
        countShips = UnityEngine.Random.Range(data.minSpawnShip, data.maxSpawnShip);
        planetUI.UpdateCountShips(countShips);
        OutlineLoad();
        StartCoroutine(routine: FactoryShipsCoroutine());
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = data.sprite;
        _obstacle = GetComponent<NavMeshObstacle>();
        _obstacle.radius = data.radius;
    }
    private void OutlineEnable()
    {
        if (_outline.OutlineWidth == 2)
            OutlineDisable();
        else
            _outline.OutlineWidth = 2;
    }
    private void OutlineDisable()
    {
        _outline.OutlineWidth = 0;
    }
    private void OutlineLoad()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }
    private IEnumerator FactoryShipsCoroutine()
    {
        while (true)
        {
            countShips += data.spawnRate;
            planetUI.UpdateCountShips(countShips);
            yield return new WaitForSeconds(1);
        }
    }
    private void OnHover()
    {
        _sr.color = Color.green;
    }
    private void OnNotHover()
    {
        if (!is?aptured)
            _sr.color = new Color(0.465559f, 0f, 1f, 1f);
        else
            _sr.color = Color.red;
    }
}
