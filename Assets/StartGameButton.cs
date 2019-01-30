using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    private KeyCode m_startGameKeyCode;

    private void Start()
    {
        m_startGameKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "Joystick1Button1", true);
    }

    private void Update()
    {
        if(Input.GetKey(m_startGameKeyCode))
        {
            SceneManager.LoadScene(1);
            gameObject.SetActive(false);
        }
    }
}
