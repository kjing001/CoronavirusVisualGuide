using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int actionPoints;
    public float apTimeInterval = 2f;
    public int totalVirusCount = 1;

    public List<AnimalCell> animalCells;
    int selectedCellID = -1;

    bool isPlaying = true;
    bool isRepulicating;
    float mutationChance;

    // UIs
    public Text actionPointsText;
    public Text promptText;
    public Button replicateButton;
    public Button mutateButton;
    public Button InfectButton;
    public UIProfile profile;
    public UIProgressBar progressBar;

    public Sprite[] numberIcons;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        replicateButton.onClick.AddListener(OnReplicate);
        mutateButton.onClick.AddListener(OnMutate);
        InfectButton.onClick.AddListener(OnInfect);

        StartCoroutine(GenerateActionPoints());
    }

    private void OnInfect()
    {
    }

    private void OnMutate()
    {
        if (selectedCellID == -1)
        {
            promptText.text = "You need to choose a target!";
            return;
        }

        progressBar.ProgressBar(3f, "Mutating... Chance: " + (mutationChance * 100).ToString());

        float randomFloat = Random.Range(0.0f,1.0f); // Create chance
        if (randomFloat < mutationChance)
        {
            promptText.text = "Mutation Succeeded, able to infect more targets.";
        }
        else
        {
            promptText.text = "Mutation failed.";
        }
    }

    private void OnReplicate()
    {
        if (selectedCellID == -1)
        {
            promptText.text = "You need to choose a target!";
            return;
        }
        if (isRepulicating)
            return;

        promptText.text = "";

        foreach (var item in animalCells)
        {
            if (item.id == selectedCellID)
            {
                if (item.virusCount >= item.maxVirusCount)
                {
                    promptText.text = "Can't repulicate more on this host.";
                    return;
                }
                else
                {
                    actionPoints--;
                    actionPointsText.text = actionPoints.ToString();

                    StartCoroutine(ReplicateVirus(item));
                }
            }
        }

    }
    IEnumerator ReplicateVirus(AnimalCell animalCell)
    {
        isRepulicating = true;
        yield return progressBar.ShowProgress(2, "Repulicating...");
        isRepulicating = false;

        // update profile and sprites
        animalCell.virusCount++;
        profile.virusCountText.text = "Virus Count: " + animalCell.virusCount.ToString() + "/" + animalCell.maxVirusCount.ToString();
        animalCell.UpdateVirusSprite();
    }


    public void OnAnimalCellClicked(int i)
    {
        selectedCellID = i;
        UpdateProfile(i);
    }

    private void UpdateProfile(int i)
    {
        foreach (var item in animalCells)
        {
            if (item.id == i)
            {
                profile.nameText.text = "Name: " + item.animalName;
                profile.virusCountText.text = "Virus Count: " + item.virusCount.ToString() + "/" + item.maxVirusCount.ToString();

            }
        }
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
