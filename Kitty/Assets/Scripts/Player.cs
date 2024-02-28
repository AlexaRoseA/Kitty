using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static string username;
    private int exp;
    private static int coins = 0;

    private static int level;
    public static int PlayerLevel { get { return level; } }
    public static string Username { get { return username; } }
    public static int Coins { get { return coins; } }

    /// <summary>
    /// Basic player constructor that passes in username
    /// </summary>
    /// <param name="username"></param>
    public Player(string username)
    {
        Player.username = username;
        Player.level = 1;
        this.exp = 0;
    }

    void Update()
    {
        
    }

    void IncreaseExp(int amount)
    {

    }

    public static void AddMoney(int amount)
    {
        Player.coins += amount;
    }

    public static void RemoveMoney(int amount)
    {
        Player.coins -= amount;
    }


    public static bool CanIPurchaseSomething(int cost)
    {
        if(Player.Coins - cost >= 0)
        {
            return true;
        }
        return false;
    }

}
