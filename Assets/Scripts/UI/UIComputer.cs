using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComputer : MonoBehaviour
{
    public Button newsButton;
    public Button entertainButton;
    public Button closeNewsPageButton;

    public GameObject newsPanel;
    public GameObject newsPage;
    public GameObject entertainPanel;

    public GameObject newsList;

    public Text newsPageHeadline;
    public Text newsPageContent;
    public Text[] newsHeadlines;
    public Button[] newsSlots;

    public float selectedHeightIncrease = 30;
    float unselectedHeight;
    float selectedHeight;
    float buttonWidth;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;

        RectTransform rt = entertainButton.GetComponent<RectTransform>();
        buttonWidth = rt.sizeDelta.x;
        unselectedHeight = rt.sizeDelta.y;
        selectedHeight = unselectedHeight + selectedHeightIncrease;

        newsButton.onClick.AddListener(OnNewsClicked);
        entertainButton.onClick.AddListener(OnEntertainmentClicked);
        closeNewsPageButton.onClick.AddListener(OnCloseNewsPageClicked);

        newsHeadlines = newsList.transform.GetComponentsInChildren<Text>();
        newsSlots = newsList.transform.GetComponentsInChildren<Button>();

        for (int i = 0; i < newsSlots.Length; i++)
        {
            int iCopy = i;
            newsSlots[i].onClick.AddListener(() => OnNewSlotClicked(iCopy));
        }

        // show default news
        OnNewsClicked();
        transform.GetChild(0).gameObject.SetActive(false);


    }
    void OnCloseNewsPageClicked()
    {
        newsPage.SetActive(false);
        newsList.SetActive(true);
        LoadNewsList();
    }

    void OnNewSlotClicked(int id)
    {
        newsList.SetActive(false);
        newsPage.SetActive(true);
        newsPageHeadline.text = manager.news[id].headline;
        newsPageContent.text = manager.news[id].content;
    }

    void OnNewsClicked()
    {
        newsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, selectedHeight);
        entertainButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, unselectedHeight);

        entertainPanel.SetActive(false);
        newsPanel.SetActive(true);
        newsPage.SetActive(false);
        newsList.SetActive(true);
        LoadNewsList();
    }
    
    void OnEntertainmentClicked()
    {
        entertainButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, selectedHeight);
        newsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, unselectedHeight);

        newsPanel.SetActive(false);
        entertainPanel.SetActive(true);
    }

    void LoadNewsList()
    {
        for (int i = 0; i < newsHeadlines.Length; i++)
        {
            newsHeadlines[i].text = manager.news[i].headline;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
