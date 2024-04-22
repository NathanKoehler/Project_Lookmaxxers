using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BossInterface
{

    public string enemyName { get; set; }

    public float currHP { get; set; }

    public float maxHP { get; set; }

    public void GoIdle();

}