using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public const float k_defaultDuration = 2f;
    public const string k_defaultText = "Infection...";
    public RectTransform progress;
    public RectTransform background;
    public Text progressText;
    float width, height;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        width = background.sizeDelta.x;
        height = background.sizeDelta.y;

        background.gameObject.SetActive(false);
        StartCoroutine(ShowProgress(k_defaultDuration, k_defaultText));

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ProgressBar(float duration, string text)
    {
        StartCoroutine(ShowProgress(duration, text));
    }
    public IEnumerator ShowProgress(float duration, string text)
    {
        gameManager.promptText.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
        progress.gameObject.SetActive(true);

        progress.sizeDelta = Vector2.right * height;
        float x = 0;

        progressText.text = text;

        while (x < width - 0.1f)
        {
            x += Time.deltaTime / duration * width;
            progress.sizeDelta = new Vector2(x, height);
            yield return null;
        }

        background.gameObject.SetActive(false);
        gameManager.promptText.gameObject.SetActive(true);
    }


}
