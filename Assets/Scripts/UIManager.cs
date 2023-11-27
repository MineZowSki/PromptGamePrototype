using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [Header("�b����")]
    [Tooltip("(�b����)���a���U�@�ӿ�J")] public TextMeshProUGUI nextInput;
    [Tooltip("(�b����)���a��J�ɶ�")] public TextMeshProUGUI inputTime;
    [Tooltip("(�b����)���a������J�ɶ�")] public TextMeshProUGUI averageTime;
    [Tooltip("(�b����)�ѾlStage")] public TextMeshProUGUI stageClear;
    [Tooltip("(�b����)���a��q")] public Image playerHealth;
    [Tooltip("(�b����)���a�@��")] public Image playerPatience;
    [Tooltip("(�b����)�ͦ�����r")] public GameObject actionText;
    [Tooltip("(�b����)�ͦ�����r�|���b�o��")] public GameObject actionHolder;
    [Tooltip("(�b����)�Ψ���ܪ��a�]�w��Input")] public GameObject inputIndicator;
    [Tooltip("(�b����)�Ψ����Prompt�O�_�b�ͦ���")] public GameObject searchingInput;
    [Tooltip("(�b����)�I����")] public SpriteRenderer backgroundSprite;
    [Tooltip("(�b����)Boss��")] public SpriteRenderer bossSprite;
    [Space(10)]
    [Header("������")]
    [Tooltip("(������)Prompt�����V���᪺���O")] public GameObject leaveStage;
    [Tooltip("(������)�������")] public Image blackout;
    [Space(10)]
    [Header("�����")]
    [Tooltip("(�����)�̨ο�J�ɶ�")] public TextMeshProUGUI bestInputTime;
    [Tooltip("(�����)�̨Υ�����J�ɶ�")] public TextMeshProUGUI bestAverageTime;
    [Tooltip("(�����)�̫���������r���")] public GameObject endSceneText;
    [Tooltip("(�����)�̰�BossRush�s��")] public TextMeshProUGUI bestBossRushCombo;
    [Tooltip("(�����)���a�Ѿl�@��")] public TextMeshProUGUI playerPatienceLeft;
    [Space(10)]
    [Header("��x��")]
    [Tooltip("(��x��)���⭱�O���ʵe")] public GameObject[] resultPanelForAnimation;
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