using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreTextFX : MonoBehaviour
{
    public float popScale = 1.35f;
    public float popTime = 0.12f;
    public Color flashColor = Color.yellow;

    TextMeshProUGUI tmp;
    Vector3 baseScale;
    Color baseColor;
    Coroutine routine;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        baseScale = transform.localScale;
        baseColor = tmp.color;
    }

    public void Play()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(DoFX());
    }

    IEnumerator DoFX()
    {
        tmp.color = flashColor;
        transform.localScale = baseScale * popScale;
        yield return new WaitForSeconds(popTime);
        transform.localScale = baseScale;
        tmp.color = baseColor;
    }
}