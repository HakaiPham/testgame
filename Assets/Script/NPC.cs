using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panel_cotruyen;
    //[SerializeField] private string[] content;
    [SerializeField] private TextMeshProUGUI textContent;
    Coroutine coroutine;
    List<DoiTuong> doituongList = new List<DoiTuong>();
    public int tiendo;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panel_NV;
    bool isAttack = false;
    bool isStartNPC = false;
    bool isQuestFinished = false;
    void Start()
    {
        panel_cotruyen.SetActive(false);
        doituongList.Add(new DoiTuong("Player: ", "Hello,NPC"));
        doituongList.Add(new DoiTuong("NPC: ", "HI, Tan Dep Trai"));
    }

    // Update is called once per frame
    void Update()
    {
        if (tiendo == 3&&!isQuestFinished)
        {
            isQuestFinished = true;
            panel_NV.SetActive(true);
            text.color= Color.green;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player"&&!isStartNPC)
        {
            isStartNPC = true;
            panel_cotruyen.SetActive(true);
            coroutine = StartCoroutine(StartCotTruyen());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            panel_cotruyen.SetActive(false);
            StopCoroutine(coroutine);
        }
    }
    private IEnumerator StartCotTruyen()
    {
        foreach (DoiTuong list in doituongList)
        {
            textContent.text = list.doituong;
            foreach (char t in list.hoithoai)
            {
                textContent.text += t;
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.2f);
        }
        panel_cotruyen.SetActive(false);
        panel_NV.SetActive(true);

    }
    [Serializable]
    public class DoiTuong
    {
        public string doituong;
        public string hoithoai;
        public DoiTuong(string doiTuong, string hoiThoai)
        {
            doituong = doiTuong;
            hoithoai = hoiThoai;
        }
    }
    public int TienDoNv()
    {
        tiendo++;
        tiendo = Mathf.Min(3, tiendo);
        text.text = "Diệt 3 con quái: " + tiendo + "/3";
        return tiendo;
    }
}
