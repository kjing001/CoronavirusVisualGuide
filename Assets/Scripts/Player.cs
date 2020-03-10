using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public const int startMoney = 100;
    public const int startHp = 100;
    public const int startMP = 100;
    public int hpMax;
    public int mpMax;
    public int hp = startHp;
    public int mp = startMP;
    public float money = startMoney;

    public int speed;

    public List<FoodItemAndAmount> food = new List<FoodItemAndAmount>();

    Dictionary<int, double> itemCooldowns = new Dictionary<int, double>();
    GameManager gameManager;

    private void Awake()
    {
        instance = this;        
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
        gameManager = GameManager.instance;
    }

    int TryGetFood(FoodItem foodItem)
    {
        for (int i = 0; i < food.Count; i++)
        {
            if (foodItem.name == food[i].item.name)
                return i;
        }
        return -1;
    }    

    /// <param name="index"> index of saleFood </param>
    public void BuyFood(int index, int amount)
    {
        if(gameManager == null)
        {
            print("nn");
        }

        FoodItem foodItem = gameManager.saleFood[index].item;

        if (1 <= amount && amount <= foodItem.maxStack)
        {
            long price = foodItem.price * amount;

            // enough gold and enough space in inventory?
            if (money >= price)
            {
                // pay for it, add to food
                money -= price;

                // do we already have this food?
                int i = TryGetFood(foodItem);
                if (i != -1)
                {
                    // increase its amount
                    FoodItemAndAmount t = food[i];
                    t.amount += amount;
                    food[i] = t;
                }
                else
                {
                    food.Add(new FoodItemAndAmount(foodItem, amount));
                }
            }
        }


    }

    public void EatFood(int index)
    {
        if (0 <= index && index < food.Count && food[index].amount > 0)
        {
            // get food data
            FoodItem foodItemData = food[index].item;

            // eat food and increase hp, mp
            foodItemData.Use(this, index);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
