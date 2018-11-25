using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAnimation : MonoBehaviour
{
    Animator transition;
    public bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         if (trigger)
        {
            LoadScene("SampleScene");
        }
    }

    void LoadScene(string name)
    {
        StartCoroutine(Transition(name));
    }

    IEnumerator Transition(string name)
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(name);
    }
}
