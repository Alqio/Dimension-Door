using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphicsMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotation_speed;

    public bool alfa_loop;
    public float alfa_speed;
    public float alfa_max;
    public float alfa_min;

    public bool scale_auto;
    public float scale_speed;
    public float scale_max;
    public float scale_min;

    public bool color_auto;

    public float color_speed;
    public Vector3 color_rgb;

    //SpriteRenderer renderer;

    void Start()
    {
        //renderer = GetComponent<SpriteRenderer>();
        scale_max = scale_max * transform.localScale.x;
        scale_min = scale_min * transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        Color c = GetComponent<SpriteRenderer>().color;

        transform.Rotate(new Vector3(0,0,rotation_speed) * Time.deltaTime);

        if (alfa_min < 0) alfa_min = 0;
        if (alfa_max > 1) alfa_max = 1;
        float f = c.a + alfa_speed * Time.deltaTime;
        if (f > alfa_max) f = alfa_max;
        if (f < alfa_min) f = alfa_min;

        if (alfa_loop)
        {
            if (f == alfa_min && alfa_speed < 0)
                alfa_speed = -alfa_speed;
            if (f == alfa_max && alfa_speed > 0)
                alfa_speed = -alfa_speed;

        }

        if (scale_auto)
        {
            float scale_now = transform.localScale.x;
            float t = Time.deltaTime * scale_speed;
            if (alfa_speed < 0 && scale_now > scale_min)
            {
                float x = transform.localScale.x - t;
                float y = transform.localScale.y - t;
                float z = transform.localScale.z - t;

                transform.localScale = new Vector3(x,y,z);
            } else if (alfa_speed > 0 && scale_now < scale_max)
            {
                float x = transform.localScale.x + t;
                float y = transform.localScale.y + t;
                float z = transform.localScale.z + t;

                transform.localScale = new Vector3(x, y, z);
            }
        }
        else
        {
            if (transform.localScale.x < scale_min)
                transform.localScale = new Vector3(scale_min, scale_min, scale_min);
            if(transform.localScale.x > scale_max)
                transform.localScale = new Vector3(scale_max, scale_max, scale_max);
            float t = Time.deltaTime * scale_speed;

            float x = transform.localScale.x + t;
            float y = transform.localScale.y + t;
            float z = transform.localScale.z + t;
            transform.localScale = new Vector3(x, y, z);

        }
        float r = c.r;
        float g = c.g;
        float b = c.b;

        if (color_auto)
        {
            float c_r = color_rgb.x / 255;
            float c_g = color_rgb.y / 255;
            float c_b = color_rgb.z / 255;

            if (c.r != c_r)
                r += ((c_r - c.r) / Mathf.Abs(c_r - c.r)) * (color_speed/100) * Time.deltaTime;

            if (c.g != c_g)
                g += ((c_g - c.g) / Mathf.Abs(c_g - c.g)) * (color_speed/100) * Time.deltaTime;

            if (c.b != c_b)
                b += ((c_b - c.b) / Mathf.Abs(c_b - c.b)) * (color_speed/100) * Time.deltaTime;
        }
        GetComponent<SpriteRenderer>().color = new Color(r, g, b, f);

    }
}
