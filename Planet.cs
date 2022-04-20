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
    public bool isÑaptured;
    private int countShips;
    private SpriteRenderer _sr;
    private Outline _outline;
    private NavMeshObstacle _obstacle;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left && isÑaptured)
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
    public void Attack(Vector2 dir)
    {
        int countAttackShips = Convert.ToInt32(Mathf.Round(countShips * percentShips));
        countShips -= countAttackShips;
        planetUI.UpdateCountShips(countShips);
        for(int ship = 0; ship < countAttackShips; ship++)
        {
            Ship activeShip = Instantiate(shipPrefab, shipSpawnPoint);
            activeShip.setDir(dir);
        }
    }
    public void TakeDamage()
    {
        if (countShips >= 0 && !isÑaptured)
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
        isÑaptured = true;
    }
    public void EnableObstacle()
    {
        _obstacle.enabled = true;
    }
    public void DisableObstacle()
    {
        _obstacle.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ship ship = collision.GetComponent<Ship>();
        if (ship)
        {
            ship.Delete();
            TakeDamage();
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
        if (!isÑaptured)
            _sr.color = new Color(0.465559f, 0f, 1f, 1f);
        else
            _sr.color = Color.red;
    }
}
