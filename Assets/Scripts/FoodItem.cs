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
        ItemSlot slot = player.food[inventoryIndex];
        slot.DecreaseAmount(1);
        player.food[inventoryIndex] = slot;
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
