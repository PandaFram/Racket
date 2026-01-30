using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("UI")]
    public Image greenFillImage;
    public GameObject timerRoot;

    [Header("Time")]
    public float totalTime = 10f;

    [Header("Clock Hand")]
    public RectTransform clockHand;

    float remainingTime;

    void Start()
    {
        remainingTime = totalTime;
        UpdateVisual();
    }

    void Update()
    {
        if (remainingTime <= 0f)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            timerRoot.SetActive(false);
        }

        UpdateVisual();
    }

    void UpdateVisual()
    {
        float t = remainingTime / totalTime;

        greenFillImage.fillAmount = t;

        if (clockHand != null)
        {
            float angle = 180f - t * 360f;
            clockHand.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }


    // Call this when you want to start/reset the timer
    public void StartTimer(float time)
    {
        totalTime = time;
        remainingTime = time;
        timerRoot.SetActive(true);
        UpdateVisual();
    }
}
