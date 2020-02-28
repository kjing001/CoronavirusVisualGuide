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

public class FoodDB : MonoBehaviour
{
    public static Food chicken = new Food()
    {
        foodID = 0,
        name = "Chicken",
        hp = 10,
        mp = 10,
        price = 8,
        info = "Good source of protein."
    };

    public static Food fruit = new Food()
    {
        foodID = 1,
        name = "Fruit",
        hp = 2,
        mp = 5,
        price = 2,
        info = "Provide necessary vitamins."
    };
    
    public static Food vegetables = new Food()
    {
        foodID = 2,
        name = "Vegetables",
        hp = 5,
        mp = 3,
        price = 3,
        info = "Fresh from local market."
    };

    public static Food beef = new Food()
    {
        foodID = 3,
        name = "Prime Beef",
        hp = 14,
        mp = 10,
        price = 10,
        info = "Prime wagyu beef, very delicious."
    };

    public static Food egg = new Food()
    {
        foodID = 4,
        name = "Egg",
        hp = 2,
        mp = 2,
        price = 1,
        info = ""
    };

    public static Food water = new Food()
    {
        foodID = 5,
        name = "Water",
        hp = 2,
        mp = 2,
        price = 1,
        info = "Experts recommend having drinking more water everyday."
    };


}
