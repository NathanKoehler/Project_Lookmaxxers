using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float curHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private int flaskNum;
    [SerializeField] private Scrollbar HSlider;
    [SerializeField] private TMP_Text FlaskNumDisplay;

    // Start is called before the first frame update
    void Start()
    {
        flaskNum = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HSlider.size = (curHealth / maxHealth);
        FlaskNumDisplay.text = flaskNum.ToString();
    }

    public void Rest()
    {
        flaskNum = maxFlasks;
        curHealth = maxHealth;
    }
}
