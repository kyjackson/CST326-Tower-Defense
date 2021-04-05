using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int smallEnemyValue = 5;
    public float health = 100;
    public float speed = .25f;

    private float healthPerUnit;
    public Transform healthBar;

    public Path route;
    private Waypoint[] myPathThroughLife;
    private int index = 0;
    private Vector3 nextWaypoint;
    private bool stop = false;

    public GameObject purseManager;
    public PurseManager purse;

    void Start()
    {
        healthPerUnit = 100f / health;
        
        myPathThroughLife = route.path;
        transform.position = myPathThroughLife[index].transform.position;
        Recalculate();

        purse = purseManager.GetComponent<PurseManager>();
    }

    void Update()
    {
        if (!stop)
        {
            if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
            {
                index = index + 1;
                Recalculate();
            }


            Vector3 moveThisFrame = nextWaypoint * Time.deltaTime * speed;
            transform.Translate(moveThisFrame);
        }

    }

    void Recalculate()
    {
        if (index < myPathThroughLife.Length - 1)
        {
            nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;

        }
        else
        {
            stop = true;
        }
    }

    public void Damage()
    {
        health -= 20;
        if (health <= 0)
        {
            Debug.Log($"{transform.name} is Dead");
            Destroy(this.gameObject);

            purse.setBalance(purse.getBalance() + smallEnemyValue);
        }

        float percentage = healthPerUnit * health;
        Vector3 newHealthAmount = new Vector3(percentage / 100f, healthBar.localScale.y, healthBar.localScale.z);
        healthBar.localScale = newHealthAmount;
    }

}
