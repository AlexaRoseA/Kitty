using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostPerLevel
{
    public int level;
    public int cost;

    CostPerLevel(int level, int cost)
    {
        this.level = level;
        this.cost = cost;
    }

}

public class Purchasable : MonoBehaviour
{
    [SerializeField]
    private int unlockLevel;

    [SerializeField]
    private int currentLevel;

    private int currentCost;

    private bool unlocked = false;
    private bool purchased = false;

    [SerializeField]
    private bool upgradable;

    private bool maxed = false;

    [SerializeField]
    private List<CostPerLevel> itemPricesPerLevel;

    /// <summary>
    /// Sets up the level up cost to be the first item
    /// in the list.
    /// </summary>
    void Start()
    {
        currentCost = itemPricesPerLevel[0].cost;
    }

    /// <summary>
    /// Checks to see if the current player level is greater than or equal
    /// to the level needed to unlock this item.
    /// </summary>
    void CheckIfUnlockable()
    {
        if (Player.PlayerLevel >= unlockLevel)
        {
            unlocked = true;
        }
    }

    /// <summary>
    /// If the player can upgrade and is not maxed out, check
    /// if they have enough money to purchase the upgrade.
    /// 
    /// If so, increase the current level.
    /// If the current level is less than or equal to the max count,
    /// set up the new level up cost. If it's greater than the max,
    /// set the maxed variable to true.
    /// </summary>
    /// <param name="payment"></param>
    public void CheckIfUpgradeItem(int payment)
    {
        if (upgradable && !maxed)
        {
            if (Player.CanIPurchaseSomething(payment))
            {
                currentLevel++;
                if (currentLevel <= itemPricesPerLevel.Count)
                {
                    currentCost = itemPricesPerLevel[currentLevel - 1].cost;
                }
                else
                {
                    maxed = true;
                }

            }
        }
    }

    /// <summary>
    /// Purchases an item and does the proper checks.
    /// 
    /// If the item is not purchased, check if it can be.
    /// If I can purchase the item, set the item as purchased, remove the money.
    /// Then check if it can be upgraded, if so increase the current level and set the next cost.
    /// If not upgradable, set it to be maxed out.
    /// 
    /// If it's already purchased, call the method to check if it can be upgraded instead.
    /// </summary>
    /// <param name="payment"></param>
    public void PurchaseItem(int payment)
    {
        if (!purchased)
        {
            if (Player.CanIPurchaseSomething(payment))
            {
                purchased = true;
                Player.RemoveMoney(payment);

                if (upgradable)
                {
                    currentLevel++;
                    currentCost = itemPricesPerLevel[currentLevel - 1].cost;
                }
                else
                {
                    maxed = true;
                }

            }
        }
        else
        {
            CheckIfUpgradeItem(payment);
        }

    }
}
