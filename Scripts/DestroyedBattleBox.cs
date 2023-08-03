using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBattleBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBox());
    }

    IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
