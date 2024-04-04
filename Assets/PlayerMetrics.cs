using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMetrics : MonoBehaviour
{
    public int[] weaponUsed;
    public TMP_Text metricsText;
    // Start is called before the first frame update
    void Start()
    {
        weaponUsed = new int[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (metricsText != null)
        {
            metricsText.text = string.Format("Weapon 1: {0} \n Weapon 2: {1} \n Weapon 3: {2}", weaponUsed[0], weaponUsed[1], weaponUsed[2]);
        }
    }
}
