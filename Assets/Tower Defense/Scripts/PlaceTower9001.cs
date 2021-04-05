using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower9001 : MonoBehaviour
{
    public GameObject Tower;
    public GameObject World;
    public GameObject purseManager;    
    private PurseManager purse;

    private int towerValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        purse = purseManager.GetComponent<PurseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "TowerSpot")
                {
                    if (purse.hasAmount(towerValue))
                    {
                        //Book keeping
                        // if good 
                        hit.transform.gameObject.SetActive(false);
                        PlaceTower(hit.transform.position);
                        purse.setBalance(purse.getBalance() - towerValue);
                        print("Tower placed");
                    }
                }
            }
        }

        //raycast
        //hitplace
        //purse script $$$$
        //instantiate a tower

    }

    void PlaceTower(Vector3 position)
    {
        //Book keeping
        Instantiate(Tower, position, Quaternion.identity, World.transform);
    }
}
