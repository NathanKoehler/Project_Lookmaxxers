using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using TMPro;

public class BossHealthUIHandler : MonoBehaviour
{
    private CanvasGroup cg;
    public bool nearBoss;
    public PlayerRange pr;
    public Scrollbar EntityHealth;
    public GameObject boss;

    private BossInterface bossInterface;
    public TMP_Text BossName;

    // Start is called before the first frame update
    void Start()
    {
        bossInterface = boss.GetComponent<BossInterface>();
        cg = GetComponent<CanvasGroup>();
        nearBoss = false;
        cg.alpha = 0.0f;
        BossName.text = bossInterface.enemyName;
    }

    // Update is called once per frame
    void Update()
    {
        EntityHealth.size = (bossInterface.currHP / bossInterface.maxHP);

        nearBoss = pr.isNearEntity;
        if (nearBoss)
        {
            if (!cg.gameObject.activeSelf && bossInterface.currHP <= 0)
            {
                cg.gameObject.SetActive(true);
                cg.alpha = 1.0f;
            } else if (!cg.gameObject.activeSelf)
            {
                cg.gameObject.SetActive(false);
                cg.alpha = 0.0f;
            }
        } else
        {
            cg.alpha = 0.0f;
        }
    }
}
