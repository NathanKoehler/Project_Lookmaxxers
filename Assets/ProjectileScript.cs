using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string againstTag;
    public int damage = 4;
    [SerializeField] private Vector3 dir;
    public float speed = 120f;
    float timer = 4;
    void Start()
    {
        
    }

    private void Update()
    {
        if (timer < 0f) Destroy(this.gameObject);
        timer -= 1f * Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //GetComponent<Rigidbody>().AddForce(dir * speed);
        GetComponent<Rigidbody>().AddForce(dir.normalized * speed);
    }

    private GameObject FindEnemyWithStats(GameObject obj)
    {
        IEntityStats stats = obj.GetComponent<IEntityStats>();
        if (stats != null)
        {
            return obj;
        }
        else
        {
            return FindEnemyWithStats(obj.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == againstTag)
        {
            GameObject enemy = FindEnemyWithStats(other.gameObject);
            GetComponent<Collider>().enabled = false;
            Debug.Log(enemy.GetInstanceID());
            IEntityStats stats = enemy.GetComponent<IEntityStats>();
            stats.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        //print("Projectile collision " + other.gameObject + ", Tag:" + other.gameObject.tag);
        //if (!other.CompareTag("Player") && !other.CompareTag("Attack")) Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 _dir)
    {
        Debug.Log("Set projectile Direction");
        dir = new Vector3(_dir.x, _dir.y, _dir.z);
        StartCoroutine(AddForce());
    }

    IEnumerator AddForce()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().AddForce(dir.normalized * speed);
    }


}
