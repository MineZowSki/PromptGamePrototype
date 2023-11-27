using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public abstract class Prompts : PromptSetting
{
    [Tooltip("場景第一個Prompt")] public FourButton fourButton => transform.GetComponentInChildren<FourButton>();
    [Header("基本設定")]
    [Tooltip("玩家物件")] public GameObject playerGameObject;
    [Tooltip("目前場景的狀態")] private GameState state;
    [Tooltip("場景Prompt的List")] protected List<GameObject> getGameObjects = new List<GameObject>(PromptLength);
    [Tooltip("玩家Class")] protected Player player;
    [Tooltip("這是放滿場景Prompt用的")] private List<GameObject> promptsSpawned;
    [Space(10)]
    [Header("時間相關")]
    [Tooltip("玩家在單個Stage的平均輸入時間")] private float averageTime;
    [Tooltip("玩家在單個Prompt之間所花的時間")] private float totalTime;
    [Space(10)]
    [Header("在場相關")]
    [Tooltip("玩家在單個Stage對Boss造成的傷害")] private float playerTotalDealtDamage;
    [Tooltip("玩家在此場景的正確輸入數量")] private int correctInput;
    [Tooltip("玩家在此場景的錯誤輸入數量")] private int wrongInput;
    [Tooltip("此場景的剩餘Stage")] private int stageClear;
    //private int spawnPosition;
    [Space(10)]
    [Header("動畫相關")]
    [Tooltip("全部結算動畫是否結束")] private bool allAnimationDone = false;
    [Tooltip("玩家是否跳過動畫")] private bool skipAnimation = false;
    [Tooltip("動畫是否已結束")] private bool animationFinished = true;
    [Tooltip("離場與結算之間的黑屏的動畫確認")] private bool blackout = false;
    [Header("Boss相關")]
    [Tooltip("這是用來取得BossRush最高傷害用的")] private List<float> bossRushDamageResult = new List<float>();
    [Tooltip("這是BossRush的時間")] private float bossRushTime;
    [Tooltip("這是在BossRush的連擊")] private int playerHitOnBoss = 0;
    [Tooltip("這是用來判斷BossRush初始化的")] private bool BossRushOnAndOff = true;
    [Space(10)]
    [Header("結算相關")]
    [Tooltip("(結算用)玩家的最快輸入時間的List")] private List<float> playerReactTimeList = new List<float>();
    [Tooltip("(結算用)玩家的最快平均輸入時間的List")] private List<float> playerAverageReactTimeList = new List<float>();
    [Tooltip("(結算用)玩家的最高Boss連擊")] private List<int> playerMaxHitOnBossList = new List<int>();
    [Space(10)]
    [Header("任務相關")]
    [Tooltip("(任務用)這是計算玩家取得物品數量用的")] protected Dictionary<AllItem, int> currentSceneObtainedItem = new Dictionary<AllItem, int>();
    [Space(10)]
    [Header("其他")]
    [Tooltip("遊戲結束，最終Boss才會用到，一般請調False或不要使用")] private bool endGame;
    [Space(10)]
    [Header("聲音相關，之後會刪掉變成裝備與Prompt之間產生的AudioClip")]
    public AudioClip correctInputSound;
    public AudioClip wrongInputSound;
    protected override void Awake()
    {
        base.Awake();
        player = playerGameObject.GetComponent<Player>();
    }
    protected override void Start()
    {
        base.Start();
        SoundManager.instance.PlayBGM(currentSceneBGM, 0.2f);
        totalTime = 0f;
        ParameterSetting(player, true);
        stageClear = currentSceneTotalStage;
        wrongInput = 0;
        UIManager.instance.bestBossRushCombo.gameObject.SetActive(true);
        UIManager.instance.stageClear.gameObject.SetActive(!currentSceneBossSceneOrNot);
        UIManager.instance.playerPatience.fillAmount = 1f;
        UIManager.instance.stageClear.text = currentSceneTotalStage.ToString();
        InventoryManager.instance.inventoryOnOff.SetActive(false);
        EventHandler.CallPlayerCanOpenInventoryOrNot(false);
        ChangeState(GameState.SpawnPrompt);
    }
    protected virtual void ChangeState(GameState gameState)
    {
        state = gameState;
        switch (gameState)
        {
            case GameState.SpawnPrompt:
                UIManager.instance.searchingInput.GetComponent<TextMeshProUGUI>().text = "Searching...";
                UIManager.instance.searchingInput.SetActive(true);
                StartCoroutine(SpawnPrompts());
                break;
            case GameState.PlayerInput:
                UIManager.instance.searchingInput.SetActive(false);
                break;
            case GameState.WrongInput:
                StartCoroutine(PressingWrong(currentSceneDestroyAllOrNot));
                break;
            case GameState.BossRush:
                UIManager.instance.searchingInput.GetComponent<TextMeshProUGUI>().text = "BossRush!!!";
                UIManager.instance.searchingInput.SetActive(true);
                break;
            case GameState.BossDealtDamage:
                StartCoroutine(BossDealtDamage());
                break;
            case GameState.LeavingLevel:
                player.playerTotalWrongInput += wrongInput;
                MissionList.sceneCompleteDict.Clear();
                MissionList.sceneCompleteDict.Add(sceneDefaultParameter.sceneName, true);
                MissionList.itemCompleteDict = currentSceneObtainedItem;
                UIManager.instance.leaveStage.SetActive(true);
                playerReactTimeList.Sort();
                playerAverageReactTimeList.Sort();
                playerMaxHitOnBossList.Sort();
                UIManager.instance.RankingTimer(playerReactTimeList.Count != 0 ? playerReactTimeList.First() : 100f, UIManager.instance.bestInputTime);
                UIManager.instance.RankingTimer(playerAverageReactTimeList.Count != 0 ? playerAverageReactTimeList.First() : 100f, UIManager.instance.bestAverageTime);
                UIManager.instance.bestInputTime.text = playerReactTimeList.Count != 0 ? playerReactTimeList.First().ToString("F2") : "0";
                UIManager.instance.bestAverageTime.text = playerAverageReactTimeList.Count != 0 ? playerAverageReactTimeList.First().ToString("F2") : "0";
                if (!currentSceneBossSceneOrNot) UIManager.instance.bestBossRushCombo.gameObject.SetActive(false);
                else UIManager.instance.bestBossRushCombo.text = playerMaxHitOnBossList.Count != 0 ? playerMaxHitOnBossList.Last().ToString("00") : "0";
                break;
        }
    }
    public IEnumerator SpawnPrompts()
    {
        promptsSpawned = getGameObjects;
        while (promptsSpawned.Count < Utilities.GameMaxPrompts)
        {
            if (promptsSpawned.Count == 0) break;
            promptsSpawned.AddRange(promptsSpawned);
        }
        while (promptsSpawned.Count > Utilities.GameMaxPrompts)
        {
            promptsSpawned.RemoveAt(promptsSpawned.Count - 1);
        }
        int promptNumber = Random.value > currentSceneMinPromptOdds ? currentSceneMaxPromptsNumber : currentSceneMinPromptsNumber;
        for (int i = 0; i < promptNumber; i++)
        {
            yield return new WaitForSeconds(currentScenePromptEveryPopTime);
            int random = Random.Range(0, Utilities.GameMaxPrompts);
            var whichPromptSpawned = Instantiate(promptsSpawned[random], new Vector3Int(i < 15 ? -7 + i : i - 22, i < 15 ? 0 : -1, 0), Quaternion.identity, transform);
            SoundManager.instance.PlaySound(whichPromptSpawned.GetComponent<FourButton>().promptSO.promptAudio, 0.3f);
            whichPromptSpawned.GetComponent<FourButton>().button = InputsManager.instance.defaultKeyBinding[random % currentScenePromptTypeNumber];
        }
        ChangeState(GameState.PlayerInput);
    }
    public IEnumerator RightInput()
    {
        correctInput++;
        inputTime = totalTime;
        if (player.isPlayerMainEquipmentEquipped && PlayerInput.mainEquipmentInput)
        {
            PlayerImpactOnInput(player);
            playerTotalDealtDamage += player.playerMainEquipmentDamageDelta;
            if (player.playerMainEquipmentType == EquipmentType.MAIN || player.playerMainEquipmentType == EquipmentType.NOT_RESTRICTED) player.EquipmentWearDown(fourButton.promptSO.promptWearing, player.playerInfo.playerMainEquipment);
            else player.EquipmentWearDown(2 * fourButton.promptSO.promptWearing, player.playerInfo.playerMainEquipment);
        }
        else if (player.isPlayerSecondaryEquipment1Equipped && PlayerInput.secondaryEquipmentInput0)
        {
            playerTotalDealtDamage += player.playerSecondaryEquipment1DamageDelta;
            if (player.playerSecondaryEquipment1Type == EquipmentType.SECONDARY || player.playerSecondaryEquipment1Type == EquipmentType.NOT_RESTRICTED) player.EquipmentWearDown(fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment1);
            else player.EquipmentWearDown(2 * fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment1);
        }
        else if (player.isPlayerSecondaryEquipment2Equipped && PlayerInput.secondaryEquipmentInput1)
        {
            playerTotalDealtDamage += player.playerSecondaryEquipment2DamageDelta;
            if (player.playerSecondaryEquipment2Type == EquipmentType.SECONDARY || player.playerSecondaryEquipment2Type == EquipmentType.NOT_RESTRICTED) player.EquipmentWearDown(fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment2);
            else player.EquipmentWearDown(2 * fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment2);
        }
        else if (player.isPlayerSecondaryEquipment3Equipped && PlayerInput.secondaryEquipmentInput2)
        {
            playerTotalDealtDamage += player.playerSecondaryEquipment3DamageDelta;
            if (player.playerSecondaryEquipment3Type == EquipmentType.SECONDARY || player.playerSecondaryEquipment3Type == EquipmentType.NOT_RESTRICTED) player.EquipmentWearDown(fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment3);
            else player.EquipmentWearDown(2 * fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment3);
        }
        else if (player.isPlayerSecondaryEquipment4Equipped && PlayerInput.secondaryEquipmentInput3)
        {
            playerTotalDealtDamage += player.playerSecondaryEquipment4DamageDelta;
            if (player.playerSecondaryEquipment4Type == EquipmentType.SECONDARY || player.playerSecondaryEquipment4Type == EquipmentType.NOT_RESTRICTED) player.EquipmentWearDown(fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment4);
            else player.EquipmentWearDown(2 * fourButton.promptSO.promptWearing, player.playerInfo.playerSecondaryEquipment4);
        }
        else
        {
            playerTotalDealtDamage += player.playerDamageDelta;
        }
        playerReactTimeList.Add(inputTime);
        averageTime += inputTime;
        player.playerEXP += fourButton.promptSO.promptEXP;
        UIManager.instance.RankingTimer(inputTime, UIManager.instance.inputTime);
        SoundManager.instance.PlaySound(correctInputSound);
        if (inputTime < 100f) UIManager.instance.inputTime.text = inputTime.ToString("F2");
        else UIManager.instance.inputTime.text = inputTime.ToString("000");
        totalTime = 0f;
        float byproductChance = Random.value;
        var action = Instantiate(UIManager.instance.actionText, UIManager.instance.actionHolder.transform).GetComponent<TextMeshProUGUI>();
        var itemCondition = ItemCondition.None;
        if (!fourButton.promptSO.promptIsObtainableOrNot) itemCondition = ItemCondition.PromptCanNotBeAnItem;
        else if (fourButton.promptSO.promptTransferToItem == null) Debug.Log("This Prompt hasnt implement item yet");
        else if (!fourButton.promptSO.promptTransferToItem.itemIsCurrentlyObatainableOrNot) itemCondition = ItemCondition.PlayerCanNotGetThisItemYet;
        else if (InventoryManager.instance.inventoryFull(fourButton.promptSO.promptTransferToItem.itemEnum)) itemCondition = ItemCondition.PlayerInventoryIsFull;
        else if (fourButton.promptSO.containsByproduct && byproductChance < fourButton.promptSO.byproductOdds) itemCondition = ItemCondition.ItemHasDescendedToByProduct;
        else itemCondition = ItemCondition.PlayerGetThisItem;
        switch (itemCondition)
        {
            case ItemCondition.None:
                action.text = "Nothing happened";
                break;
            case ItemCondition.PromptCanNotBeAnItem:
                action.text = "This is not obtainable";
                break;
            case ItemCondition.PlayerCanNotGetThisItemYet:
                action.text = "You're unable to obtained " + fourButton.promptSO.promptName_English;
                break;
            case ItemCondition.PlayerInventoryIsFull:
                action.text = "Inventory Full";
                break;
            case ItemCondition.ItemHasDescendedToByProduct:
                action.text = "Obtained " + fourButton.promptSO.byproductList[Utilities.ByproductWeighted(fourButton.promptSO.eachByproductOdds)];
                InventoryManager.instance.playerInventoryForTransfer.byproductList.Add(fourButton.promptSO.byproductList[Utilities.ByproductWeighted(fourButton.promptSO.eachByproductOdds)].byproductName_English);
                break;
            case ItemCondition.PlayerGetThisItem:
                action.text = "Obtained " + fourButton.promptSO.promptName_English;
                InventoryManager.instance.playerInventoryForTransfer.promptItemList.Add(fourButton.promptSO);
                currentSceneObtainedItem[fourButton.promptSO.promptTransferToItem.itemEnum] += 1;
                PromptToResult();
                break;
            default:
                action.text = "Unawared Behaviour Has Occured";
                break;
        }
        Destroy(fourButton.gameObject);
        yield return null;
    }
    public IEnumerator WrongInput()
    {
        averageTime += 1f;
        player.playerCurrentHealth -= fourButton.promptSO.promptDamage;
        wrongInput++;
        UIManager.instance.playerPatience.fillAmount -= currentSceneWrongInputPatincePenalty;
        var action = Instantiate(UIManager.instance.actionText, UIManager.instance.actionHolder.transform);
        action.GetComponent<TextMeshProUGUI>().text = wrongPromptAction();
        SoundManager.instance.PlaySound(wrongInputSound);
        ChangeState(GameState.WrongInput);
        yield return null;
    }
    public IEnumerator PressingWrong(bool destroyAll)
    {
        if (!destroyAll)
        {
            Destroy(fourButton.gameObject);
            yield return null;
            ChangeState(GameState.PlayerInput);
        }
        if (destroyAll)
        {
            foreach (var prompt in GetComponentsInChildren<FourButton>())
            {
                if (prompt != null) Destroy(prompt.gameObject);
                yield return new WaitForSeconds(currentScenePromptRespawnTime);
            }
            ChangeState(GameState.PlayerInput);
        }
    }
    public void PromptToResult()
    {
        GameObject temp = Instantiate(fourButton.gameObject, playerGameObject.transform.GetChild(0).transform.position, Quaternion.identity, playerGameObject.transform.GetChild(0).transform);
        temp.transform.position = new Vector3(1000f, 1000f);
    }
    public void MatchPrompts()
    {
        totalTime += Time.deltaTime;
        var levelDifficulty = PromptDefaultInt * currentScenePatienceMinus;
        #region 玩家的下一個指令
        if (fourButton != null) UIManager.instance.nextInput.text = fourButton.button;
        if ((PlayerInput.input0 && PlayerInput.input1 && PlayerInput.input2 && PlayerInput.input3) ||
            (PlayerInput.input0 && PlayerInput.input1 && PlayerInput.input2) ||
            (PlayerInput.input1 && PlayerInput.input2 && PlayerInput.input3) ||
            (PlayerInput.input0 && PlayerInput.input1) ||
            (PlayerInput.input0 && PlayerInput.input2) ||
            (PlayerInput.input0 && PlayerInput.input3) ||
            (PlayerInput.input1 && PlayerInput.input2) ||
            (PlayerInput.input1 && PlayerInput.input3) ||
            (PlayerInput.input2 && PlayerInput.input3))
        {
            Debug.Log("Please hit properly");
            return;
        }
        #endregion 玩家的下一個指令
        #region 玩家血量
        if (player.playerCurrentHealth <= 0f)
        {
            //TODO: 這裡要改
            UIManager.instance.leaveStage.GetComponentInChildren<TextMeshProUGUI>().text = "You are dead";
            ChangeState(GameState.LeavingLevel);
            return;
        }
        #endregion 玩家血量
        #region 玩家耐心
        if (UIManager.instance.playerPatience.fillAmount <= 0f)
        {
            UIManager.instance.playerPatience.fillAmount = 0f;
            UIManager.instance.leaveStage.GetComponentInChildren<TextMeshProUGUI>().text = "You are out of patience";
            ChangeState(GameState.LeavingLevel);
            return;
        }
        else UIManager.instance.playerPatience.fillAmount -= (totalTime / levelDifficulty);
        #endregion 玩家耐心
        #region 所有Prompt被玩家消滅
        if (fourButton == null)
        {
            #region 時間計算
            if (correctInput != 0)
            {
                playerAverageReactTimeList.Add(averageTime / correctInput);
                UIManager.instance.averageTime.color = Color.white;
                UIManager.instance.averageTime.text = (averageTime / correctInput) > 100f ? (averageTime / correctInput).ToString("000") : (averageTime / correctInput).ToString("F2");
            }
            else
            {
                UIManager.instance.averageTime.color = Color.red;
                UIManager.instance.averageTime.text = "BAD";
            }
            averageTime = 0f;
            correctInput = 0;
            #endregion 時間計算
            #region 如果此場景是沒有Boss的
            if (!currentSceneBossSceneOrNot)
            {
                #region 層數到零
                if (stageClear == 0)
                {
                    UIManager.instance.leaveStage.GetComponentInChildren<TextMeshProUGUI>().text = "This area is empty now";
                    ChangeState(GameState.LeavingLevel);
                    return;
                }
                #endregion 層數到零
                #region 還沒到零
                else
                {
                    stageClear--;
                    UIManager.instance.stageClear.text = stageClear.ToString();
                    ChangeState(GameState.SpawnPrompt);
                    return;
                }
                #endregion 還沒到零
            }
            #endregion 如果此場景沒是有Boss的
            #region 如果此場景是有Boss的
            else
            {
                #region 可觸發連打
                if (Random.value < currentSceneBossRushOdds)
                {
                    ChangeState(GameState.BossRush);
                    return;
                }
                #endregion 可觸發連打
                ChangeState(GameState.BossDealtDamage);
                return;
            }
            #endregion 如果此場景是有Boss的                       
        }
        #endregion 所有Prompt被玩家消滅
        #region 玩家打擊Prompt
        switch (currentScenePromptTypeNumber)
        {
            case 4:
                if (PlayerInput.input0 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[0]) StartCoroutine(RightInput());
                else if (PlayerInput.input1 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[1]) StartCoroutine(RightInput());
                else if (PlayerInput.input2 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[2]) StartCoroutine(RightInput());
                else if (PlayerInput.input3 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[3]) StartCoroutine(RightInput());
                #region Wrong Input
                if ((PlayerInput.input0 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[0]) ||
                    (PlayerInput.input1 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[1]) ||
                    (PlayerInput.input2 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[2]) ||
                    (PlayerInput.input3 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[3]))
                {
                    StartCoroutine(WrongInput());
                }
                #endregion Wrong Input    
                break;
            case 3:
                if (PlayerInput.input0 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[0]) StartCoroutine(RightInput());
                else if (PlayerInput.input1 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[1]) StartCoroutine(RightInput());
                else if (PlayerInput.input2 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[2]) StartCoroutine(RightInput());
                #region Wrong Input
                if ((PlayerInput.input0 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[0]) ||
                    (PlayerInput.input1 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[1]) ||
                    (PlayerInput.input2 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[2]))
                {
                    StartCoroutine(WrongInput());
                }
                #endregion Wrong Input    
                break;
            case 2:
                if (PlayerInput.input0 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[0]) StartCoroutine(RightInput());
                else if (PlayerInput.input1 && fourButton != null && fourButton.button == InputsManager.instance.defaultKeyBinding[1]) StartCoroutine(RightInput());
                #region Wrong Input
                if ((PlayerInput.input0 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[0]) ||
                    (PlayerInput.input1 && fourButton != null && fourButton.button != InputsManager.instance.defaultKeyBinding[1]))
                {
                    StartCoroutine(WrongInput());
                }
                #endregion Wrong Input
                break;
            case 1:
                #region HyperTapMode
                if (PlayerInput.input0 && fourButton.button == InputsManager.instance.defaultKeyBinding[0])
                {
                    StartCoroutine(RightInput());
                }
                #endregion HyperTapMode
                break;
            default:
                break;
        }
        #endregion 玩家打擊Prompt
    }
    protected IEnumerator BossRush()
    {
        if (BossRushOnAndOff)
        {
            bossRushTime = currentSceneBossRushTime;
            playerHitOnBoss = 0;
            yield return new WaitForSeconds(1f);
            BossRushOnAndOff = false;
        }
        else if (PlayerInput.input0 || PlayerInput.input1 || PlayerInput.input2 || PlayerInput.input3)
        {
            SoundManager.instance.PlaySound(correctInputSound);
            playerHitOnBoss++;
            if (player.isPlayerMainEquipmentEquipped && PlayerInput.mainEquipmentInput)
            {
                bossRushDamageResult.Add(player.playerMainEquipmentDamageDelta);
            }
            else
            {
                bossRushDamageResult.Add(player.playerDamageDelta);
            }
        }
        UIManager.instance.inputTime.text = bossRushTime.ToString("F1");
        bossRushTime -= Time.deltaTime;
        if (bossRushTime <= 0f)
        {
            if (playerHitOnBoss != 0) playerMaxHitOnBossList.Add(playerHitOnBoss);
            bossRushDamageResult.Sort();
            var action = Instantiate(UIManager.instance.actionText, UIManager.instance.actionHolder.transform).GetComponent<TextMeshProUGUI>();
            float damage = (bossRushDamageResult.Count == 0 ? 0f : bossRushDamageResult.Last()) * playerHitOnBoss;
            action.text = $"Boss Taken Damage {damage:F2} in BOSSRUSH";
            currentSceneBossHealth -= damage;
            bossRushDamageResult.Clear();
            BossRushOnAndOff = true;
            ChangeState(GameState.BossDealtDamage);
        }
    }
    protected IEnumerator BossDealtDamage()
    {
        yield return new WaitForSeconds(1f);
        var action = Instantiate(UIManager.instance.actionText, UIManager.instance.actionHolder.transform).GetComponent<TextMeshProUGUI>();
        action.text = $"Boss Taken Damage {playerTotalDealtDamage:F2}";
        currentSceneBossHealth -= playerTotalDealtDamage;
        playerTotalDealtDamage = 0f;
        if (currentSceneBossHealth <= 0)
        {
            UIManager.instance.leaveStage.GetComponentInChildren<TextMeshProUGUI>().text = "Boss Has Been Beaten";
            //endGame = true;
            ChangeState(GameState.LeavingLevel);
        }
        else ChangeState(GameState.SpawnPrompt);
    }
    protected string rightPromptAction()
    {
        return "";
    }
    protected string wrongPromptAction()
    {
        return fourButton.promptSO.promptType switch
        {
            PromptType.Cat => $"{fourButton.promptSO.promptName_English} has run away",
            PromptType.Geometry => "wrong geometry",
            PromptType.Mineral => $"{fourButton.promptSO.promptName_English} has break",
            PromptType.Food => $"You crushed the {fourButton.promptSO.promptName_English}",
            PromptType.BossAttack => $"You got hit by {fourButton.promptSO.promptName_English}",
            PromptType.Liquor => $"You totaled the {fourButton.promptSO.promptName_English}",
            PromptType.Wood => $"You Broke the {fourButton.promptSO.promptName_English}",
            _ => "",
        };
    }
    protected override void ParameterSetting(Player player, bool isInitial)
    {
        currentScenePromptTypeNumber = scenePromptTypeNumberPriority(player);
        currentSceneMinPromptOdds = sceneMinPromptOddsPriority(player);
        currentSceneMaxPromptsNumber = sceneMaxPromptsNumberPriority(player);
        currentSceneMinPromptsNumber = sceneMinPromptsNumberPriority(player);
        currentScenePatienceMinus = scenePatienceMinusPriority(player);
        currentSceneTotalStage = sceneTotalStagePriority(player, isInitial);
        currentSceneWrongInputPatincePenalty = sceneWrongInputPatincePenaltyPriority(player);
        currentSceneDestroyAllOrNot = sceneDestroyAllOrNotPriority(player);
        //currentSceneEffect = sceneDefaultParameter.sceneEffect;
        currentScenePromptEveryPopTime = scenePromptEveryPopTimePriority(player);
        currentScenePromptRespawnTime = scenePromptRespawnTimePriority(player);
        currentSceneBossSceneOrNot = sceneBossSceneOrNotPriority();
        currentSceneBossRushOdds = sceneBossRushOddsPriority(player);
        currentSceneBossRushTime = sceneBossRushTimePriority(player);
        currentSceneBossHealth = sceneBossHealthPriority(player, isInitial);
    }
    protected override void PlayerImpactOnInput(Player player)
    {
        inputTime = inputTimePriority(player);
    }
    #region Animation
    private IEnumerator StartPromptResultAnimation()
    {
        yield return new WaitForSeconds(2f);
        foreach (var member in playerGameObject.transform.GetChild(0).transform.GetComponentsInChildren<FourButton>())
        {
            member.AddComponent<Rigidbody2D>();
            member.AddComponent<CircleCollider2D>();
            member.GetComponent<Rigidbody2D>().gravityScale = 3f;
            member.GetComponent<CircleCollider2D>().radius = 0.3f;
            member.transform.position = new Vector3(Random.Range(-5f, 5f), playerGameObject.transform.GetChild(0).transform.position.y, ConstantZero);
            member.gameObject.layer = 6;
            SoundManager.instance.PlaySound(currentSceneResultSound, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator StartBlackoutAnimation(Image image)
    {
        image.color = new Color(0.0f, 0.0f, 0.0f, Mathf.MoveTowards(image.color.a, 0.8f, Time.deltaTime));
        yield return null;
    }
    private IEnumerator StartResultAnimation()
    {
        foreach (var member in UIManager.instance.resultPanelForAnimation)
        {
            if (!member.activeSelf) continue;
            animationFinished = false;
            float time = 0f;
            while (!skipAnimation && time < 1.5f)
            {
                member.GetComponent<Animator>().SetFloat("MotionTime", time);
                time += Time.deltaTime;
                yield return null;
            }
            member.GetComponent<Animator>().SetFloat("MotionTime", 1.5f);
            skipAnimation = false;
            animationFinished = true;
        }
        UIManager.instance.endSceneText.SetActive(true);
        allAnimationDone = true;
    }
    #endregion Animation
    protected void Update()
    {
        for (int i = 0; i < UIManager.instance.inputIndicator.transform.childCount; i++)
        {
            if (i < currentScenePromptTypeNumber) UIManager.instance.inputIndicator.transform.GetChild(i).gameObject.SetActive(true);
            else UIManager.instance.inputIndicator.transform.GetChild(i).gameObject.SetActive(false);
        }
        ParameterSetting(player, false);
        if (state == GameState.LeavingLevel)
        {
            if (blackout) StartCoroutine(StartBlackoutAnimation(UIManager.instance.blackout));
            if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
            {
                UIManager.instance.resultPanelForAnimation[0].SetActive(true);
                blackout = true;
                if (allAnimationDone)
                {
                    UIManager.instance.leaveStage.SetActive(false);
                    UIManager.instance.endSceneText.SetActive(false);
                    if (endGame) GameManager.instance.LoadSceneByName("EndScene");
                    else GameManager.instance.LoadSceneByID(1);
                    return;
                }
                if (animationFinished && !skipAnimation)
                {
                    StartCoroutine(StartPromptResultAnimation());
                    StartCoroutine(StartResultAnimation());
                }
                else if (!animationFinished && !skipAnimation)
                {
                    skipAnimation = true;
                }
            }
            if (Utilities.hit.collider == null) return;
            if (Utilities.hit.collider.GetComponent<FourButton>() == null) return;
            else
            {
                GameObject grab = Utilities.hit.collider.gameObject;
                if (Input.GetMouseButton(0))
                {
                    grab.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    grab.transform.position = Utilities.mousePosition;
                }
            }
        }
        if (state == GameState.BossRush) StartCoroutine(BossRush());
        if (state == GameState.PlayerInput)
        {
            if (PlayerInput.leaveGameInput) ChangeState(GameState.LeavingLevel);
            MatchPrompts();
        }
    }
}