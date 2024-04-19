using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    public Collider real;
    [SerializeField] private mikeAi Mike;
    [SerializeField] private CharacterStats player;

    [SerializeField] private AudioClip enterSFX;

    // Start is called before the first frame update
    void Start()
    {
        real.enabled = false;
    }

    void Update() 
    {
        if(Mike.isDead) {
            Destroy(this.gameObject);
        }
        if(player.isDead) {
            Mike.GoIdle();
            real.enabled = false;
        }
    }

    void OnTriggerExit(Collider other) {
        if (!real.enabled && other.gameObject.tag == "Player")
        {
            real.enabled = true;
            SoundManager.instance.PlaySoundClip(enterSFX, transform, 1f);
        }
    }

}