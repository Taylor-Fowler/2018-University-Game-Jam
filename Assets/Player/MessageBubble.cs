using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBubble : MonoBehaviour
{
    public float DisplayTime;
    public Vector2 Offset;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;
    private Transform _position;
    private Coroutine _messageRoutine;
    private RectTransform _transform;

    private void Start()
    {
        _transform = (RectTransform)transform;
    }

    public void DisplayMessage(string message, Transform position)
    {
        _position = position;
        _text.text = message;

        if(_messageRoutine != null)
        {
            StopCoroutine(_messageRoutine);
        }
        _messageRoutine = StartCoroutine(DisplayMessage(DisplayTime));
    }

    private IEnumerator DisplayMessage(float timer)
    {
        _image.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);

        while(timer > 0f)
        {
            //_transform.anchoredPosition = (Vector2)Camera.main.WorldToScreenPoint(_position.position);// - Offset;

            yield return null;
            timer -= Time.deltaTime;
        }

        _image.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
        _messageRoutine = null;
        _position = null;
    }
}
