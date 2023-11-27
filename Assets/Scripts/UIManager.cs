using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [Header("在場用")]
    [Tooltip("(在場用)玩家的下一個輸入")] public TextMeshProUGUI nextInput;
    [Tooltip("(在場用)玩家輸入時間")] public TextMeshProUGUI inputTime;
    [Tooltip("(在場用)玩家平均輸入時間")] public TextMeshProUGUI averageTime;
    [Tooltip("(在場用)剩餘Stage")] public TextMeshProUGUI stageClear;
    [Tooltip("(在場用)玩家血量")] public Image playerHealth;
    [Tooltip("(在場用)玩家耐心")] public Image playerPatience;
    [Tooltip("(在場用)生成的文字")] public GameObject actionText;
    [Tooltip("(在場用)生成的文字會掛在這裡")] public GameObject actionHolder;
    [Tooltip("(在場用)用來顯示玩家設定的Input")] public GameObject inputIndicator;
    [Tooltip("(在場用)用來顯示Prompt是否在生成中")] public GameObject searchingInput;
    [Tooltip("(在場用)背景圖")] public SpriteRenderer backgroundSprite;
    [Tooltip("(在場用)Boss圖")] public SpriteRenderer bossSprite;
    [Space(10)]
    [Header("離場用")]
    [Tooltip("(離場用)Prompt全部敲完後的面板")] public GameObject leaveStage;
    [Tooltip("(離場用)場景轉黑")] public Image blackout;
    [Space(10)]
    [Header("結算用")]
    [Tooltip("(結算用)最佳輸入時間")] public TextMeshProUGUI bestInputTime;
    [Tooltip("(結算用)最佳平均輸入時間")] public TextMeshProUGUI bestAverageTime;
    [Tooltip("(結算用)最後離場的文字顯示")] public GameObject endSceneText;
    [Tooltip("(結算用)最高BossRush連擊")] public TextMeshProUGUI bestBossRushCombo;
    [Tooltip("(結算用)玩家剩餘耐心")] public TextMeshProUGUI playerPatienceLeft;
    [Space(10)]
    [Header("後台用")]
    [Tooltip("(後台用)結算面板的動畫")] public GameObject[] resultPanelForAnimation;
    private void Start()
    {
        transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        transform.GetComponent<Canvas>().worldCamera = Camera.main;
        transform.GetComponent<Canvas>().sortingOrder = 3;
        blackout.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        PlayerInputIndicate();
        //backgroundSprite.enabled = true;
        //bossSprite.enabled = true;
    }
    private void Update()
    {
        if (playerHealth.fillAmount <= 0.2f) playerHealth.color = new Color(1.0f, 0.137f, 0.0f, 1.0f);
        else if (playerHealth.fillAmount <= 0.5f) playerHealth.color = new Color(1.0f, 0.784f, 0.0f, 1.0f);
        else playerHealth.color = new Color(0.392f, 1f, 0.0f, 1.0f);
        playerPatienceLeft.text = $"{(playerPatience.fillAmount * 100).ToString("0")} %";
    }
    private void PlayerInputIndicate()
    {
        TextMeshProUGUI[] textMeshProUGUIs = inputIndicator.GetComponentsInChildren<TextMeshProUGUI>();
        for(int i = 0; i < textMeshProUGUIs.Length; i++)
        {
            textMeshProUGUIs[i].text = InputsManager.instance.defaultKeyBinding[i];
        }
    }
    public void RankingTimer(float time, TextMeshProUGUI text)
    {
        if (time < 0.1f) text.color = Color.red;       
        else if (time < 0.15f) text.color = Color.magenta;       
        else if (time < 0.2f) text.color = Color.yellow;       
        else if (time < 0.5f) text.color = Color.cyan;        
        else text.color = Color.white;        
    }
}