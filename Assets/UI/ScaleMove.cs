using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ScaleMove : MonoBehaviour
{
    public float MoveX = -120f;
    public float ScaleTime = 1f;
    public float MoveTime = 1f;
    public float Delay = 1f;
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private RectTransform _warningTransform;
    [SerializeField] private GameObject _activate;
    private Action _callback;

    private IEnumerator MoveText()
    {
        yield return new WaitForSeconds(Delay);
        Vector2 position = _warningTransform.anchoredPosition;
        Debug.Log(position);
        Vector2 target = position + new Vector2(MoveX, 0f);

        float timeElapsed = 0f;
        while(timeElapsed < MoveTime)
        {
            yield return null;

            timeElapsed += Time.deltaTime;
            _warningTransform.anchoredPosition = Vector2.Lerp(position, target, timeElapsed / MoveTime);
        }
        _activate.SetActive(true);
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        _callback();
    }

    public IEnumerator ScaleText(Action callback)
    {
        gameObject.SetActive(true);
        _callback = callback;
        float timeElapsed = 0f;

        while(timeElapsed < ScaleTime)
        {
            yield return null;

            timeElapsed += Time.deltaTime;
            _warningTransform.localScale = Vector3.Lerp(new Vector3(10f, 10f, 10f), new Vector3(1f, 1f, 1f), timeElapsed / ScaleTime);
        }

        StartCoroutine(MoveText());
    }
}
