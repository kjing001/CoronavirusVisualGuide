using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Manages game states and player resources
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnFoodChange;
    public GameObject foodItemPrefab;
    public Transform homeFoodLayout;
    public Transform marketFoodLayout;

    [Range (1, 20)]
    public int foodSlotCount = 20;

    [Header("FoodItems for Sale")]
    public List<FoodItem> saleFoodItems;

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
