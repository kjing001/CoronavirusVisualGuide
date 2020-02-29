using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float money;
    public float hp;
    public float mp;

    public UnityEvent OnFoodChange;
    public GameObject foodItemPrefab;
    public Transform homeFoodLayout;
    public Transform marketFoodLayout;

    [Range (1, 20)]
    public float maxFoodCount = 20;

    public List<Food> myFoods = new List<Food>();
    public List<Food> marketFoods = new List<Food>();

    public void AddFoodToMyFoods(Food food)
    {
        myFoods.Add(food);

        // add an item to the food panel at home
        UIFoodItem foodItem = Instantiate(foodItemPrefab, homeFoodLayout).GetComponent<UIFoodItem>();
        foodItem.food = food;
        foodItem.ShowFoodItemInfo();
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // initial food information
        marketFoods.Add(FoodDB.chicken);
        marketFoods.Add(FoodDB.fruit);
        marketFoods.Add(FoodDB.vegetables);
        marketFoods.Add(FoodDB.beef);
        marketFoods.Add(FoodDB.egg);
        marketFoods.Add(FoodDB.water);

        // create ui food items on the market
        for (int i = 0; i < marketFoods.Count; i++)
        {
            UIFoodItem foodItem = Instantiate(foodItemPrefab, marketFoodLayout).GetComponent<UIFoodItem>();
            foodItem.food = marketFoods[i];
            //foodItem.ShowFoodItemInfo();
        }

    }

    // Update is called once per frame
    void Update()
    {
        TestInputs();
    }

    void TestInputs()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddFoodToMyFoods(FoodDB.beef);
        }
    }




}
