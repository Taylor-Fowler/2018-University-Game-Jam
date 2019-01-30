using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private static bool _firstRun = true;
    [SerializeField] private ScaleMove _scaleMove;
    [SerializeField] private GameObject _menuObject;
    [SerializeField] private RectTransform _titleTransform;
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private float _bounciness;
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private AudioSource _knockSound;
    // https://freesound.org/people/juskiddink/sounds/108615/
    private Vector2 _endPosition;


    private void Awake()
    {
        Debug.Log(_titleTransform.anchoredPosition);
        if (_firstRun)
        {
            StartCoroutine(_scaleMove.ScaleText(BounceTitle));
        }
    }

    private void BounceTitle()
    {
        _endPosition = _titleTransform.anchoredPosition;
        _startGameButton.gameObject.SetActive(false);
        _titleTransform.anchoredPosition = _endPosition + _startPosition;
        _firstRun = false;

        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        float motion = 0f;
        float force = 1f;

        while(force > 0.4f)
        {
            yield return null;
            motion -= 0.1f;

            _titleTransform.anchoredPosition = _titleTransform.anchoredPosition + new Vector2(0f, 1f * motion);

            if(_titleTransform.anchoredPosition.y < _endPosition.y)
            {
                _knockSound.volume = (force - 0.4f) / 0.6f;
                _knockSound.Play();
                _titleTransform.anchoredPosition = _endPosition;
                force = force * _bounciness;
                motion = force * -motion;
            }
        }

        _titleTransform.anchoredPosition = _endPosition;
        _startGameButton.gameObject.SetActive(true);
    }
}
