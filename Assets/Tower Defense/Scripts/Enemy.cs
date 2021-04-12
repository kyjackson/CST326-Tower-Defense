using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject buttonRestart;
    public Path route;
    private Waypoint[] myPathThroughLife;
    public int coinWorth;
    public float health = 100;
    public float speed = .25f;
    public float hitAmount = 10;
    private int index = 0;
    private Vector3 nextWaypoint;
    private bool stop = false;
    private float healthPerUnit;
    public List<Tower> currentTowers;
    public Tower currentTarget;
    public AudioSource deathSound;
    public GameObject death;
    public PurseManager purse;
    

    public Transform healthBar;

    public UnityEvent DeathEvent;

    void Start()
    {
        purse = GameObject.Find("PurseManager").GetComponent<PurseManager>();
        buttonRestart = GameObject.Find("Restart");
        
        death = GameObject.Find("death");
        deathSound = death.GetComponent<AudioSource>();
        healthPerUnit = 100f / health;

        myPathThroughLife = route.path;
        transform.position = myPathThroughLife[index].transform.position;
        Recalculate();
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

            if (purse.totalKilled >= 7)
            {
                buttonRestart.SetActive(true);
            }
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
        Damage(20);
    }


    public void Damage(float hitAmount)
    {
        health -= hitAmount;
        
        if (health <= 0)
        {
            deathSound.Play();

            Debug.Log($"{transform.name} is Dead");
            DeathEvent.Invoke();
            DeathEvent.RemoveAllListeners();

            Destroy(this.gameObject);
            purse.totalKilled++;
            Debug.Log("enemies killed = "+purse.totalKilled);
        }

        float percentage = healthPerUnit * health;
        Vector3 newHealthAmount = new Vector3(percentage / 100f, healthBar.localScale.y, healthBar.localScale.z);
        healthBar.localScale = newHealthAmount;
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    //laser.enabled = true;
    //    if (collider.GetComponent<Tower>() != null)
    //    {
    //        Tower newTower = collider.GetComponent<Tower>();
    //        newTower.DeathEvent.AddListener(delegate { BookKeeping(newTower); });
    //        currentTowers.Add(newTower);
    //        if (currentTarget == null)
    //        {
    //            currentTarget = newTower;
    //        }
    //        currentTarget.Damage(hitAmount);
    //        Debug.Log("Tower health: " + currentTarget.health);
    //    }
    //}

    //void OnTriggerExit(Collider collider)
    //{
    //    if (collider.GetComponent<Tower>() != null)
    //    {
    //        Tower oldTower = collider.GetComponent<Tower>();
    //        BookKeeping(oldTower);
    //    }
    //}

    //void BookKeeping(Tower tower)
    //{
    //    currentTowers.Remove(tower);

    //    if (currentTowers.Count > 0)
    //    {
    //        //laser.enabled = true;
    //        currentTarget = currentTowers[0];
    //    }
    //    else
    //    {
    //        //laser.enabled = false;
    //        currentTarget = null;
    //    }
    //    //currentTarget = (currentEnemies.Count > 0) ? currentEnemies[0] : null;

    //}
}
