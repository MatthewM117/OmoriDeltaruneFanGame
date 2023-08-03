using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBGMoving : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private void Update()
    {
        if (transform.position.y >= 100f)
        {
            transform.position = new Vector3(-0.1125f, -22.7f, 0);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector2.up);
    }
}
