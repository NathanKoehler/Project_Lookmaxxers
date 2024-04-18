using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    public Collider real;
    [SerializeField] private mikeAi Mike;
    [SerializeField] private CharacterStats player;

    // Start is called before the first frame update
    void Start()
    {
        real.enabled = false;
    }

    void Update() 
    {
        Debug.Log(Mike.currentState);
        if(Mike.isDead) {
            Destroy(this.gameObject);
        }
        if(player.isDead) {
            Mike.currentState = mikeAi.State.Idle;
            real.enabled = false;
        }
    }

    void OnTriggerExit() {
        real.enabled = true;
    }
}
