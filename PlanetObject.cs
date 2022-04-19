using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanetObject : ScriptableObject
{
    public Sprite sprite;
    public int spawnRate;
    public int minSpawnShip;
    public int maxSpawnShip;
}
