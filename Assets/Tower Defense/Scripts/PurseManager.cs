using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurseManager : MonoBehaviour
{
    public int totalKilled = 0;
    public int coins = 0;
    private int defaultCoins = 40;

    void Start()
    {
        coins = defaultCoins;
    }

    public int getBalance()
    {
        return coins;
    }

    public int setBalance(int newBalance)
    {
        coins = newBalance;
        Debug.Log("New balance: " + coins);
        return coins;
    }

    public bool hasAmount(int amount)
    {
        if(coins >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
