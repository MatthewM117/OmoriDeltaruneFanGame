using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Attacks attacks;

    [SerializeField] Animator bblhAnimator;
    [SerializeField] Animator bbrhAnimator;
    [SerializeField] Animator topLeftAnimator;
    [SerializeField] Animator bottomLeftAnimator;
    [SerializeField] Animator topRightAnimator;
    [SerializeField] Animator bottomRightAnimator;
    [SerializeField] Animator battleBoxAnimator;
    [SerializeField] Animator sunnySlashAnimator;
    [SerializeField] Animator sunnyAnimator;

    [SerializeField] GameObject battleBox;
    [SerializeField] GameObject destroyedBattleBox;

    [SerializeField] Transform player;

    [SerializeField] RectTransform pBoxRectTransform;
    [SerializeField] RectTransform dBoxRectTransform;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI skillDescriptionTitle;
    [SerializeField] TextMeshProUGUI skillDescriptionText;
    [SerializeField] TextMeshProUGUI skillCostText;

    [SerializeField] PlayerMovement pm;

    [SerializeField] RectTransform skillsBox;

    [SerializeField] Button calmDownButton;
    [SerializeField] Button allegroButton;
    [SerializeField] Button attackButton;
    [SerializeField] Button skillsButton;

    [SerializeField] Slider playerHealth;
    [SerializeField] TextMeshProUGUI playerHealthText;

    [SerializeField] Slider playerJuice;
    [SerializeField] TextMeshProUGUI playerJuiceText;

    [SerializeField] GameObject sunnySlash;

    [SerializeField] Slider omoriHealth;
    [SerializeField] GameObject omoriProfile;
    [SerializeField] Button omoriButton;
    [SerializeField] Button omoriButtonNormal;

    [SerializeField] TextMeshProUGUI allegroButtonText;
    [SerializeField] TextMeshProUGUI cherishButtonText;
    [SerializeField] TextMeshProUGUI encoreButtonText;

    [SerializeField] Light2D globalLighting;

    [SerializeField] GameObject sunnyIdle;
    [SerializeField] GameObject sunnyEyesClosed;

    [SerializeField] GameObject whiteBG;
    [SerializeField] GameObject redHandsBG;
    [SerializeField] GameObject hangingBG;
    [SerializeField] GameObject hellMariBG;
    [SerializeField] GameObject redNoHandsBG;

    [SerializeField] SpriteRenderer black;

    [SerializeField] TextMeshProUGUI scaryDialogueText;

    [SerializeField] GameObject dialoguePointerObj;

    [SerializeField] Image white;

    [SerializeField] Transform player2;
    [SerializeField] PlayerTwoMovement p2m;

    [SerializeField] GameObject p2Profile;

    [SerializeField] Slider p2Health;
    [SerializeField] TextMeshProUGUI p2HealthText;

    int currentBattlePhase = 0;
    //int currentBattlePhase = 5;
    bool waitForButtonPress = false;
    bool waitForButtonPressSkill = false;
    bool skillsIsShowing = false;
    bool selectingOmori = false;
    bool selectingOmoriNormal = false;
    int noJuiceTurns = 0;
    bool encoreActive = false;
    bool disableSelection = false;
    bool cherishDialogueActive = false;

    // for all dialogues: put & symbol when that part is over
    readonly string[] cherishDialogue =
        {
            "You remembered KEL's words.",
            "KEL: Friends... Friends are supposed to be there for each other.&",
            "You remembered AUBREY's wish.",
            "AUBREY: I hope you can find some peace... or you know... some happiness.&",
            "You remembered HERO's promise.",
            "HERO: Last time... We made the mistake of leaving each other when we needed each other the most.",
            "HERO: This time... We'll stay together.&",
            "You remembered BASIL's hope.",
            "BASIL: Maybe one day... things can go back to the way they were before.&"
        };
    int currentCherishDialogue = 0;
    bool allowCherish = true;
    bool allowEncore = true;
    bool allowAllegro = true;
    bool allowAttack = true;
    bool advancedFight = false;

    string phase = "white"; // phases: 1st = "white", 2nd = "red", 3rd = "mari" 4th = "hanging"
    //string phase = "hanging";

    readonly string[] omoriDialogue =
    {
        "You've caused so much suffering... yet you do nothing.",
        "And so you've earned nothing in return.&",
        "Your friends will never forgive you.",
        "They'll abandon you like you did them... and that's what you deserve.&",
        "You tell yourself that you don't want to burden others...",
        "But the truth is that you're selfish. You just don't want people to depend on you.&",
        "When do you think about others?",
        "How long are you going to let people take care of you?",
        "You say you care but you're a liar. You've never done anything for anyone else.",
        "You're useless... less than useless. You're sick.&",
        "People like you don't deserve to live.&",
        "People like you don't deserve to live.&",
        "Your friends are wrong about you. The person they love isn't you at all.",
        "You let them believe in a lie to protect yourself.&",
        "You're nothing but a liar... and when they see the truth...",
        "They'll hate you as much as you hate yourself.&",
        "If they know the truth, you'll never be able to regain their trust.",
        "No matter what you do, it will be hopeless.",
        "All you'll do is make things worse. It would be better to just die.&",
        "It would be better to just die.&", // index 19
        "You killed MARI.",
        "She loved you and you killed her.&",
        "HERO loved her and you killed her.&",
        "AUBREY loved her and you killed her.&",
        "KEL loved her and you killed her.&",
        "BASIL loved her and you killed her.&",
        "You loved her and you killed her.&",
        "You loved her and you killed her.&",
        "You loved her and you killed her.&",
        "You should just die.&",
        "You should just die.&"
    };

    bool omoriDialogueActive = false;
    int currentOmoriDialogue = 0;
    //int currentOmoriDialogue = 27;
    bool stopTextAudio = false;

    [HideInInspector] public bool onePlayerDead = false;

    [HideInInspector] public bool p2Dead = false;
    [HideInInspector] public bool p1Dead = false;

    [HideInInspector] public bool allowEscapePress = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        attacks = GetComponent<Attacks>();
        //StartCoroutine(attacks.SlashAttack(10));
        //StartCoroutine(attacks.KnifeAttack(10, false));
        attackButton.Select();

        AudioManager.instance.PlayAudio("omori");
        GlobalData.instance.gotHit = false;

        if (GlobalData.instance.is2Player)
        {
            p2Profile.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && waitForButtonPress && cherishDialogueActive)
        {
            waitForButtonPress = false;
            StartCoroutine(AnimateDialogue(cherishDialogue[currentCherishDialogue]));
            if (currentCherishDialogue < 8)
            {
                currentCherishDialogue++;
            }
            else
            {
                allowCherish = false;
                cherishButtonText.color = Color.gray;
            }
            dialoguePointerObj.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && waitForButtonPress && omoriDialogueActive)
        {
            waitForButtonPress = false;
            if (currentOmoriDialogue < omoriDialogue.Length - 1)
            {
                currentOmoriDialogue++;
            }
            StartCoroutine(AnimateScaryDialogue(omoriDialogue[currentOmoriDialogue]));
            dialoguePointerObj.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && waitForButtonPress)
        {
            waitForButtonPress = false;
            StartCoroutine(CheckOmoriHealth());
            dialoguePointerObj.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && waitForButtonPressSkill)
        {
            waitForButtonPressSkill = false;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
            if (currentBattlePhase == 0)
            {
                currentBattlePhase++;
            }
            StartCoroutine(StartAttack());
            dialogueText.text = "What will SUNNY do?";
            scaryDialogueText.text = string.Empty;
            dialoguePointerObj.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.X) && selectingOmori && skillsIsShowing)
        {
            selectingOmori = false;
            allegroButton.Select();
            omoriProfile.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.X) && skillsIsShowing)
        {
            skillsIsShowing = false;
            skillsBox.DOAnchorPos(new Vector2(0, -165), 0.5f).SetEase(Ease.OutQuad);
            skillsButton.Select();
        }
        else if (Input.GetKeyDown(KeyCode.X) && selectingOmoriNormal)
        {
            selectingOmoriNormal = false;
            attackButton.Select();
            omoriProfile.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && !disableSelection)
        {
            if (skillsIsShowing)
            {
                calmDownButton.Select();
            }
            else if (selectingOmori)
            {
                omoriButton.Select();
            }
            else if (selectingOmoriNormal)
            {
                omoriButtonNormal.Select();
            }
            else
            {
                attackButton.Select();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && allowEscapePress)
        {
            AudioManager.instance.StopAudio("omori");
            AudioManager.instance.StopAudio("alter");
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void AnimateBattleBox(bool halves)
    {
        if (halves)
        {
            bbrhAnimator.Play("BattleBoxRightHalfAnims", 0, -1f);
            bblhAnimator.Play("split_left", 0, -1f);
            return;
        }

        topLeftAnimator.Play("TLH_split_up", 0, -1f);
        bottomLeftAnimator.Play("BLH_split_down", 0, -1f);
        topRightAnimator.Play("TRH_split_up", 0, -1f);
        bottomRightAnimator.Play("BRH_split_down", 0, -1f);
    }

    IEnumerator AnimateDialogue(string theText)
    {
        bool nonono = false;
        dialogueText.text = string.Empty;
        scaryDialogueText.text = string.Empty;
        stopTextAudio = false;
        StartCoroutine(PlayTextAudio());
        int length = theText.ToCharArray().Length;
        int i = 0;
        foreach (char c in theText.ToCharArray())
        {
            if (c == '&')
            {
                cherishDialogueActive = false;
                waitForButtonPressSkill = true;
                nonono = true;
                StartCoroutine(HealPlayer(325));
                StartCoroutine(HealJuice(55));
                stopTextAudio = true;
                break;
            }

            dialogueText.text += c;

            if (c == '.')
            {
                stopTextAudio = true;
                yield return new WaitForSeconds(0.5f);
                if (i != length - 1 && i != length - 2)
                {
                    StartCoroutine(PlayTextAudio());
                }
            }

            yield return new WaitForSeconds(0.025f);
            i++;
        }
        stopTextAudio = true;
        if (!nonono)
        {
            waitForButtonPress = true;
        }
        dialoguePointerObj.SetActive(true);
    }

    IEnumerator AnimateScaryDialogue(string theText)
    {
        dialogueText.text = string.Empty;
        scaryDialogueText.text = string.Empty;
        stopTextAudio = false;
        StartCoroutine(PlayTextAudio());
        int length = theText.ToCharArray().Length;
        int i = 0;
        foreach (char c in theText.ToCharArray())
        {
            if (c == '&')
            {
                omoriDialogueActive = false;
                stopTextAudio = true;
                break;
            }

            scaryDialogueText.text += c;

            if (c == '.')
            {
                stopTextAudio = true;
                yield return new WaitForSeconds(0.5f);
                if (i != length - 2 && i != length - 1)
                {
                    StartCoroutine(PlayTextAudio());
                }
            }

            yield return new WaitForSeconds(0.025f);
            i++;
        }
        stopTextAudio = true;
        waitForButtonPress = true;
        dialoguePointerObj.SetActive(true);
    }

    IEnumerator PlayTextAudio()
    {
        while (true)
        {
            if (stopTextAudio)
            {
                stopTextAudio = false;
                break;
            }
            AudioManager.instance.PlayAudio("text");
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void DestroyBattleBox()
    {
        Instantiate(destroyedBattleBox, new Vector3(1.599445f, 1.175163f, 0), Quaternion.identity);
        battleBox.SetActive(false);
    }

    void ShowBattleBox()
    {
        battleBox.SetActive(true);
        battleBoxAnimator.Play("spin", 0, 0f);
    }

    public IEnumerator EndAttack()
    {
        if (!p1Dead)
        {
            pm.SetDisableMovement(true);
            pm.SetHitDebounce(true);
            player.DOMove(new Vector3(-7.19f, -0.75f, 0), 1f);
        }

        if (GlobalData.instance.is2Player && !p2Dead)
        {
            p2m.SetDisableMovement(true);
            p2m.SetHitDebounce(true);
            player2.DOMove(new Vector3(11.05f, 6.83f, 0), 1f);
        }

        yield return new WaitForSeconds(0.5f);

        battleBox.SetActive(false);

        if (currentOmoriDialogue == omoriDialogue.Length - 1)
        {
            StartCoroutine(YouWin());
            yield return new WaitForSeconds(10);
        }

        disableSelection = false;

        if ((currentBattlePhase >= 5 && advancedFight && currentOmoriDialogue < 19) || phase == "something")
        {
            disableSelection = true;
            omoriDialogueActive = true;
            dialogueText.text = string.Empty;
            dBoxRectTransform.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            if (currentOmoriDialogue != 0 && currentOmoriDialogue < omoriDialogue.Length - 1)
            {
                currentOmoriDialogue++;
            }
            StartCoroutine(AnimateScaryDialogue(omoriDialogue[currentOmoriDialogue]));
        }
        else
        {
            StartCoroutine(CheckOmoriHealth());
        }

        if (encoreActive)
        {
            noJuiceTurns++;
            if (noJuiceTurns > 3)
            {
                encoreActive = false;
                noJuiceTurns = 0;
                allowEncore = true;
                encoreButtonText.color = Color.white;
            }
        }
    }

    IEnumerator CheckOmoriHealth()
    {
        if (omoriHealth.value <= 0 && phase == "white")
        {
            phase = "red";
            omoriHealth.value = omoriHealth.maxValue;
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(2);
            disableSelection = true;
            dialogueText.text = string.Empty;
            dBoxRectTransform.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(AnimateDialogue("OMORI did not succumb."));
            redHandsBG.SetActive(true);
            whiteBG.SetActive(false);
        }
        else if (omoriHealth.value <= 0 && phase == "red")
        {
            phase = "mari";
            omoriHealth.value = omoriHealth.maxValue;
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(2);
            disableSelection = true;
            dialogueText.text = string.Empty;
            dBoxRectTransform.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(AnimateDialogue("OMORI did not succumb."));
            hellMariBG.SetActive(true);
            redNoHandsBG.SetActive(true);
            redHandsBG.SetActive(false);
            AudioManager.instance.StopAudio("omori");
            AudioManager.instance.PlayAudio("alter");
        }
        else if (omoriHealth.value <= 0 && phase == "mari")
        {
            phase = "hanging";
            omoriHealth.value = omoriHealth.maxValue;
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(2);
            disableSelection = true;
            dialogueText.text = string.Empty;
            dBoxRectTransform.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(AnimateDialogue("OMORI will not succumb."));
            hangingBG.SetActive(true);
            hellMariBG.SetActive(false);
        }
        else if (omoriHealth.value <= 0 && phase == "hanging")
        {
            phase = "something";
            //omoriHealth.value = omoriHealth.maxValue;
            StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(2);
            disableSelection = true;
            dialogueText.text = string.Empty;
            dBoxRectTransform.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(AnimateDialogue("OMORI will not succumb."));
            //hangingBG.SetActive(true);
            //hellMariBG.SetActive(false);
        }
        else
        {
            disableSelection = false;
            dBoxRectTransform.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
            pBoxRectTransform.DOAnchorPos(new Vector2(0, 380), 0.5f).SetEase(Ease.OutQuad);
            attackButton.Select();
            StartCoroutine(FadeBack());
            dialogueText.text = "What will SUNNY do?";
            scaryDialogueText.text = string.Empty;
        }
    }

    public void NextAttack()
    {
        if (!p1Dead)
        {
            if (!pm.GetDisableMovement()) return;
        }

        if (!allowAttack)
        {
            AudioManager.instance.PlayAudio("buzzer");
            return;
        }
        
        selectingOmoriNormal = true;
        omoriButtonNormal.Select();
        omoriProfile.SetActive(true);
        AudioManager.instance.PlayAudio("sys_select");
    }

    public void StartNextAttack()
    {
        if (!p1Dead)
        {
            if (!pm.GetDisableMovement()) return;
        }
        selectingOmoriNormal = false;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        disableSelection = true;
        omoriProfile.SetActive(false);
        if (phase == "white" && currentBattlePhase < 4)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "white")
        {
            advancedFight = false;
        }
        else if (phase == "red" && currentBattlePhase < 9)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "red")
        {
            advancedFight = false;
        }
        else if (phase == "mari" && currentBattlePhase < 11)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "mari")
        {
            advancedFight = false;
        }
        else if (phase == "hanging" && currentBattlePhase < 14)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "hanging")
        {
            advancedFight = true;
        }
        AudioManager.instance.PlayAudio("sys_select");
        StartCoroutine(AttackOmori());
    }

    IEnumerator AttackOmori()
    {
        sunnySlash.SetActive(true);
        int damage = Random.Range(25, 50);
        omoriHealth.value -= damage;
        dialogueText.text = "SUNNY attacks! OMORI takes " + damage + " damage!";
        sunnyAnimator.Play("allegro_attack", 0, 0f);
        sunnySlashAnimator.Play("sunny_slash", 0, 0f);
        AudioManager.instance.PlayAudio("violin_attack");
        yield return new WaitForSeconds(0.5f);
        sunnySlash.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartAttack());
    }

    public void DoAllegro()
    {
        if (!p1Dead)
        {
            if (!pm.GetDisableMovement()) return;
        }
        if (playerJuice.value < 19 || !allowAllegro)
        {
            AudioManager.instance.PlayAudio("buzzer");
            return;
        }

        selectingOmori = true;
        omoriButton.Select();
        omoriProfile.SetActive(true);
        AudioManager.instance.PlayAudio("sys_select");
    }

    public void StartAllegro()
    {
        if (!p1Dead)
        {
            if (!pm.GetDisableMovement()) return;
        }
        skillsIsShowing = false;
        selectingOmori = false;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        disableSelection = true;
        omoriProfile.SetActive(false);
        if (phase == "white" && currentBattlePhase < 4)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "white")
        {
            advancedFight = false;
        }
        else if (phase == "red" && currentBattlePhase < 9)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "red")
        {
            advancedFight = false;
        }
        else if (phase == "mari" && currentBattlePhase < 11)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "mari")
        {
            advancedFight = false;
        }
        else if (phase == "hanging" && currentBattlePhase < 17)
        {
            currentBattlePhase++;
            advancedFight = true;
        }
        else if (phase == "hanging")
        {
            advancedFight = true;
        }
        AudioManager.instance.PlayAudio("sys_select");
        StartCoroutine(AllegroAttack());
    }

    IEnumerator AllegroAttack()
    {
        sunnySlash.SetActive(true);
        int damage = Random.Range(120, 160);
        omoriHealth.value -= damage;
        dialogueText.text = "SUNNY strikes three times. OMORI took " + damage + " damage!";
        skillsBox.DOAnchorPos(new Vector2(0, -165), 0.5f).SetEase(Ease.OutQuad);
        sunnyAnimator.Play("allegro_attack", 0, 0f);
        sunnySlashAnimator.Play("allegro", 0, 0f);

        AudioManager.instance.PlayAudio("violin_attack");
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlayAudio("violin_attack");
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlayAudio("violin_attack");

        yield return new WaitForSeconds(0.2f);
        sunnySlash.SetActive(false);

        if (!encoreActive)
        {
            for (int i = 0; i < 19; i++)
            {
                playerJuice.value -= 1;
                playerJuiceText.text = playerJuice.value + "/55";
                yield return new WaitForSeconds(0.01f);
            }
        }

        if (playerJuice.value < 19)
        {
            allegroButtonText.color = Color.gray;
        }
        else
        {
            allegroButtonText.color = Color.white;
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        stopTextAudio = true;
        ShowBattleBox();
        sunnyIdle.SetActive(true);
        sunnyEyesClosed.SetActive(false);
        StartCoroutine(UnDimGlobalLights());
        

        if (GlobalData.instance.is2Player && !p2Dead)
        {
            player2.DOMove(new Vector3(0, 0.39f, 0), 1f);
            p2m.SetDisableMovement(false);
            Debug.Log("SHOULD BE HERE!!");
        }

        pBoxRectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.OutQuad);
        dBoxRectTransform.DOAnchorPos(new Vector2(0, -165), 0.5f).SetEase(Ease.OutQuad);

        if (!p1Dead)
        {
            player.DOMove(new Vector3(0, 0.39f, 0), 1f);
            pm.SetDisableMovement(false);
        }
        
        yield return new WaitForSeconds(1);

        if (!p1Dead)
        {
            pm.SetHitDebounce(false);
        }
        
        if (GlobalData.instance.is2Player && !p2Dead)
        {
            p2m.SetHitDebounce(false);
        }

        int nextBattle;

        if (currentBattlePhase == 0)
        {
            currentBattlePhase++;
        }

        if (!advancedFight)
        {
            if (currentBattlePhase > 17)
            {
                nextBattle = Random.Range(15, 18);
            }
            else
            {
                nextBattle = Random.Range(1, currentBattlePhase + 1);
            }
        }
        else
        {
            nextBattle = currentBattlePhase;
        }

        if (phase == "something")
        {
            StartCoroutine(attacks.AttackBattleBox());
            allowAllegro = false;
            allowCherish = false;
            allowEncore = false;
            allowAttack = false;
            allegroButtonText.color = Color.gray;
            encoreButtonText.color = Color.gray;
            cherishButtonText.color = Color.gray;
        }
        else
        {
            switch (nextBattle)
            {
                case 1:
                    StartCoroutine(attacks.SlashAttack(10, true));
                    break;
                case 2:
                    StartCoroutine(attacks.KnifeAttack(10, false, true));
                    break;
                case 3:
                    StartCoroutine(attacks.XSlash());
                    break;
                case 4:
                    StartCoroutine(attacks.KnifeAttack(15, true, true));
                    break;
                case 5:
                    StartCoroutine(attacks.SplitBoxAttack());
                    break;
                case 6:
                    StartCoroutine(attacks.RedHandsAttack(50, true));
                    break;
                case 7:
                    StartCoroutine(attacks.HorizontalAndVerticalSlashes());
                    break;
                case 8:
                    StartCoroutine(attacks.DoubleKnivesAttack(10));
                    break;
                case 9:
                    StartCoroutine(attacks.VerticalHandsAttack(10));
                    break;
                case 10:
                    StartCoroutine(attacks.KnifeFollowAttack());
                    break;
                case 11:
                    StartCoroutine(attacks.RandomRedHands(5, true));
                    break;
                case 12:
                    StartCoroutine(attacks.FourKnives());
                    break;
                case 13:
                    StartCoroutine(attacks.RandomVerticalRedHands(10, true));
                    break;
                case 14:
                    StartCoroutine(attacks.FastHAndVSlashes());
                    break;
                case 15:
                    StartCoroutine(attacks.SlashAttack(20, false));
                    yield return new WaitForSeconds(0.2f);
                    StartCoroutine(attacks.KnifeAttack(17, true, true));
                    break;
                case 16:
                    StartCoroutine(attacks.SlashAttack(20, false));
                    StartCoroutine(attacks.RandomRedHands(5, true));
                    break;
                case 17:
                    StartCoroutine(attacks.RandomRedHands(5, false));
                    StartCoroutine(attacks.KnifeAttack(17, true, true));
                    break;
            }
        }
    }

    public void Encore()
    {
        if (!allowEncore)
        {
            AudioManager.instance.PlayAudio("buzzer");
            return;
        }
        skillsIsShowing = false;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        disableSelection = true;
        encoreActive = true;
        allowEncore = false;
        encoreButtonText.color = Color.gray;
        sunnyEyesClosed.SetActive(true);
        sunnyIdle.SetActive(false);
        StartCoroutine(DimGlobalLights());
        skillsBox.DOAnchorPos(new Vector2(0, -165), 0.5f).SetEase(Ease.OutQuad);
        dialogueText.text = "SUNNY gathered himself...";
        StartCoroutine(ButtonCooldown());
        advancedFight = false;
        AudioManager.instance.PlayAudio("sys_select");
    }

    public void SetSkillDescriptionText(string title, string desc, string cost)
    {
        skillDescriptionTitle.text = title;
        skillDescriptionText.text = desc;
        skillCostText.text = "Cost: " + cost;
    }

    public void OnSkillPress()
    {
        skillsBox.DOAnchorPos(new Vector2(0, 113), 0.5f).SetEase(Ease.OutQuad);
        calmDownButton.Select();
        skillsIsShowing = true;
        AudioManager.instance.PlayAudio("sys_select");
    }

    public void CalmDown()
    {
        skillsIsShowing = false;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        disableSelection = true;
        sunnyEyesClosed.SetActive(true);
        sunnyIdle.SetActive(false);
        StartCoroutine(DimGlobalLights());
        skillsBox.DOAnchorPos(new Vector2(0, -165), 0.5f).SetEase(Ease.OutQuad);
        int recovery = 162;
        StartCoroutine(HealPlayer(recovery));
        dialogueText.text = "SUNNY calms down. SUNNY recovered " + recovery + " HEART!";

        if (!p1Dead)
        {
            if (pm.IsAfraid())
            {
                pm.RemoveEmotion();
            }
        }

        if (GlobalData.instance.is2Player && !p2Dead)
        {
            if (p2m.IsAfraid())
            {
                p2m.RemoveEmotion();
            }
        }

        StartCoroutine(ButtonCooldown());
        advancedFight = false;
        AudioManager.instance.PlayAudio("sys_select");
    }

    public void Cherish()
    {
        if (!allowCherish)
        {
            AudioManager.instance.PlayAudio("buzzer");
            return;
        }
        skillsIsShowing = false;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        disableSelection = true;
        sunnyEyesClosed.SetActive(true);
        sunnyIdle.SetActive(false);
        StartCoroutine(DimGlobalLights());
        skillsBox.DOAnchorPos(new Vector2(0, -165), 0.5f).SetEase(Ease.OutQuad);
        cherishDialogueActive = true;
        dialogueText.text = "SUNNY steadies his breathing.";

        if (!p1Dead)
        {
            if (pm.IsAfraid())
            {
                pm.RemoveEmotion();
            }
        }

        if (GlobalData.instance.is2Player && !p2Dead)
        {
            if (p2m.IsAfraid())
            {
                p2m.RemoveEmotion();
            }
        }

        StartCoroutine(ButtonCooldown2());
        advancedFight = false;
        AudioManager.instance.PlayAudio("sys_select");
    }
    
    IEnumerator DimGlobalLights()
    {
        for (int i = 0; i < 100; i++)
        {
            globalLighting.intensity -= 0.05f;
            if (globalLighting.intensity <= 0.05f)
            {
                globalLighting.intensity = 0.05f;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator UnDimGlobalLights()
    {
        for (int i = 0; i < 100; i++)
        {
            if (globalLighting.intensity == 1f) break;
            globalLighting.intensity += 0.05f;
            if (globalLighting.intensity >= 1f)
            {
                globalLighting.intensity = 1f;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ButtonCooldown()
    {
        yield return new WaitForSeconds(2);
        waitForButtonPressSkill = true;
        dialoguePointerObj.SetActive(true);
    }

    IEnumerator ButtonCooldown2()
    {
        yield return new WaitForSeconds(1);
        waitForButtonPress = true;
        dialoguePointerObj.SetActive(true);
    }

    IEnumerator HealPlayer(int healAmount)
    {
        if (!GlobalData.instance.is2Player)
        {
            for (int i = 0; i < healAmount; i++)
            {
                playerHealth.value += 1;
                playerHealthText.text = playerHealth.value + " / 325";
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (GlobalData.instance.is2Player && !onePlayerDead)
        {
            StartCoroutine(HealPlayer2(healAmount));
            for (int i = 0; i < healAmount; i++)
            {
                playerHealth.value += 1;
                playerHealthText.text = playerHealth.value + " / 325";
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (GlobalData.instance.is2Player && p1Dead)
        {
            StartCoroutine(HealPlayer2(healAmount));
        }
        else if (GlobalData.instance.is2Player && p2Dead)
        {
            for (int i = 0; i < healAmount; i++)
            {
                playerHealth.value += 1;
                playerHealthText.text = playerHealth.value + " / 325";
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator HealPlayer2(int healAmount)
    {
        for (int i = 0; i < healAmount; i++)
        {
            p2Health.value += 1;
            p2HealthText.text = p2Health.value + " / 325";
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator HealJuice(int healAmount)
    {
        for (int i = 0; i < healAmount; i++)
        {
            playerJuice.value += 1;
            playerJuiceText.text = playerJuice.value + "/55";
            yield return new WaitForSeconds(0.01f);
        }
        allegroButtonText.color = Color.white;
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

            yield return new WaitForSeconds(0.05f);
        }

        alpha = 1f;
        black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);
    }

    IEnumerator FadeBack()
    {
        if (black.color.a > 0f)
        {
            float alpha = 1f;
            // fade back to normal
            for (int i = 0; i < 1000; i++)
            {
                alpha -= 0.1f;
                black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);

                if (alpha <= 0f)
                {
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

            alpha = 0f;
            black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);
        }
    }

    public void RunButton()
    {
        AudioManager.instance.PlayAudio("buzzer");
    }

    IEnumerator FadeToWhite()
    {
        float alpha = 0f;

        // fade to white
        for (int i = 0; i < 1000; i++)
        {
            alpha += 0.2f;
            white.color = new Color(white.color.r, white.color.g, white.color.b, alpha);

            if (alpha >= 1f)
            {
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        alpha = 1f;
        white.color = new Color(white.color.r, white.color.g, white.color.b, alpha);
    }

    IEnumerator YouWin()
    {
        disableSelection = true;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        StartCoroutine(FadeToWhite());
        AudioManager.instance.StopAudio("alter");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("WinScene");
    }
}
