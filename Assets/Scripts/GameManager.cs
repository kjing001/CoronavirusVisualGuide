using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float money;
    public float immune;
    public float mentalHealth;

    public UnityEvent OnFoodChange;

    [Range (1, 20)]
    public float maxFoodCount = 20;

    public List<Food> myFoods = new List<Food>();

    public void AddFood(Food food)
    {
        myFoods.Add(food);
        OnFoodChange.Invoke();
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // create initial foods
        
    }

    // Update is called once per frame
    void Update()
    {
        TestInputs();
    }

    void TestInputs()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddFood(FoodManager.instance.beef);
        }
    }




}
