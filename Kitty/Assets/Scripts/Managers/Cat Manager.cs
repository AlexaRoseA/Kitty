using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    private GameObject[] hatPrefabs;

    public Dictionary<Profession, GameObject> hatDictionary = new Dictionary<Profession, GameObject>();

    public List<Profession> unlockedProfessions = new List<Profession>();

    [SerializeField]
    private SortedList<Profession, int> levelReqPer = new SortedList<Profession, int>();


    void Start()
    {
        hatPrefabs = Resources.LoadAll<GameObject>("Prefab/Hats");
        GenerateDictionaryHats();
        PopulateLevels();
    }

    public void CreateRandomCat()
    {

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
