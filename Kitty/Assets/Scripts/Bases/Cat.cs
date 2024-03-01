using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Idle,
    Resting,
    Working,
    Eating,
    Sick,
    Bathroom,
    Playing,
    Pickup
}

public enum Emotion
{
    Neutral,
    Happy,
    Sad,
    Curious,
    Hungry,
    Sleepy,
    Focused,
    Lazy,
    Angry,
    Devious
}

public abstract class Cat : MonoBehaviour
{
    // Variables
    protected Vector2 position;

    protected string catName;

    protected Animator ani;

    protected Type type;

    protected Status status;

    protected Emotion emote;

    protected Profession profession;

    protected Rarity rarity;

    protected float hunger;

    protected float health;

    protected int level;

    [SerializeField]
    protected Sprite boxIcon;

    // Getters/Setters
    public Type CatType { get { return type; } set { type = value; }  }
    public int Level { get { return level; } set { level = value; } }
    public string Name { get { return catName; } set { catName = value; }  }
    public Vector2 Position { get { return position; } set { position = value; } }
    public Status CatStatus { get { return status; } set {  status = value; } }
    public Animator Ani { get { return ani; } }
    public Emotion Emote { get { return emote; }  set { emote = value; } }
    public Profession CatProfession { get { return profession; } set { profession = value; } }
    public Rarity Rarity { get { return rarity; } }
    public Sprite BoxIcon { get { return boxIcon; } }

    // Base
    /// <summary>
    /// Base constructor for a cat (type not known or random)
    /// </summary>
    /// <param name="catName">Name of the cat</param>
    /// <param name="type">Species type</param>
    public Cat(string catName, Type type, Profession profession, GameObject hat = null)
    {
        position = new Vector2(0, 0);
        ani = GetComponent<Animator>();
        this.catName = catName;

        this.status = Status.Idle;
        this.emote = Emotion.Neutral;
        this.profession = profession;

        this.level = 1;
        this.health = 100;
        this.hunger = 100;
    }

    /// <summary>
    /// Base constructor when type is known
    /// </summary>
    /// <param name="catName"></param>
    /// <param name="profession"></param>
    public Cat(string catName, Profession profession, GameObject hat = null)
    {
        position = new Vector2(0, 0);
        ani = GetComponent<Animator>();
        this.catName = catName;

        this.status = Status.Idle;
        this.emote = Emotion.Neutral;
        this.profession = profession;

        this.level = 1;
        this.health = 100;
        this.hunger = 100;
    }

    /// <summary>
    /// Change the status
    /// </summary>
    public void ChangeStatus(Status s)
    {
        status = s;
    }

    /// <summary>
    /// What happens when the player interacts?
    /// </summary>
    public void Interact()
    {
        //pickup code here
    }

    /// <summary>
    /// What showcases when the cat emotes?
    /// </summary>
    public void ShowEmotion(Emotion e)
    {
        emote = e;
    }

    /// <summary>
    /// The general loop of what the cat is doing
    /// </summary>
    public abstract void Lifecycle();

}
