using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyLeft : MonoBehaviour
{
    [SerializeField] private RectTransform _energyPanel;
    [SerializeField] private RectTransform _backgroundPanel;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _levelLabel;

    private void Awake()
    {
        GameManager.Event_OnGainEnergy += OnGainEnergy;
        GameManager.Event_OnLoseEnergy += OnLoseEnergy;
        GameManager.Event_OnStartLevel += OnStartLevel;
    }

    private void OnDestroy()
    {
        GameManager.Event_OnGainEnergy -= OnGainEnergy;
        GameManager.Event_OnLoseEnergy -= OnLoseEnergy;
        GameManager.Event_OnStartLevel -= OnStartLevel;
    }

    private void OnStartLevel(int level)
    {
        _levelLabel.text = level.ToString();
    }

    private void OnGainEnergy(int total, int gained)
    {
        _label.text = total.ToString();
        Debug.Log(_backgroundPanel.sizeDelta);
        _energyPanel.sizeDelta = new Vector2(_backgroundPanel.sizeDelta.x * (total / 100f), _energyPanel.sizeDelta.y);
    }

    private void OnLoseEnergy(int total, int lost)
    {
        _label.text = total.ToString();

        _energyPanel.sizeDelta = new Vector2(_backgroundPanel.sizeDelta.x * (total / 100f), _energyPanel.sizeDelta.y);
    }
}
