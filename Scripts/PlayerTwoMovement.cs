using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerTwoMovement : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;

    bool hitDebounce = false;
    bool disableMovement = true;

    [SerializeField] float speed = 5f;
    [SerializeField] Slider playerHealth;
    [SerializeField] GameObject playerHeartSprite;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI emotionText;
    [SerializeField] SpriteRenderer playerSR;
    [SerializeField] Sprite brokenHeart;
    [SerializeField] SpriteRenderer black;
    [SerializeField] Sprite platformerHeart;
    [SerializeField] Sprite platformerHeartBroken;
    [SerializeField] Sprite playerHeartNormal;

    GameManager gm;

    CharacterController2D cc;

    bool isPlatformer = false;
    readonly float platformerSpeed = 20f;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cc = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableMovement) return;
        if (!isPlatformer)
        {
            movement.x = Input.GetAxisRaw("PlayerTwoH");
            movement.y = Input.GetAxisRaw("PlayerTwoV");
        }
        else
        {
            movement.x = Input.GetAxisRaw("PlayerTwoH") * platformerSpeed;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.W) && isJumping)
        {
            isJumping = false;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (disableMovement) return;
        if (!isPlatformer)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement.normalized);
        }
        else
        {
            cc.Move(movement.x * Time.fixedDeltaTime, false, isJumping);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            if (hitDebounce) return;
            hitDebounce = true;

            if (IsAfraid())
            {
                StartCoroutine(DecreaseHealth(Random.Range(115, 163)));
            }
            else
            {
                StartCoroutine(DecreaseHealth(Random.Range(50, 100)));
            }
            GlobalData.instance.gotHit = true;
        }
    }

    IEnumerator IFrames()
    {
        for (int i = 0; i < 2; i++)
        {
            playerHeartSprite.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            playerHeartSprite.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            playerHeartSprite.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            playerHeartSprite.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            playerHeartSprite.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        playerHeartSprite.SetActive(true);
        hitDebounce = false;
    }

    public IEnumerator DecreaseHealth(int damage)
    {
        AudioManager.instance.PlayAudio("heart_hit");

        // game over
        if (playerHealth.value - damage <= 0 && gm.onePlayerDead)
        {
            hitDebounce = true;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
            playerHealth.value = 0;
            playerHealthText.text = "0 / 325";
            gm.allowEscapePress = false;
            Time.timeScale = 0.1f;
            disableMovement = true;
            if (isPlatformer)
            {
                playerSR.sprite = platformerHeartBroken;
            }
            else
            {
                playerSR.sprite = brokenHeart;
            }
            AudioManager.instance.StopAudio("omori");
            AudioManager.instance.StopAudio("alter");
            AudioManager.instance.PlayAudio("dead");
            emotionText.text = "Erased";
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("GameOverScene");
        }
        else if (playerHealth.value - damage <= 0)
        {
            hitDebounce = true;
            disableMovement = true;
            if (isPlatformer)
            {
                playerSR.sprite = platformerHeartBroken;
            }
            else
            {
                playerSR.sprite = brokenHeart;
            }
            gm.onePlayerDead = true;
            gm.p2Dead = true;
            playerHealth.value = 0;
            playerHealthText.text = "0 / 325";
            emotionText.text = "Erased";
            yield return new WaitForSeconds(2);
            playerHealth.value = 0;
            playerHealthText.text = "0 / 325";
            Destroy(gameObject);
            yield break;
        }
        
        StartCoroutine(IFrames());

        for (int i = 0; i < damage; i++)
        {
            playerHealth.value -= 1;
            playerHealthText.text = playerHealth.value + " / 325";

            yield return new WaitForSeconds(0.01f);
        }
        if (Random.Range(0, 100) <= 10)
        {
            emotionText.text = "Afraid";
        }
    }

    public void SetHitDebounce(bool newValue)
    {
        hitDebounce = newValue;
    }

    public void SetDisableMovement(bool newValue)
    {
        disableMovement = newValue;
    }

    public bool GetDisableMovement()
    {
        return disableMovement;
    }

    public float GetHealthToOne()
    {
        return playerHealth.value - 1;
    }

    public void RemoveEmotion()
    {
        emotionText.text = "Neutral";
    }

    public bool IsAfraid()
    {
        return emotionText.text == "Afraid";
    }

    public void SetIsPlatformer(bool newValue)
    {
        isPlatformer = newValue;

        if (newValue)
        {
            rb.gravityScale = 1;
            playerSR.sprite = platformerHeart;
        }
        else
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            playerSR.sprite = playerHeartNormal;
        }
    }

    IEnumerator FadeToBlack()
    {
        float alpha = 0f;

        // fade to black
        for (int i = 0; i < 1000; i++)
        {
            alpha += 0.2f;
            black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);

            if (alpha >= 1f)
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        alpha = 1f;
        black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);
    }
}
