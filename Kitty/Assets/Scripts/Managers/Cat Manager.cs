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
    Gardener,
    Designer
}

public class CatManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hatPrefabs;

    public Dictionary<Profession, GameObject> hatDictionary = new Dictionary<Profession, GameObject>();

    void Start()
    {
        hatPrefabs = Resources.LoadAll<GameObject>("Prefab/Hats");
        GenerateDictionaryHats();
    }

    // Update is called once per frame
    void Update()
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
}
