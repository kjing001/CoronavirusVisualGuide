using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int actionPoints;
    public float apTimeInterval = 2f;
    public int virusCount = 1;


    bool isPlaying = true;


    // UIs
    public Text actionPointsText;
    public Button replicateButton;
    public Button mutateButton;
    public Button transmitButton;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateActionPoints());
    }

    public void OnAnimalCellClicked(int i)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GenerateActionPoints()
    {
        while (isPlaying)
        {
            actionPoints++;
            actionPointsText.text = actionPoints.ToString();
            yield return new WaitForSeconds(apTimeInterval);
        }        
    }

}
