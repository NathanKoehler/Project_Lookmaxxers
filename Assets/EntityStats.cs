using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEntityStats: MonoBehaviour
{
    public abstract void TakeDamage(int damage);
    public abstract IEnumerator Die();

    public abstract void StartStagger();

    public abstract void EndStagger();

    public abstract void OnAttackBegin();

    public abstract void OnAttackEnd();

    public bool isDead = false;
}
