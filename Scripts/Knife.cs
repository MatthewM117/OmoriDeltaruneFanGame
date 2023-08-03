using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1);
        animator.Play("knife_attack", 0, 0f);
        AudioManager.instance.PlayAudio("whoosh");
    }
}
