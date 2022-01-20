using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public List<GameObject> things;
    public PostProcessSettings postProcessSettings;

    int currentItem = 0;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                Next();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Next();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Reveal();
        }
    }

    void Next()
    {
        things[currentItem].SetActive(false);

        currentItem = (currentItem + 1) % things.Count;

        things[currentItem].SetActive(true);

        postProcessSettings.Change();
    }

    void Reveal()
    {
        postProcessSettings.Reveal();
    }
}
