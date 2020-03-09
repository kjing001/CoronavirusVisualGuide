using System;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Food", order = 999)]

public class FoodItem : UsableItem
{
    [Header("Food")]
    public int hp;
    public int mp;

    public override void Use(Player player, int inventoryIndex)
    {
        // always call base function too
        base.Use(player, inventoryIndex);

        // increase health/mana/etc.
        player.hp += hp;
        player.mp += mp;

        // decrease amount in inventory
        FoodItemAndAmount food = player.food[inventoryIndex];
        food.amount--;
        player.food[inventoryIndex] = food;
    }

    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());
        tip.Replace("{USAGEHEALTH}", hp.ToString());
        tip.Replace("{USAGEMANA}", mp.ToString());
        return tip.ToString();
    }
}

[Serializable]
public struct FoodItemAndAmount
{
    public FoodItem item;
    public int amount;

    // constructors
    public FoodItemAndAmount(FoodItem item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }

}
