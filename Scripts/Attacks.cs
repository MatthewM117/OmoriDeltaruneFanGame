using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] GameObject slashAttackObj;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject redHand;
    [SerializeField] GameObject doubleKnives;
    [SerializeField] GameObject knifeFollow;
    [SerializeField] GameObject fourKnives;
    [SerializeField] GameObject slashSlowObj;

    [SerializeField] Transform leftBorder;
    [SerializeField] Transform rightBorder;
    [SerializeField] Transform topBorder;
    [SerializeField] Transform bottomBorder;

    [SerializeField] GameObject originalBattleBox;
    [SerializeField] GameObject bbLeftHalf;
    [SerializeField] GameObject bbRightHalf;
    [SerializeField] GameObject topLeftBox;
    [SerializeField] GameObject bottomLeftBox;
    [SerializeField] GameObject topRightBox;
    [SerializeField] GameObject bottomRightBox;

    [SerializeField] GameObject something;

    [SerializeField] PlayerMovement pm;

    GameManager gm;

    [SerializeField] PlayerTwoMovement p2m;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public IEnumerator SlashAttack(int length, bool solo)
    {
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            float randomPosX = Random.Range(leftBorder.position.x, rightBorder.position.x);
            float randomPosY = Random.Range(bottomBorder.position.y, topBorder.position.y);
            float randomRotation;
            if (randomPosX > 0)
            {
                randomRotation = Random.Range(60f, 145f);
            }
            else
            {
                randomRotation = Random.Range(-100f, 0f);
            }

            Instantiate(slashSlowObj, new Vector3(randomPosX, randomPosY, 0), Quaternion.Euler(0, 0, randomRotation));
        }

        if (solo)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(gm.EndAttack());
        }
    }

    public IEnumerator KnifeAttack(int length, bool shootThree, bool solo)
    {
        for (int i = 0; i < length; i++)
        {
            if (shootThree)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(0.75f);
            }
            int side = Random.Range(1, 5); // 1 = left, 2 = right, 3 = up, 4 = down
            float randomPosY;
            float randomPosX;
            float randomRotation;
            switch (side)
            {
                case 2:
                    randomPosY = Random.Range(bottomBorder.position.y + 1, topBorder.position.y - 1);
                    randomRotation = Random.Range(36f, 90f);
                    Instantiate(knife, new Vector3(4.22f, randomPosY, 0), Quaternion.Euler(0, 0, randomRotation));
                    break;
                case 1:
                    randomPosY = Random.Range(bottomBorder.position.y + 1, topBorder.position.y - 1);
                    randomRotation = Random.Range(-90f, -32f);
                    GameObject newKnife = Instantiate(knife, new Vector3(-4.22f, randomPosY, 0), Quaternion.Euler(0, 0, randomRotation));
                    newKnife.transform.localScale = new Vector3(-1, 1, 1);
                    break;
                case 3:
                    randomPosX = Random.Range(leftBorder.position.x + 1, rightBorder.position.x - 1);
                    randomRotation = Random.Range(-175f, -100f);
                    GameObject newKnifeUp = Instantiate(knife, new Vector3(randomPosX, 4.22f, 0), Quaternion.Euler(0, 0, randomRotation));
                    newKnifeUp.transform.localScale = new Vector3(-1, 1, 1);
                    break;
                case 4:
                    randomPosX = Random.Range(leftBorder.position.x + 1, rightBorder.position.x - 1);
                    randomRotation = Random.Range(-410, -367f);
                    Instantiate(knife, new Vector3(randomPosX, -4.22f, 0), Quaternion.Euler(0, 0, randomRotation));
                    break;
            }
        }

        if (shootThree)
        {
            yield return new WaitForSeconds(0.75f);
            Instantiate(knife, new Vector3(4.09f, 0.61f), Quaternion.Euler(0, 0, 64.617f));
            Instantiate(knife, new Vector3(4.09f, 0.61f), Quaternion.Euler(0, 0, 43.012f));
            Instantiate(knife, new Vector3(4.09f, 0.61f), Quaternion.Euler(0, 0, 83.924f));
        }

        if (solo)
        {
            yield return new WaitForSeconds(2);
            StartCoroutine(gm.EndAttack());
        }
    }

    public IEnumerator XSlash()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(slashAttackObj, new Vector3(-0.2f, 0, 0), Quaternion.identity);
        Instantiate(slashAttackObj, new Vector3(0.65f, 0.25f, 0), Quaternion.Euler(0, 0, 70.427f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(0.01f, 1.28f, 0), Quaternion.Euler(0, 0, -134.9f)); // top
        Instantiate(slashAttackObj, new Vector3(-0.91f, 0.12f, 0), Quaternion.Euler(0, 0, -49.38f)); // left
        Instantiate(slashAttackObj, new Vector3(0.2f, -0.58f, 0), Quaternion.Euler(0, 0, 40.93f)); // bottom
        Instantiate(slashAttackObj, new Vector3(0.91f, 0.48f, 0), Quaternion.Euler(0, 0, 127.75f)); // right
        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(-1.24f, -0.96f, 0), Quaternion.identity);
        Instantiate(slashAttackObj, new Vector3(1.29f, -0.85f, 0), Quaternion.Euler(0, 0, 85.926f));
        Instantiate(slashAttackObj, new Vector3(-1.24f, 1.55f, 0), Quaternion.Euler(0, 0, 266.54f));
        Instantiate(slashAttackObj, new Vector3(1.16f, 1.63f, 0), Quaternion.Euler(0, 0, 178.94f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(0.02f, -1.11f, 0), Quaternion.Euler(0, 0, 135.549f));
        Instantiate(slashAttackObj, new Vector3(0.02f, 1.87f, 0), Quaternion.Euler(0, 0, 135.549f));
        Instantiate(slashAttackObj, new Vector3(-0.29f, 0.04f, 0), Quaternion.Euler(0, 0, 310.218f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(0.95f, 1.28f, 0), Quaternion.Euler(0, 0, -134.9f));
        Instantiate(slashAttackObj, new Vector3(-0.91f, 1f, 0), Quaternion.Euler(0, 0, -49.38f));
        Instantiate(slashAttackObj, new Vector3(-0.85f, -0.58f, 0), Quaternion.Euler(0, 0, 40.93f));
        Instantiate(slashAttackObj, new Vector3(0.91f, -0.6f, 0), Quaternion.Euler(0, 0, 127.75f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(0.94f, -1.45f, 0), Quaternion.Euler(0, 0, 117.027f));
        Instantiate(slashAttackObj, new Vector3(-0.88f, -0.25f, 0), Quaternion.Euler(0, 0, -35.641f));
        Instantiate(slashAttackObj, new Vector3(0.94f, 1.75f, 0), Quaternion.Euler(0, 0, 117.027f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(-0.49f, -1.97f, 0), Quaternion.Euler(0, 0, -49.38f));
        Instantiate(slashAttackObj, new Vector3(-2.11f, 0.82f, 0), Quaternion.Euler(0, 0, -134.9f));
        Instantiate(slashAttackObj, new Vector3(2.16f, -0.18f, 0), Quaternion.Euler(0, 0, 40.93f));
        Instantiate(slashAttackObj, new Vector3(0.34f, 2.66f, 0), Quaternion.Euler(0, 0, 127.75f));

        yield return new WaitForSeconds(1);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator SplitBoxAttack()
    {
        yield return new WaitForSeconds(0.25f);
        bbLeftHalf.SetActive(true);
        bbRightHalf.SetActive(true);
        originalBattleBox.SetActive(false);
        Instantiate(slashAttackObj, new Vector3(0.14f, -0.98f, 0), Quaternion.Euler(0, 0, 40.93f));
        gm.AnimateBattleBox(true);
        
        yield return new WaitForSeconds(1.5f);
        topLeftBox.SetActive(true);
        bottomLeftBox.SetActive(true);
        topRightBox.SetActive(true);
        bottomRightBox.SetActive(true);
        bbLeftHalf.SetActive(false);
        bbRightHalf.SetActive(false);
        Instantiate(slashAttackObj, new Vector3(-3, 0.2f, 0), Quaternion.Euler(0, 0, -49.38f));
        Instantiate(slashAttackObj, new Vector3(4.5f, 0.2f, 0), Quaternion.Euler(0, 0, 127.75f));
        gm.AnimateBattleBox(false);

        // first attack
        yield return new WaitForSeconds(1.5f);
        GameObject newSlash1 = Instantiate(slashAttackObj, new Vector3(-2.52f, 2.11f, 0), Quaternion.Euler(0, 0, 40.93f));
        newSlash1.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash4 = Instantiate(slashAttackObj, new Vector3(2.06f, 2.75f, 0), Quaternion.Euler(0, 0, -49.38f));
        newSlash4.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash7 = Instantiate(slashAttackObj, new Vector3(2.77f, -1.96f, 0), Quaternion.Euler(0, 0, -134.9f));
        newSlash7.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash10 = Instantiate(slashAttackObj, new Vector3(-2.04f, -2.67f, 0), Quaternion.Euler(0, 0, 136.639f));
        newSlash10.transform.localScale = new Vector3(1, 1, 1);

        // second attack
        yield return new WaitForSeconds(0.2f);
        GameObject newSlash2 = Instantiate(slashAttackObj, new Vector3(-3.28f, 3.32f, 0), Quaternion.Euler(0, 0, -98.84f));
        newSlash2.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash5 = Instantiate(slashAttackObj, new Vector3(3.2f, 3.42f, 0), Quaternion.Euler(0, 0, 184.752f));
        newSlash5.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash8 = Instantiate(slashAttackObj, new Vector3(3.19f, -3.2f, 0), Quaternion.Euler(0, 0, -265.831f));
        newSlash8.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash11 = Instantiate(slashAttackObj, new Vector3(-3.13f, -3.19f, 0), Quaternion.Euler(0, 0, -1.786f));
        newSlash11.transform.localScale = new Vector3(1, 1, 1);

        // third attack
        yield return new WaitForSeconds(0.2f);
        GameObject newSlash3 = Instantiate(slashAttackObj, new Vector3(-3.57f, 2.11f, 0), Quaternion.Euler(0, 0, 40.93f));
        newSlash3.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash6 = Instantiate(slashAttackObj, new Vector3(2.06f, 3.71f, 0), Quaternion.Euler(0, 0, -49.38f));
        newSlash6.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash9 = Instantiate(slashAttackObj, new Vector3(3.61f, -2.06f, 0), Quaternion.Euler(0, 0, -134.9f));
        newSlash9.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.1f);
        GameObject newSlash12 = Instantiate(slashAttackObj, new Vector3(-2.1f, -3.54f, 0), Quaternion.Euler(0, 0, 136.639f));
        newSlash12.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(1);
        topLeftBox.SetActive(false);
        topRightBox.SetActive(false);
        bottomLeftBox.SetActive(false);
        bottomRightBox.SetActive(false);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator RedHandsAttack(int length, bool solo)
    {
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.2f);

            Instantiate(redHand, new Vector3(3.18f, -0.59f, 0), Quaternion.Euler(0, 0, Random.Range(43f, 143f)));
            Instantiate(redHand, new Vector3(2.43f, -2.71f, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(redHand, new Vector3(2.43f, 1.61f, 0), Quaternion.Euler(0, 0, 90));
        }

        if (solo)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(gm.EndAttack());
        }
    }

    public IEnumerator HorizontalAndVerticalSlashes()
    {
        yield return new WaitForSeconds(0.25f);

        Instantiate(slashAttackObj, new Vector3(0.95f, -1.64f, 0), Quaternion.Euler(0, 0, 135.14f));
        Instantiate(slashAttackObj, new Vector3(-0.93f, 2.24f, 0), Quaternion.Euler(0, 0, -44.95f));

        yield return new WaitForSeconds(0.5f);

        Instantiate(slashAttackObj, new Vector3(0.95f, -1.13f, 0), Quaternion.Euler(0, 0, 135.14f));
        Instantiate(slashAttackObj, new Vector3(-0.93f, 1.73f, 0), Quaternion.Euler(0, 0, -44.95f));

        yield return new WaitForSeconds(0.5f);

        Instantiate(slashAttackObj, new Vector3(0.95f, -0.62f, 0), Quaternion.Euler(0, 0, 135.14f));
        Instantiate(slashAttackObj, new Vector3(-0.93f, 1.22f, 0), Quaternion.Euler(0, 0, -44.95f));

        yield return new WaitForSeconds(0.5f);

        Instantiate(slashAttackObj, new Vector3(0.95f, -0.11f, 0), Quaternion.Euler(0, 0, 135.14f));
        Instantiate(slashAttackObj, new Vector3(-0.93f, 0.71f, 0), Quaternion.Euler(0, 0, -44.95f));

        yield return new WaitForSeconds(0.5f);

        Instantiate(slashAttackObj, new Vector3(0.95f, 0.3f, 0), Quaternion.Euler(0, 0, 135.14f));
        Instantiate(slashAttackObj, new Vector3(-0.93f, 0.3f, 0), Quaternion.Euler(0, 0, -44.95f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(1.95f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(1f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(0, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(-1f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(-1.95f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(-1f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(0, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(1f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(1.95f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(1);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator DoubleKnivesAttack(int length)
    {
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(doubleKnives, new Vector3(3.87f, 2.64f, 0), Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        Instantiate(knife, new Vector3(4.22f, 2.1f, 0), Quaternion.Euler(0, 0, 64.617f));
        Instantiate(knife, new Vector3(4.22f, 0.57f, 0), Quaternion.Euler(0, 0, 64.617f));
        Instantiate(knife, new Vector3(4.22f, -0.97f, 0), Quaternion.Euler(0, 0, 64.617f));

        yield return new WaitForSeconds(2);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator VerticalHandsAttack(int length)
    {
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(redHand, new Vector3(1.25f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(0.21f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(-0.83f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(-1.87f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(-2.91f, -1.88f, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
            Instantiate(redHand, new Vector3(0.75f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(-0.29f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(-1.33f, -1.88f, 0), Quaternion.identity);
            Instantiate(redHand, new Vector3(-2.37f, -1.88f, 0), Quaternion.identity);
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator KnifeFollowAttack()
    {
        Instantiate(knifeFollow, new Vector3(4.09f, 2.24f, 0), Quaternion.Euler(0, 0, 34.815f));
        yield return new WaitForSeconds(0.2f);
        Instantiate(knifeFollow, new Vector3(4.09f, -3.14f, 0), Quaternion.Euler(0, 0, 34.815f));
        yield return new WaitForSeconds(0.2f);
        GameObject newKnife = Instantiate(knifeFollow, new Vector3(-4.09f, -3.14f, 0), Quaternion.Euler(0, 0, 34.815f));
        newKnife.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        GameObject newKnife2 = Instantiate(knifeFollow, new Vector3(-4.09f, 2.24f, 0), Quaternion.Euler(0, 0, 34.815f));
        newKnife2.transform.localScale = new Vector3(-1, 1, 1);

        yield return new WaitForSeconds(16);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator RandomRedHands(int length, bool solo)
    {
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(redHand, new Vector3(Random.Range(leftBorder.position.x, rightBorder.position.x), -6f, 0), Quaternion.Euler(0, 0, Random.Range(-60f, 60f)));
            yield return new WaitForSeconds(0.5f);
            Instantiate(redHand, new Vector3(Random.Range(leftBorder.position.x, rightBorder.position.x), 8f, 0), Quaternion.Euler(0, 0, Random.Range(-226f, -132f)));
            yield return new WaitForSeconds(0.5f);
            Instantiate(redHand, new Vector3(-5f, Random.Range(bottomBorder.position.y, topBorder.position.y), 0), Quaternion.Euler(0, 0, Random.Range(-45f, -135f)));
            yield return new WaitForSeconds(0.5f);
            Instantiate(redHand, new Vector3(5f, Random.Range(bottomBorder.position.y, topBorder.position.y), 0), Quaternion.Euler(0, 0, Random.Range(45f, 135f)));
        }

        if (solo)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(gm.EndAttack());
        }
    }

    public IEnumerator FourKnives()
    {
        AudioManager.instance.PlayAudio("thunder");
        yield return new WaitForSeconds(1);
        Instantiate(fourKnives, new Vector3(3.5f, 1.2f, 0), Quaternion.identity);
        yield return new WaitForSeconds(15);

        yield return new WaitForSeconds(1);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator RandomVerticalRedHands(int length, bool solo)
    {
        for (int i = 0; i < length; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(redHand, new Vector3(Random.Range(leftBorder.position.x + 1f, rightBorder.position.x - 1f), -2.47f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
            Instantiate(redHand, new Vector3(Random.Range(leftBorder.position.x + 1f, rightBorder.position.x - 1f), 2.97f, 0), Quaternion.Euler(0, 0, 180));
        }

        if (solo)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(gm.EndAttack());
        }
    }

    public IEnumerator FastHAndVSlashes()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(slashAttackObj, new Vector3(-0.97f, -1.78f, 0), Quaternion.Euler(0, 0, -43.66f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(-0.97f, -0.53f, 0), Quaternion.Euler(0, 0, -43.66f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(-0.97f, 0.83f, 0), Quaternion.Euler(0, 0, -43.66f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(-0.97f, 2.14f, 0), Quaternion.Euler(0, 0, -43.66f));

        // phase 2

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(1.95f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(0.64f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(0, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(-1f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(-2.07f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        // phase 3

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(1.04f, 2.34f, 0), Quaternion.Euler(0, 0, 135.113f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(1.04f, 1.18f, 0), Quaternion.Euler(0, 0, 135.113f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(1.04f, -0.07f, 0), Quaternion.Euler(0, 0, 135.113f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(1.04f, -1.35f, 0), Quaternion.Euler(0, 0, 135.113f));

        // phase 4

        yield return new WaitForSeconds(0.5f);
        Instantiate(slashAttackObj, new Vector3(-2.07f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(-1f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(0, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(0.64f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(0.15f);
        Instantiate(slashAttackObj, new Vector3(1.95f, -0.79f, 0), Quaternion.Euler(0, 0, 44.448f));

        yield return new WaitForSeconds(1);
        StartCoroutine(gm.EndAttack());
    }

    public IEnumerator AttackBattleBox()
    {
        AudioManager.instance.PlayAudio("something");
        yield return new WaitForSeconds(0.25f);
        Instantiate(something, new Vector3(6.71f, -0.77f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.8f);
        gm.DestroyBattleBox();

        if (!GlobalData.instance.is2Player || gm.p2Dead)
        {
            if (pm.GetHealthToOne() == 0)
            {
                StartCoroutine(pm.DecreaseHealth(1));
            }
            else
            {
                StartCoroutine(pm.DecreaseHealth((int)pm.GetHealthToOne()));
            }
        }
        else if (GlobalData.instance.is2Player)
        {
            if (gm.p1Dead)
            {
                if (p2m.GetHealthToOne() == 0)
                {
                    StartCoroutine(p2m.DecreaseHealth(1));
                }
                else
                {
                    StartCoroutine(p2m.DecreaseHealth((int)p2m.GetHealthToOne()));
                }
            }
            else
            {
                if (pm.GetHealthToOne() == 0)
                {
                    StartCoroutine(pm.DecreaseHealth(1));
                }
                else
                {
                    StartCoroutine(pm.DecreaseHealth((int)pm.GetHealthToOne()));
                }

                if (p2m.GetHealthToOne() == 0)
                {
                    StartCoroutine(p2m.DecreaseHealth(1));
                }
                else
                {
                    StartCoroutine(p2m.DecreaseHealth((int)p2m.GetHealthToOne()));
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(gm.EndAttack());
    }
}
