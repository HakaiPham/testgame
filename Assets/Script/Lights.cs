using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeDayNight = 1f;
    public Light light;
    public float rotationSpeed;
    public Gradient gradient;
    void Start()
    {
        rotationSpeed = 360 / (timeDayNight * 60f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        if(light != null&&gradient != null)
        {
            float time = Mathf.PingPong(Time.time / (timeDayNight * 30f), 1f);
            light.color = gradient.Evaluate(time);
        }
    }
}
