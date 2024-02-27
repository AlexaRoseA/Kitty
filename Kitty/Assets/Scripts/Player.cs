using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string username;
    private int exp;
    private int level;
    public int PlayerLevel { get { return level; } }
    public string Username { get { return username; } }

    /// <summary>
    /// Basic player constructor that passes in username
    /// </summary>
    /// <param name="username"></param>
    public Player(string username)
    {
        this.username = username;
        this.level = 1;
        this.exp = 0;
    }

    void Update()
    {
        
    }
}
