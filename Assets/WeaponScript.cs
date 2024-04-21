using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private List<int> hitEnemies; // Array to keep track of enemies hit by the weapon
    private Collider weaponCollider;
    public GameObject wielder = null;
    public GameObject[] weapons;
    public int currSelectedWeapon = 0;
    public string againstTag;
    public int damage;
    public int staminaCost = 0;
    public bool heavyAttacking;
    public GameObject magicProj;


    private const int NUM_WEAPONS = 3; 
    bool[] weaponAvailable; 
    // Start is called before the first frame update
    void Start()
    {
        hitEnemies = new List<int>();
        weaponCollider = GetComponentInChildren<Collider>();
        weaponCollider.enabled = false;
        heavyAttacking = false;
        weaponAvailable = new bool[NUM_WEAPONS];
        weaponAvailable[0] = true; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeWeapon(int x) 
    {
        if (!weaponAvailable[x]) return; 
        switch (x)
        {
            case 0:
                damage = 12;
                staminaCost = 10;
                break;
            case 1:
                damage = 6;
                staminaCost = 5;
                break;
        }
                
        weapons[currSelectedWeapon].SetActive(false);
        currSelectedWeapon = x; 
        weapons[x].SetActive(true);
        weaponCollider = GetComponentInChildren<Collider>();
    }

    private GameObject FindEntityWithStats(GameObject obj)
    {
        IEntityStats stats = obj.GetComponent<IEntityStats>();
        if (stats != null)
        {
            return obj;
        }
        else
        {
            return FindEntityWithStats(obj.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected " + other.gameObject.tag);
        if (other.gameObject.tag == againstTag)
        {
            GameObject enemy = FindEntityWithStats(other.gameObject);
            if (!hitEnemies.Contains(enemy.GetInstanceID()))
            {
                hitEnemies.Add(enemy.GetInstanceID());
                IEntityStats stats = enemy.GetComponent<IEntityStats>();
                int damageToEnemy = damage;
                if (heavyAttacking)
                {
                    damageToEnemy += 5;
                    print("heavy");
                }
                stats.TakeDamage(damageToEnemy);
            }
        }
        else if (other.gameObject.tag == "Breakable")
        {
            BreakablePropScript breakableScript = other.transform.parent.GetComponent<BreakablePropScript>();
            if (!breakableScript.isBroken)
            {
                breakableScript.Break(other.ClosestPoint(transform.position));
            }
        }
    }

    public void SetWielder(GameObject wielder)
    {
        this.wielder = wielder;
    }

    public int HitEnemy(int enemyID)
    {
        hitEnemies.Append(enemyID);
        int damageToEnemy = damage;
        if (heavyAttacking)
        {
            damageToEnemy *= 2;
            print("heavy");
        }
        return damageToEnemy;
    }


    public void OnAttackBegin()
    {
        hitEnemies = new List<int>();
        if (currSelectedWeapon != 2)
        {
            weaponCollider.enabled = true;
        } else if (currSelectedWeapon == 2)
        {
            GameObject proj = Instantiate(magicProj, weapons[currSelectedWeapon].transform.position + new Vector3(0, .5f, 0), new Quaternion());
            Vector3 projDir = (Quaternion.Euler(0.0f, wielder.transform.eulerAngles.y, 0.0f) * Vector3.forward).normalized;
            Debug.Log(projDir);
            proj.GetComponent<ProjectileScript>().SetDirection(projDir);
/*            if (heavyAttacking)
            {
                proj.GetComponent<SphereCollider>().radius *= 2;
            }*/
        }
    }

    public void OnAttackEnd()
    {
        weaponCollider.enabled = false;
        heavyAttacking = false; 
    }

    public float GetStaminaCost()
    {
        return staminaCost;
    }

    public void EnableWeapon(int x)
    {
        weaponAvailable[x] = true;
    }
}
