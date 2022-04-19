using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetUI : MonoBehaviour
{
    public TextMeshProUGUI txCountShips;

    public void UpdateCountShips(int count)
    {
        txCountShips.text = count.ToString();
    }
}
