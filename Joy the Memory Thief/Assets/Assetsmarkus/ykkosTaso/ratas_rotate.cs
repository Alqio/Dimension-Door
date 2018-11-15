using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratas_rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public RotateGameWorld world;

    private void Awake()
    {
        world = GameObject.FindGameObjectWithTag("Player").GetComponent<RotateGameWorld>();
    }

    void Start()
    {
        if (speed == 0)
        {
            speed = 15f;
        }
        float f = Random.Range(-10.0f, 10.0f);
        if (f < 0)
        {
            speed = -speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float nopeus = speed * (1f / transform.localScale.x);
        if (world.rotating)
        {
            nopeus *= 2;
        }
        transform.Rotate(new Vector3(0, 0, nopeus * Time.deltaTime));

    }
}
