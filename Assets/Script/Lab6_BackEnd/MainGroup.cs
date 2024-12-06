using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGroup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loginGroup;
    public GameObject registerGroup;
    public GameObject mainGroup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
