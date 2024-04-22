using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    public Collider real;
    public Collider fake;

    public GameObject enemy;


    private BossInterface enemyStats;
    [SerializeField] private CharacterStats player;

    [SerializeField] private AudioClip enterSFX;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = enemy.GetComponent<BossInterface>();
        real.enabled = false;
        fake.enabled = true;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    void Update()
    {
        if (enemyStats.currHP <= 0)
        {
            Destroy(this.gameObject);
        }
        if (player.isDead)
        {
            enemyStats.GoIdle();
            real.enabled = false;
            fake.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!real.enabled && other.gameObject.tag == "Player")
        {
            real.enabled = true;
            fake.enabled = false;
            SoundManager.instance.PlaySoundClip(enterSFX, transform, 1f);
            meshRenderer.enabled = true;
        }
    }

}