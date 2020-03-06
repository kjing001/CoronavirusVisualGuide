using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int hpMax;
    public int mpMax;
    public int hp;
    public int mp;

    public float money;

    public int speed;
    Camera camera;

    public List<ItemSlot> food = new List<ItemSlot>();

    Dictionary<int, double> itemCooldowns = new Dictionary<int, double>();


    private void Awake()
    {
        instance = this;
        camera = Camera.main;
    }

    public void SetItemCooldown(string cooldownCategory, float cooldown)
    {
        // get stable hash to reduce bandwidth
        int hash = cooldownCategory.GetStableHashCode();

        // save end time
        itemCooldowns[hash] = cooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    /// <param name="index"> index of saleFood </param>
    public void BuyFood(int index)
    {

    }

    public void EatFood(int index)
    {
        if (0 <= index && index < food.Count && food[index].amount > 0)
        {
            // get food data
            FoodItem foodItemData = (FoodItem)food[index].item.data;

            // eat food and increase hp, mp
            foodItemData.Use(this, index);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
