using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleKnives : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private void Start()
    {
        StartCoroutine(DestroyKnives());
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector2.down);
    }

    IEnumerator DestroyKnives()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
