using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Type
{
    Orange,
    White,
    Siamese,
    LongHair,
    Tabby
}

public enum Profession
{
    Labor,
    Lumber,
    Miner,
    Manager,
    Banker,
    Designer,
    Gardener,
}

public class CatManager : MonoBehaviour
{
    // ALL Hats possible
    [SerializeField]
    private GameObject[] hatPrefabs;

    // ALL Cats possible
    [SerializeField]
    private GameObject[] catPrefabs;

    // All hats dictionary
    public Dictionary<Profession, GameObject> hatDictionary = new Dictionary<Profession, GameObject>();

    // All cats dictionary
    public Dictionary<Type, GameObject> catDictionary = new Dictionary<Type, GameObject>();

    // All professions unlocked by level
    public List<Profession> unlockedProfessions = new List<Profession>();

    // Level requirements to unlock professions
    [SerializeField]
    private SortedList<Profession, int> levelReqPer = new SortedList<Profession, int>();

    // Cats owned at the ranch
    List<GameObject> catsOwned = new List<GameObject>();

    [SerializeField]
    // Cats unlocked to purchase for the ranch
    List<GameObject> catsUnlocked = new List<GameObject>();

    void Start()
    {
        hatPrefabs = Resources.LoadAll<GameObject>("Prefab/Hats");
        catPrefabs = Resources.LoadAll<GameObject>("Prefab/Cats");
        GenerateDictionaryHats();
        GenerateDictionaryCat();

        PopulateLevels();
        UnlockCat(Type.Siamese);

        CreateNonSpecificCat();
    }

    /// <summary>
    /// Creates a random cat based on what
    /// professions the player has unlocked with the cats
    /// they have unlocked.
    /// </summary>
    public void CreateNonSpecificCat()
    {
        // Generate a random cat
        GameObject catPrefab = catsUnlocked[UnityEngine.Random.Range(0, catsUnlocked.Count - 1)];
        GameObject catItself = Instantiate(catPrefab);

        // Generate a random hat, add as child to cat
        Profession ranProfession = unlockedProfessions[UnityEngine.Random.Range(0, unlockedProfessions.Count - 1)];
        hatDictionary.TryGetValue(ranProfession, out GameObject hatGrab);

        Instantiate(hatGrab, catItself.transform.GetChild(1).transform);

        // Add cat to cats owned
        catsOwned.Add(catItself);
    }

    /// <summary>
    /// Unlocks a specific cat based on the type.
    /// Has an optional parameter to ignore if it's unlocked
    /// or not already.
    /// </summary>
    /// <param name="type">Cat type</param>
    /// <param name="unlocked">Should the cat be unlocked already? Default is true</param>
    public void CreateSpecificCatRandomHat(Type type, bool unlocked = true)
    {
        GameObject catPrefab = null;
        GameObject catItself = null;

        if (unlocked)
        {
            catPrefab = catsUnlocked.Where(obj => obj.name == type.ToString()).SingleOrDefault();
            if(catPrefab == null)
            {
                Debug.Log("You have not unlocked this cat yet. Creating random cat instead.");
                catPrefab = catsUnlocked[UnityEngine.Random.Range(0, catsUnlocked.Count - 1)];
            }
            catItself = Instantiate(catPrefab);
        } else
        {
            Debug.Log("Bypassing cat unlock need.");
            catPrefab = catPrefabs.Where(obj => obj.name == type.ToString()).SingleOrDefault();
            catItself = Instantiate(catPrefab);
        }

        // Generate a random hat, add as child to cat
        Profession ranProfession = unlockedProfessions[UnityEngine.Random.Range(0, unlockedProfessions.Count - 1)];
        hatDictionary.TryGetValue(ranProfession, out GameObject hatGrab);
        
        Instantiate(hatGrab, catItself.transform.GetChild(1).transform);

        // Add cat to cats owned
        catsOwned.Add(catItself);
    }

