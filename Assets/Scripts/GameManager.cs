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
    public float maxFood = 20;

    private float m_Food = 10;
    public float food
    {
        get { return m_Food; }
        set
        {
            m_Food = value;
            OnFoodChange.Invoke();
        }
    }    

    public List<Food> foods = new List<Food>();

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
            food++;
        }
    }

}
