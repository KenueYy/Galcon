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
    private Outline _outline;

    public void OnPointerClick(PointerEventData eventData)
    {
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
    private void Start()
    {
        OutlineLoad();
        StartCoroutine(routine: FactoryShipsCoroutine());
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = data.sprite;
        _countShips = Random.Range(data.minSpawnShip, data.maxSpawnShip);
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
            _countShips += data.spawnRate;
            planetUI.UpdateCountShips(_countShips);
            yield return new WaitForSeconds(1);
        }
    }
    private void OnHover()
    {
        _sr.color = Color.green;
    }
    private void OnNotHover()
    {
        _sr.color = new Color(0.465559f,0f,1f,1f);
    }
}
