using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    const float defaultSpeed = 2f;
    public float speed = defaultSpeed;
    public const float maxWaitTime = 2f;
    public const float distanceThreshold = 0.01f;

    Vector2 destination;

    SpriteRenderer[] spriteRenderers;

    Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.instance;
        PickPosition();

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in spriteRenderers)
        {
            item.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Reset()
    {
        speed = defaultSpeed;
    }

    void PickPosition()
    {
        destination = manager.GetRandomPosition();
        StartCoroutine(MoveToRandomPos());
    }

    IEnumerator MoveToRandomPos()
    {

        while (Vector2.Distance(transform.position, destination) > distanceThreshold)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        float randomFloat = Random.Range(0.0f,1.0f); // Create %80 chance to wait
        if (randomFloat < 0.8f)
            StartCoroutine(WaitForSomeTime());
        else
            PickPosition();
    }

    IEnumerator WaitForSomeTime()
    {
        yield return new WaitForSeconds(Random.Range(0, maxWaitTime));
        PickPosition();
    }

}
