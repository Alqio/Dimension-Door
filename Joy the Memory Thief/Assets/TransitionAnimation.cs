using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAnimation : MonoBehaviour
{
    Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponent<Animator>();
    }

    public void LoadScene(string name)
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
