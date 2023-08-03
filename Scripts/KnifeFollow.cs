using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeFollow : MonoBehaviour
{
    Transform player;
    Transform player2;

    [SerializeField] float speed = 7f;
    [SerializeField] Animator knifeFollowAnimator;

    bool attack = false;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (!gm.p1Dead)
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
        
        if (!gm.p2Dead)
        {
            player2 = GameObject.Find("Player2").GetComponent<Transform>();
        }

        StartCoroutine(LungeAttack());
        StartCoroutine(DestroyKnife());
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack)
        {
            if (GlobalData.instance.is2Player)
            {
                if (gm.p1Dead && !gm.p2Dead)
                {
                    Quaternion rotation = Quaternion.LookRotation(player2.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                    transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                }
                else if (!gm.p1Dead && gm.p2Dead)
                {
                    Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                    transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                }
                else
                {
                    Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                    transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                }
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            }
        }
    }

    private void FixedUpdate()
    {
        if (attack)
        {
            transform.Translate(speed * Time.fixedDeltaTime * Vector2.up);
        }
    }

    IEnumerator LungeAttack()
    {
        while (true) {
            yield return new WaitForSeconds(1);
            knifeFollowAnimator.Play("knife_follow_attack", 0, 0f);
            AudioManager.instance.PlayAudio("whoosh");
            attack = true;
            yield return new WaitForSeconds(2);
            attack = false;
        }
    }

    IEnumerator DestroyKnife()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
