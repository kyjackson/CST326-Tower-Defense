using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health health;
    public Camera camera;
    public Path route;
    private Waypoint[] myPathThroughLife;
    private Score purse;

    public int coinWorth;
    public float speed = .25f;
    private int index = 0;
    private Vector3 nextWaypoint;
    private bool stop = false;

    void Awake()
    {
        health = GetComponent<Health>();
        purse = camera.GetComponent<Score>();

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
        }

        // attack enemy if user clicks on them
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    health.hp -= 10;
                    Debug.Log(hit.collider.name + " hit\n Health: " + this.health.hp);

                    if (health.hp <= 0)
                    {
                        Debug.Log(gameObject.name + " destroyed");                       
                        Destroy(gameObject);

                        purse.score = purse.score + this.coinWorth;
                        Debug.Log("New Score: " + purse.score + " coins");
                    }
                }
                
            }
        }

        
    }

    void Recalculate()
    {
        if (index < myPathThroughLife.Length -1)
        {
            nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;

        }
        else
        {
            stop = true;
        }
    }

    
}
