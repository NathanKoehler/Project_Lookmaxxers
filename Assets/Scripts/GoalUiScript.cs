using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUiScript : MonoBehaviour
{
    public List<GameObject> goals;
    public int currentGoal = 0;

    private void Awake()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            goals.Add(child.gameObject);
        }
        goals[currentGoal].SetActive(true);
    }

    public void ChangeGoal(int index)
    {
        if (index > currentGoal)
        {
            goals[currentGoal].SetActive(false);
            currentGoal = index;
            goals[index].SetActive(true);
        }
    }
}
