using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _upgradeCanvas;
    [SerializeField] private Button[] _upgradeButtons;
    [SerializeField] private Image[] _upgradeImages;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private Button _continueButton;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Sprite _upgradedSprite;
    private int _pointsToSpend = 0;
    private int[] _upgrades = new int[3];


    public void Initialise(Action cont)
    {
        _continueButton.onClick.AddListener(delegate
        {
            cont();
        });
        Hide();

        GameManager.Event_OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.Event_OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void Show()
    {
        _continueButton.interactable = true;
        _upgradeCanvas.gameObject.SetActive(true);
        _pointsToSpend++;
        _pointsText.text = _pointsToSpend.ToString();
        UpdateUpgradeButtons();
    }

    public void Hide()
    {
        _continueButton.interactable = false;
        _upgradeCanvas.gameObject.SetActive(false);
    }


    private void DeductPoint()
    {
        if(_pointsToSpend > 0)
        {
            _pointsToSpend--;
            _pointsText.text = _pointsToSpend.ToString();
        }
        UpdateUpgradeButtons();
    }

    private void UpdateUpgradeButtons()
    {
        bool interactable = _pointsToSpend > 0;
        for(int i = 0; i < 3; i++)
        {
            _upgradeButtons[i].interactable = interactable && _upgrades[i] < 3;
        }
    }

    public void BuyWeapon()
    {
        _playerController.BuyWeapons();
        _upgradeImages[_upgrades[0]].sprite = _upgradedSprite;
        _upgrades[0]++;
        DeductPoint();
    }

    public void UpgradeWeapon()
    {
        _playerController.UpgradeWeapons();
        _upgradeImages[3 + _upgrades[1]].sprite = _upgradedSprite;
        _upgrades[1]++;
        DeductPoint();
    }

    public void UpgradeSpeed()
    {
        _playerController.UpgradeSpeed();
        _upgradeImages[6 + _upgrades[2]].sprite = _upgradedSprite;
        _upgrades[2]++;
        DeductPoint();
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
