using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ApiService : MonoBehaviour
{
    // Start is called before the first frame update
    private string baseUrl = "http://localhost:5121/api/gameuser";
    [System.Serializable]
    public class LoginRequest
    {
        public string UserName;
        public string Password;
    }
    [System.Serializable]
    public class LoginResponse
    {
        public bool IsSuccess;
        public string Notification;
        public LoginData Data;
    }
    public class LoginData {
        public string token;
        public GameUser user;
    }
    public class GameUser
    {
        public int Id;
        public string UserName;
        public string Email;
        public string Name;
        public string AvatarUrl;
        public string Role;
    }
    public IEnumerator Login(string userName, string passWord, 
        System.Action<LoginResponse>onSuccess,System.Action<string> onFailure) 
    {
        LoginRequest loginRequest = new LoginRequest {UserName = userName,Password=passWord };
        string jsonData = JsonConvert.SerializeObject(loginRequest);
        UnityWebRequest request = new UnityWebRequest($"{baseUrl}/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Reponse JSON: "+jsonResponse);
            try
            {
                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    LoginResponse response = JsonConvert
                        .DeserializeObject<LoginResponse>(jsonResponse);
                    //Kiem tra va luu token tu response.data.token
                    if (response != null && response.Data != null
                        && !string.IsNullOrEmpty(response.Data.token))
                    {
                        PlayerPrefs.SetString("AuthToken", response.Data.token);
                        PlayerPrefs.Save();
                        SceneManager.LoadScene("Lab6");
                        onSuccess?.Invoke(response);
                    }
                    else
                    {
                        Debug.Log("Token is null or missing in response data");
                        onFailure?.Invoke("Token is null or missing in response data");
                    }
                }
                else
                {
                    Debug.LogError("Empty or null response from API");
                    onFailure?.Invoke("Empty or null response from API");
                }
            }
            catch(System.Exception ex)
            {
                Debug.LogError("JSON Parse Error: " + ex.Message);
                onFailure?.Invoke("JSON Parse Error: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("Request Failed: " + request.error);
            onFailure?.Invoke(request.error);
        }
    }
}
