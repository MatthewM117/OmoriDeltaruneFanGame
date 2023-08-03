using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHand : MonoBehaviour
{
    [SerializeField] float speed = 7f;

    private void Start()
    {
        StartCoroutine(DestroyHand());
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector2.up);
    }

    IEnumerator DestroyHand()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
