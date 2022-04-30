using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    private Image image;
    public Sprite[] frames;
    private float timeIndex = 0;
    private readonly int framesPerSec = 24;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timeIndex += Time.deltaTime;

        if(timeIndex * framesPerSec >= frames.Length)
        {
            timeIndex = 0;
        }

        image.sprite = frames[Mathf.FloorToInt(timeIndex * framesPerSec)];
    }
}
