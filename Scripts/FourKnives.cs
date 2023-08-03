using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourKnives : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyKnives());
    }

    IEnumerator DestroyKnives()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
