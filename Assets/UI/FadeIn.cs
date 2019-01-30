using UnityEngine;

public class FadeIn: MonoBehaviour
{
    [Tooltip("The target maskable graphic to fade in and out.")]
    public UnityEngine.UI.MaskableGraphic FadeTarget;

    [Tooltip("The total time to complete a fade in cycle.")]
    public float FadeTime = 1.0f;

    private float _baseAlpha = 0.0f;
    private float _elapsedTime = 0.0f;

    private void Start()
    {
        if (FadeTarget == null)
        {
            Debug.LogError("FadeTarget cannot be null.");
            enabled = false;
        }
        else
        {
            _baseAlpha = FadeTarget.color.a;
            Color reset = FadeTarget.color;
            reset.a = 0f;
            FadeTarget.color = reset;
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= FadeTime)
        {
            Destroy(this);
            return;
        }

        Color fadedColour = FadeTarget.color;
        float alphaModifier = (_elapsedTime / FadeTime);

        fadedColour.a = _baseAlpha * alphaModifier;
        FadeTarget.color = fadedColour;
    }
}