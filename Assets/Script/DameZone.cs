using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameZone : MonoBehaviour
{
    // Start is called before the first frame update
    //vung sat thuong
    public Collider dameConllider;
    EnemyScript enemy;
    public List<Collider> listDame = new List<Collider>();
    public string target;
    [SerializeField] bool isPlayer;
    NV3D nv3D;

    void Start()
    {
        dameConllider.enabled = false;//tat vung sat thuong
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == target && !listDame.Contains(other))
        {
            if (Input.GetMouseButton(0))
            {
                listDame.Add(other);
                other.GetComponent<EnemyScript>().TakeDame(100);
            }
            //else
            //{
            //    Debug.Log("Đã đánh Player");
            //    listDame.Add(other);
            //    other.GetComponent<NV3D>().TakeDame(100);
            //}
        }
    }
    public void BeginDame()
    {
       //listDame.Clear();//Khi bat dau tan cong Clear het cac collider co trong danh sach
        dameConllider.enabled=true;
    }
    public void EndDame()
    {
        listDame.Clear();//Khi ket thuc tan cong Clear het cac collider co trong danh sach
        dameConllider.enabled = false;
    }
}
