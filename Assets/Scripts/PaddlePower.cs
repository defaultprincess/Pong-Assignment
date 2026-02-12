using UnityEngine;

public class PaddlePower : MonoBehaviour
{
    Vector3 originalScale;
    float timer;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f) transform.localScale = originalScale;
        }
    }

    public void Grow(float multiplier, float seconds)
    {
        transform.localScale = originalScale * multiplier;
        timer = seconds;
    }
}