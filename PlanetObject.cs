using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanetObject : ScriptableObject
{
    public Sprite sprite;
    public Type type;
    public int spawnRate;
    public int minSpawnShip;
    public int maxSpawnShip;
    public int radius;

    public enum Type
    {
        Small,
        Medium,
        Large
    }
}
