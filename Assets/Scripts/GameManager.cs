using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[Serializable]
public struct News
{
    public string headline;
    public string content;

    public News(string h, string c)
    {
        headline = h;
        content = c;
    }
}



/// <summary>
/// Manages game states and player resources
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform homeFoodLayout;
    public Transform marketFoodLayout;

    [Range (1, 20)]
    public int foodSlotCount = 20;

    [Header("Food for Sale")]
    public List<FoodItemAndAmount> saleFood;

    [Header("News on computer")]
    public List<News> news;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {


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
            //AddFoodToMyFoods(FoodDB.beef);
        }
    }




}
