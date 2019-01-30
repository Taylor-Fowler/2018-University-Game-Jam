using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{

    // Desired duration of the shake effect
    private float _shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    private float _shakeMagnitude = 0.2f;
    // A measure of how quickly the shake effect should evaporate
    private float _dampingSpeed = 1.0f;
    // The initial position of the GameObject
    Vector3 _initialPosition;
    Coroutine _shakeRoutine = null;

    private void Awake()
    {
        GameManager.Event_OnLoseEnergy += OnLoseEnergy;
    }

    private void OnEnable()
    {
        _initialPosition = transform.localPosition;
    }

    private void OnDestroy()
    {
        GameManager.Event_OnLoseEnergy -= OnLoseEnergy;
    }

    private void OnLoseEnergy(int total, int lost)
    {
        _shakeDuration = 0.4f;
        if(_shakeRoutine == null)
        {
            _shakeRoutine = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        while(_shakeDuration > 0f)
        {
            yield return null;
            transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;
            _shakeDuration -= Time.deltaTime * _dampingSpeed;
        }
        _shakeDuration = 0f;
        transform.localPosition = _initialPosition;
        _shakeRoutine = null;
    }


}
