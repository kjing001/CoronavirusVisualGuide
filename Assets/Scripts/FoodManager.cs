using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Food
{
    public int foodID;
    public string name;
    public float hp;
    public float mp;
    public float price;
    public string info;
}

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;
    public List<Food> foodsForSale = new List<Food>();

    public Food chicken = new Food()
    {
        foodID = 0,
        name = "Chicken",
        hp = 10,
        mp = 10,
        price = 8,
        info = "Good source of protein."
    };

    public Food fruit = new Food()
    {
        foodID = 1,
        name = "Fruit",
        hp = 2,
        mp = 5,
        price = 2,
        info = "Provide necessary vitamins."
    };
    
    public Food vegetables = new Food()
    {
        foodID = 2,
        name = "Vegetables",
        hp = 5,
        mp = 3,
        price = 3,
        info = "Fresh from local market."
    };

    public Food beef = new Food()
    {
        foodID = 3,
        name = "Prime Beef",
        hp = 14,
        mp = 10,
        price = 10,
        info = "Prime wagyu beef, very delicious."
    };

    public Food egg = new Food()
    {
        foodID = 4,
        name = "Egg",
        hp = 2,
        mp = 2,
        price = 1,
        info = ""
    };

    public Food water = new Food()
    {
        foodID = 5,
        name = "Water",
        hp = 2,
        mp = 2,
        price = 1,
        info = "Experts recommend having drinking more water everyday."
    };


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foodsForSale.Add(chicken);
        foodsForSale.Add(fruit);
        foodsForSale.Add(vegetables);
        foodsForSale.Add(beef);
        foodsForSale.Add(egg);
        foodsForSale.Add(water);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
