using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] GameObject slash1;
    [SerializeField] GameObject slash2;
    [SerializeField] GameObject slash3;
    [SerializeField] GameObject slash4;
    [SerializeField] GameObject slash5;
    [SerializeField] GameObject slash6;

    const float waitTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateSlash());
        AudioManager.instance.PlayAudio("slash");
    }

    IEnumerator AnimateSlash()
    {
        yield return new WaitForSeconds(waitTime);
        slash1.SetActive(false);
        slash2.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        slash2.SetActive(false);
        slash3.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        slash3.SetActive(false);
        slash4.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        slash4.SetActive(false);
        slash5.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        slash5.SetActive(false);
        slash6.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
