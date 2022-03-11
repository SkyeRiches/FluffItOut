using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChompScript : MonoBehaviour {

    private Animator hippoAnim;

    // Start is called before the first frame update
    void Start() {
        hippoAnim = gameObject.GetComponent<Animator>();

        StartCoroutine(ChompIntervals());
    }

    /// <summary>
    /// after a random amount of time, the animation for the hungry hippo will play
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChompIntervals(){
        int randInt = Random.Range(0, 10);

        yield return new WaitForSeconds(randInt);

        hippoAnim.SetBool("Chomp", true);
        yield return new WaitForSeconds(1);
        hippoAnim.SetBool("Chomp", false);

        StartCoroutine(ChompIntervals());
    }
}
