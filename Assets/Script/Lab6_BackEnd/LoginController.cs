using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoginController : MonoBehaviour
{
    // Start is called before the first frame update
    public ApiService apiService;
    public GameObject mainGroup;
    public GameObject loginGroup;
    public GameObject registerGroup;
    public TMP_InputField userNameInput;
    public TMP_InputField passwordInput;
    public TMP_Text txtMessage;
    void Start()
    {
        ShowMainGroup();
    }

    // Update is called once per frame
    public void OnLoginButtonPressed()
    {
        Debug.Log("Login Button Pressed");
        string userName = userNameInput.text;
        string password = passwordInput.text;
        StartCoroutine(apiService.Login(userName, password,OnLoginSuccess,OnLoginFailure));
    }
    public void OnLoginSuccess(ApiService.LoginResponse response)
    {
        if (response.IsSuccess) 
        {
            //dang nhap thanh cong hien token va hien thi thong bao
            txtMessage.text = response.Notification + "Hello, " + response.Data.user.UserName;
            Debug.Log("Token: " + response.Data.token);
            ShowMainGroup();
        }
        else
        {
            txtMessage.text = "Login Fail: " + response.Notification;
        }
    }
    private void OnLoginFailure(string error)
    {
        txtMessage.text = "Login Fail: " + error;
    }
    public void ShowMainGroup()
    {
        mainGroup.SetActive(true);
        loginGroup.SetActive(false);
        registerGroup.SetActive(false);
    }
    public void ShowLoginGroup()
    {
        mainGroup.SetActive(false);
        loginGroup.SetActive(true);
        registerGroup.SetActive(false);
    }
    public void ShowRegisterGroup()
    {
        mainGroup.SetActive(false);
        loginGroup.SetActive(false);
        registerGroup.SetActive(true);
    }
}
