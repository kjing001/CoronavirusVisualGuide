﻿// only usable items need minLevel and usage functions
using System.Text;
using UnityEngine;

public abstract class UsableItem : ScriptableItem
{
    [Header("Cooldown")]
    public float cooldown; // potion usage interval, etc.
    [Tooltip("Cooldown category can be used if different potion items should share the same cooldown. Cooldown applies only to this item name if empty.")]
#pragma warning disable CS0649 // Field never assigned to
    [SerializeField] string _cooldownCategory; // leave empty for itemname based cooldown. fill in for category.
#pragma warning restore CS0649 // Field never assigned to
    public string cooldownCategory =>
        // defaults to per-item-name cooldown if empty. otherwise category.
        string.IsNullOrWhiteSpace(_cooldownCategory) ? name : _cooldownCategory;


    // [Server] Use logic: make sure to call base.Use() in overrides too.
    public virtual void Use(Player player, int inventoryIndex)
    {
        // start cooldown (if any)
        // -> no need to set sync dict dirty if we have no cooldown
        if (cooldown > 0)
            player.SetItemCooldown(cooldownCategory, cooldown);
    }

    // [Client] OnUse Rpc callback for effects, sounds, etc.
    // -> can't pass slotIndex because .Use might clear it before getting here already
    public virtual void OnUsed(Player player) { }

    // tooltip /////////////////////////////////////////////////////////////////
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());
        return tip.ToString();
    }
}
