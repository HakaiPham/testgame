using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.VirtualTexturing;

public class RegisterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainGroup;
    public GameObject loginGroup;
    public GameObject registerGroup;
    public TMP_InputField emailInput;
    public TMP_InputField userNameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nameUser;
    public TMP_InputField avatarInput; //Them Input Field cho Avartar URL
    public TMP_Text txtMessage;
    private string baseUrl = "http://localhost:5121/api/gameuser";//URL den API
    void Start()
    {
        ShowMainGroup();
    }

    // Update is called once per frame
    public void OnRegisterButtonPressed()
    {
        string email = emailInput.text; ;
        string userName = userNameInput.text;
        string password = passwordInput.text;
        string name = nameUser.text;
        string avartarURL = avatarInput.text;
        StartCoroutine(Register(email,userName,password,name,avartarURL));
    }
    private IEnumerator Register(string email, string userName, string password,
        string name, string avartarUrl)
    {
        var registerRequest = new RegisterRequest
        {
            Email = email,
            UserName = userName,
            Password = password,
            Name = name,
            AvatarUrl = avartarUrl
        };
        string jsonData = JsonConvert.SerializeObject(registerRequest);
        Debug.Log("Register JSON: "+jsonData);
        UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register","POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            txtMessage.text = "Register Successfully";
            ShowMainGroup();
        }
        else
        {
            string errorResponse = request.downloadHandler.text;
            Debug.LogError("Register Failed. Error: " + request.error);
            Debug.LogError("Server Response: " + errorResponse);
            txtMessage.text = "Register Failed: " + errorResponse;
        }
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
    [System.Serializable]
    public class RegisterRequest
    {
        public string UserName;
        public string Password;
        public string Email;
        public string Name;
        public string AvatarUrl;
    }
}
