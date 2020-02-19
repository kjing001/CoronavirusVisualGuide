using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public float duration = 2f;
    public RectTransform progress;
    public RectTransform background;

    float width, height;

    // Start is called before the first frame update
    void Start()
    {
        width = background.sizeDelta.x;
        height = background.sizeDelta.y;

        StartCoroutine(ShowProgress());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowProgress()
    {
        progress.sizeDelta = Vector2.right * height;
        float x = 0;
        while (x < width - 0.1f)
        {
            x += Time.deltaTime / duration * width;
            progress.sizeDelta = new Vector2(x, height);
            yield return null;
        }

    }


}
