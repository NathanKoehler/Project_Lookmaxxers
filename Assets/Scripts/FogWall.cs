using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    public Collider real;
    public Collider fake;
    [SerializeField] private mikeAi Mike;
    [SerializeField] private CharacterStats player;

    [SerializeField] private AudioClip enterSFX;

    // Start is called before the first frame update
    void Start()
    {
        real.enabled = false;
        fake.enabled = true;

        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Mike.isDead)
        {
            Destroy(this.gameObject);
        }
        if (player.isDead)
        {
            Mike.GoIdle();
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