    /// <summary>
    /// Creates a specific cat with specific hat
    /// </summary>
    /// <param name="type">Type of cat</param>
    /// <param name="pro">Cat profession</param>
    /// <param name="unlockedCat">Require cat to be unlocked? Default is true</param>
    /// <param name="unlockedHat">Require hat to be unlocked? Default is true</param>
    public void CreateSpecificCatSpecificHat(Type type, Profession pro, bool unlockedCat = true, bool unlockedHat = true)
    {
        GameObject catPrefab = null;
        GameObject catItself = null;

        if (unlockedCat)
        {
            catPrefab = catsUnlocked.Where(obj => obj.name == type.ToString()).SingleOrDefault();
            if (catPrefab == null)
            {
                Debug.Log("You have not unlocked this cat yet. Creating random cat instead.");
                catPrefab = catsUnlocked[UnityEngine.Random.Range(0, catsUnlocked.Count - 1)];
            }
            catItself = Instantiate(catPrefab);
        }
        else
        {
            Debug.Log("Bypassing cat unlock need.");
            catPrefab = catPrefabs.Where(obj => obj.name == type.ToString()).SingleOrDefault();
            catItself = Instantiate(catPrefab);
        }

        GameObject hatPrefab = null;

        if (unlockedHat)
        {
            if(unlockedProfessions.Contains(pro))
            {
                hatDictionary.TryGetValue(pro, out hatPrefab);
                Instantiate(hatPrefab, catItself.transform.GetChild(1).transform);
            }
            else
            {
                Debug.Log("You have not unlocked this hat yet. Generating random hat instead.");

                Profession ranProfession = unlockedProfessions[UnityEngine.Random.Range(0, unlockedProfessions.Count - 1)];

                hatDictionary.TryGetValue(ranProfession, out GameObject hatGrab);
                Instantiate(hatGrab, catItself.transform.GetChild(1).transform);
            }
        }
        else
        {
            Debug.Log("Bypassing hat unlock need.");
            hatPrefab = hatPrefabs.Where(obj => obj.name == type.ToString()).SingleOrDefault();
            Instantiate(hatPrefab, catItself.transform.GetChild(1).transform);
        }

        // Add cat to cats owned
        catsOwned.Add(catItself);
    }

    /// <summary>
    /// Updates all cats
    /// </summary>
    public void Update()
    {
        foreach(GameObject cat in catsOwned)
        {
            cat.GetComponent<Cat>().Lifecycle();
        }
    }

    /// <summary>
    /// Unlocks cat type
    /// </summary>
    /// <param name="type"></param>
    public void UnlockCat(Type type)
    {
        catDictionary.TryGetValue(type, out GameObject cat);
        if(cat != null)
        {
            catsUnlocked.Add(cat);
        }
    }

    /// <summary>
    /// Grabs the hat prefabs and assigns them to a dictionary 
    /// that links the profession enum to the gameobject.
    /// </summary>
    private void GenerateDictionaryHats()
    {
        foreach(GameObject hatObj in hatPrefabs)
        {
           bool foundInEnum = System.Enum.TryParse(typeof(Profession), hatObj.name, out object pro);

            if (foundInEnum != false)
            {
                hatDictionary.Add((Profession)pro, hatObj);
            }
        }
    }

    /// <summary>
    /// Grabs the cat prefabs and assigns them to a dictionary 
    /// that links the profession enum to the gameobject.
    /// </summary>
    private void GenerateDictionaryCat()
    {
        foreach (GameObject catObj in catPrefabs)
        {
            bool foundInEnum = System.Enum.TryParse(typeof(Type), catObj.name, out object type);

            if (foundInEnum != false)
            {
                catDictionary.Add((Type)type, catObj);
            }
        }
    }

    /// <summary>
    /// Populates the level requirements per profession and then 
    /// calls the unlock to start off the cycle
    /// </summary>
    private void PopulateLevels()
    {
        levelReqPer.Add(Profession.Labor, 1);
        levelReqPer.Add(Profession.Lumber, 2);
        levelReqPer.Add(Profession.Miner, 3);
        levelReqPer.Add(Profession.Manager, 5);
        levelReqPer.Add(Profession.Banker, 6);
        levelReqPer.Add(Profession.Designer, 10);
        levelReqPer.Add(Profession.Gardener, 12);

        UnlockProfessionForMyLevel();
    }

    /// <summary>
    /// Unlocks professions based on the level
    /// </summary>
    private void UnlockProfessionForMyLevel()
    {
        foreach (Profession p in levelReqPer.Keys)
        {
            // Skip if already in unlocked professions
            if (unlockedProfessions.Contains(p))
            {
                break;
            }

            levelReqPer.TryGetValue(p, out int level);

            Debug.Log(Player.PlayerLevel + " comparing to " + level);

            if (level <= Player.PlayerLevel)
            {
                unlockedProfessions.Add(p);
            } else
            {
                return;
            }
        }
    }
}
