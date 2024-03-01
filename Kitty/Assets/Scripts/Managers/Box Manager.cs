using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Ultra
}
public class BoxManager : MonoBehaviour
{
    private int numOfBoxesToOpen = 0;

    [SerializeField] private List<GameObject> boxList;

    Dictionary<Rarity, GameObject> boxRarityDictionary = new Dictionary<Rarity, GameObject>();

    CatManager catManager;

    bool freeToOpenNextBox = true;

    public bool FreeToOpenNextBox { get {  return freeToOpenNextBox; } set {  freeToOpenNextBox = value; } }

    public void Setup()
    {
        catManager = GameObject.Find("Cat Manager").GetComponent<CatManager>();

        boxRarityDictionary.Add(Rarity.Common, boxList[0]);
        boxRarityDictionary.Add(Rarity.Uncommon, boxList[1]);
        boxRarityDictionary.Add(Rarity.Rare, boxList[2]);
        boxRarityDictionary.Add(Rarity.Ultra, boxList[3]);

        //OpenBoxRandomCatMethod();
    }

    /// <summary>
    /// Determine a random rarity for the box and a random
    /// cat to go inside of it. 
    /// 
    /// Need to add code that checks to make sure the box rarity matches the cat.
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenBoxRandomCat()
    {
        freeToOpenNextBox = false;
        Rarity r = DetermineBoxRarity();
        boxRarityDictionary.TryGetValue(r, out GameObject obj);

        GameObject c = catManager.CreateNonSpecificCat();
        c.SetActive(false);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = c.GetComponent<Cat>().BoxIcon;

        // add symbol code here for what type it is once ui is created

        yield return new WaitForSeconds(0.001f);

        GameObject currentBox = Instantiate(obj, transform);
        currentBox.GetComponent<Box>().CatInside = c;
    }

    /// <summary>
    /// Used in button
    /// </summary>
    void OpenBoxRandomCatMethod()
    {
        StartCoroutine(OpenBoxRandomCat());
    }

    private Rarity DetermineBoxRarity()
    {
        double chance = GetRandomDouble(0, 1.01);

        Debug.Log("Chance: " + chance);

        if (IsWithin(chance, 0, 0.6f))
        {
            return Rarity.Common;
        }

        if (IsWithin(chance, 0.6f, 0.9f))
        {
            return Rarity.Uncommon;
        }

        if (IsWithin(chance, 0.9f, 0.98f))
        {
            return Rarity.Rare;
        }

        return Rarity.Ultra;
    }

    /// <summary>
    /// Returns true/false if the current value is within min/max inclusively
    /// </summary>
    /// <param name="value">Value to check within minimum/maximum.</param>
    /// <param name="minimum">Minimum compare value.</param>
    /// <param name="maximum">Maximum compare value.</param>
    /// <returns></returns>
    private bool IsWithin(double value, double minimum, double maximum)
    {
        return value >= minimum && value <= maximum;
    }

    /// <summary>
    /// Custom RandomDouble method that limits to an interval ending.
    /// Returns a randomDouble within a specified range and interval.
    /// Uses steps to make sure that generated "randomness" is valid
    /// within the given interval
    /// </summary>
    /// <param name="min">The minimum value that the double can be.</param>
    /// <param name="max">The maximum value that the double can be.</param>
    /// <param name="interval">The limit interval, default to 0.05 if no interval is inputted.</param>
    /// <returns></returns>
    private double GetRandomDouble(double min, double max, double interval = 0.05)
    {
        // Calculates the number of steps between min and max
        // + 1 makes sure to include the max as a step
        int steps = (int)((max - min) / interval) + 1;

        // Grab one of the steps randomly from 0 to step variable
        double randomStep = Random.Range(0, steps);

        // Returns the double value: starting from the min value
        // and moving by randomStep steps by the interval inputted
        return min + randomStep * interval;
    }

}
