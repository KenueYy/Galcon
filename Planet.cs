using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public PlanetUI planetUI;
    public PlanetObject data;
    private int _countShips;
    private SpriteRenderer _sr;

    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    private void Start()
    {
        LoadOutline();
        StartCoroutine(routine: FactoryShipsCoroutine());
        _sr.sprite = data.sprite;
        _countShips = Random.Range(data.minSpawnShip, data.maxSpawnShip);
    }
    private void LoadOutline()
    {

    }
    private IEnumerator FactoryShipsCoroutine()
    {
        _countShips += data.spawnRate;
        planetUI.UpdateCountShips(_countShips);
        yield return new WaitForSeconds(1);
    }
}
