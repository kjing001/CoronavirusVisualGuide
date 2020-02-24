//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Manager : MonoBehaviour
//{
//    public static Manager instance;

//    public Transform animalArea;
//    public GameObject[] animalPrefabs;

//    private void Awake()
//    {
//        instance = this;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            SpawnRandomAnimal();
//        }
//    }

//    public Vector2 GetRandomPosition()
//    {
//        float randX = Random.Range(animalArea.position.x - animalArea.localScale.x/2, animalArea.position.x + animalArea.localScale.x/2);
//        float randY = Random.Range(animalArea.position.y - animalArea.localScale.y/2, animalArea.position.y + animalArea.localScale.y/2);
//        return new Vector2(randX, randY);
//    }

//    void SpawnRandomAnimal()
//    {
//        int randID = Random.Range(0, animalPrefabs.Length);

//        GameObject.Instantiate(animalPrefabs[randID], GetRandomPosition(), Quaternion.identity);

//    }

//}
