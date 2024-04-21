using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePropScript : MonoBehaviour
{
    public GameObject originalProp;
    public GameObject brokenProp;
    public bool isBroken = false;

    private float explosionMinForce = 1f;
    private float explosionMaxForce = 1000f;
    private float explosionRadius = 0.1f;

    private float fragScale = 8f;

    public void Break(Vector3 explosionSource, Transform originalTransform = null)
    {
        isBroken = true;

        SoundManager.instance.PlayBreakSound();

        GameObject brokenObj = Instantiate(brokenProp) as GameObject;

        if (originalTransform) {
            brokenObj.transform.position = originalTransform.position;
            brokenObj.transform.rotation = originalTransform.rotation;
        } else
        {
            brokenObj.transform.position = originalProp.transform.position;
            brokenObj.transform.rotation = originalProp.transform.rotation;
        }
       

        originalProp.SetActive(false);

        foreach (Transform child in brokenObj.transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), explosionSource, explosionRadius);
            }

            StartCoroutine(Shrink(child, 2));
        }
        Destroy(brokenObj, 5);
    }

    private IEnumerator Shrink(Transform child, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 scale = child.localScale;
        
        while (scale.x >= 0)
        {
            scale -= new Vector3(fragScale, fragScale, fragScale);
            child.localScale = scale;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
