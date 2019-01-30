using UnityEngine;
using TMPro;

public class EnemiesLeft : MonoBehaviour
{
    [SerializeField] private RectTransform _backgroundImage;
    [SerializeField] private RectTransform _electronImage;
    [SerializeField] private RectTransform _neutronImage;
    [SerializeField] private RectTransform _protonImage;
    [SerializeField] private TextMeshProUGUI _totalText;

    private int _electronCount;
    private int _neutronCount;
    private int _protonCount;

    private void Awake()
    {
        GameManager.Event_OnSpawnElectron += OnSpawnElectron;
        GameManager.Event_OnSpawnNeutron += OnSpawnNeutron;
        GameManager.Event_OnSpawnProton += OnSpawnProton;
        GameManager.Event_OnDestroyElectron += OnSpawnElectron;
        GameManager.Event_OnDestroyNeutron += OnSpawnNeutron;
        GameManager.Event_OnDestroyProton += OnSpawnProton;
    }

    private void OnDestroy()
    {
        GameManager.Event_OnSpawnElectron -= OnSpawnElectron;
        GameManager.Event_OnSpawnNeutron -= OnSpawnNeutron;
        GameManager.Event_OnSpawnProton -= OnSpawnProton;
        GameManager.Event_OnDestroyElectron -= OnSpawnElectron;
        GameManager.Event_OnDestroyNeutron -= OnSpawnNeutron;
        GameManager.Event_OnDestroyProton -= OnSpawnProton;
    }

    private void Redisplay()
    {
        float left = 0;
        float width = _backgroundImage.sizeDelta.x;

        int total = _electronCount + _neutronCount + _protonCount;
        _totalText.text = total.ToString();

        left = PositionPanel(left, _electronCount, _electronImage, width, total);
        left = PositionPanel(left, _neutronCount, _neutronImage, width, total);
        PositionPanel(left, _protonCount, _protonImage, width, total);
    }

    private float PositionPanel(float left, float count, RectTransform panel, float width, int overall)
    {
        if(count == 0)
        {
            panel.sizeDelta = Vector2.zero;
            return left;
        }
        else
        {
            float panelWidth = width * (count / overall);
            panel.sizeDelta = new Vector2(panelWidth, _backgroundImage.sizeDelta.y);
            panel.anchoredPosition = new Vector2(left, panel.anchoredPosition.y);
            //panel.anchorMin = new Vector2(left, 0);
            //panel.anchorMax = new Vector2(panelWidth, _backgroundImage.sizeDelta.y);
            return left + panelWidth;
        }
    }

    private void OnSpawnElectron(int count, Vector3 position)
    {
        _electronCount = count;
        Redisplay();
    }

    private void OnSpawnNeutron(int count, Vector3 position)
    {
        _neutronCount = count;
        Redisplay();
    }

    private void OnSpawnProton(int count, Vector3 position)
    {
        _protonCount = count;
        Redisplay();
    }
}
