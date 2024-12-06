using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _MenuAnima;
    [SerializeField]
    private GameObject _MenuPanel;
    private void Start()
    {
        _MenuAnima = GameObject.Find("Image").GetComponent<Animator>();
        _MenuAnima.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _MenuPanel.SetActive(true);
            _MenuAnima.SetBool("isStart", true);
            Time.timeScale = 0;
        }
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
