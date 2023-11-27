using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StartManager : Singleton<StartManager>
{
    [SerializeField] private List<InitialPlayer_SO> allInitialPlayerList = new List<InitialPlayer_SO>();
    [SerializeField] private Player player;
    [Space(10), Header("角色選單相關")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private GameObject characterContentHolder;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterScrollPanel;
    [SerializeField] private Button startGameButton;
    [Space(10), Header("主畫面按鈕")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingButton;
    [Space(10), Header("離開遊戲相關")]
    public GameObject leaveGamePanel;
    private int currentCharacterIndex;
    private void Start()
    {
        characterNameText.text = string.Empty;
        startGameButton.interactable = false;
        characterScrollPanel.SetActive(false);
        leaveGamePanel.SetActive(false);
        EventHandler.CallPlayerCanOpenInventoryOrNot(false);
        InventoryManager.instance.inventoryOnOff.SetActive(false);
        leaveGamePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(GameManager.instance.QuitGame);
        for(int i = 0; i < allInitialPlayerList.Count; i++)
        {
            int index = i;
            var initial = Instantiate(characterPrefab, characterContentHolder.transform);
            initial.GetComponent<InitialCharacter>().initialInfo = allInitialPlayerList[i];
            initial.GetComponent<Image>().sprite = initial.GetComponent<InitialCharacter>().initialInfo.initialSprite;
            initial.GetComponent<Button>().onClick.AddListener(() => CharacterChose(index));
        }
    }
    public void ChooseCharacter()
    {
        characterScrollPanel.SetActive(true);
        startButton.enabled = false;
        continueButton.enabled = false;
        settingButton.enabled = false;
    }
    public void StartNewGame()
    {
        player.playerInitial.initialInfo = allInitialPlayerList[currentCharacterIndex].initialInfo;
        player.playerInitial.initialCharacterInfo = allInitialPlayerList[currentCharacterIndex].initialCharacterInfo;
        EventHandler.CallStartNewGame();
        GameManager.instance.LoadSceneByID(1);
    }
    public virtual void GameContinue()
    {
        SaveLoadManager.instance.LoadGame();
    }
    public void CharacterChose(int index)
    {
        startGameButton.interactable = true;
        currentCharacterIndex = index;
        characterNameText.text = allInitialPlayerList[index].initialName;
    }
    private void Update()
    {
        if (PlayerInput.leaveGameInput)
        {
            if (characterScrollPanel.activeSelf)
            {
                characterScrollPanel.SetActive(false);
                startGameButton.interactable = false;
                startButton.enabled = true;
                continueButton.enabled = true;
                settingButton.enabled = true;
                characterNameText.text = string.Empty;
                return;
            }
            leaveGamePanel.SetActive(true);
        }
    }
}