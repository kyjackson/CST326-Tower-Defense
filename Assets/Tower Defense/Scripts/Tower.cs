using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    public List<Enemy> currentEnemies;
    public Enemy currentTarget;
    public Transform turret;
    public float health = 100;
    public UnityEvent DeathEvent;
    //private delegate void enemySubscription(Enemy enemy);

    private LineRenderer laser;
    public float hitAmount = 10;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        //laser.positionCount = 2;
        laser.SetPosition(0, turret.transform.position);
        laser.enabled = false;
    }

    void Update()
    {
        if (currentTarget)
        {

            currentTarget.Damage(hitAmount * Time.deltaTime);
            laser.SetPosition(1, currentTarget.transform.position);
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        laser.enabled = true;
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy newEnemy = collider.GetComponent<Enemy>();
            newEnemy.DeathEvent.AddListener(delegate { BookKeeping(newEnemy); });
            currentEnemies.Add(newEnemy);
            if (currentTarget == null)
            {
                currentTarget = newEnemy;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy oldEnemy = collider.GetComponent<Enemy>();
            BookKeeping(oldEnemy);
        }
    }

    void BookKeeping(Enemy enemy)
    {
        currentEnemies.Remove(enemy);

        if(currentEnemies.Count > 0)
        {
            laser.enabled = true;
            currentTarget = currentEnemies[0];
        } 
        else
        {
            laser.enabled = false;
            currentTarget = null;
        }
        //currentTarget = (currentEnemies.Count > 0) ? currentEnemies[0] : null;

    }

    public void Damage(float hitAmount)
    {
        health -= hitAmount;
        if (health <= 0)
        {


            Debug.Log($"{transform.name} is Dead");
            DeathEvent.Invoke();
            DeathEvent.RemoveAllListeners();

            Destroy(this.gameObject);
        }
    }
}
