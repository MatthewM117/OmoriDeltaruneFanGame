using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyKnife());
    }

    IEnumerator DestroyKnife()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
