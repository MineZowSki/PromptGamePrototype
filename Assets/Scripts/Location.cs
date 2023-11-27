using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Location : MonoBehaviour
{
    [SerializeField] private Player player;
    [Header("�Ҧ������Ѽ�")]
    public List<SceneParameter_SO> allSceneParameter = new List<SceneParameter_SO>();
    public SceneParameter_SO chosenSceneParameter;
    [Header("�����T������")]
    [Tooltip("��������Prompt�ƶq")]public TextMeshProUGUI thisScenePromptNumber;
    [Tooltip("������Prompts�O�_���H��")] public TextMeshProUGUI sceneIsRandomPromptOrNot;
    [Tooltip("���O�W���a�I�W��")]public TextMeshProUGUI locationNameInPanel;
    [Tooltip("�a�I���ϰ쪺�W��")]public TextMeshProUGUI regionName;
    [Tooltip("In Scene: ����r���")]public TextMeshProUGUI promptTitle;
    [Tooltip("�������|����Prompt")]public GameObject scenePromptInfo;
    [Tooltip("�����Ϥ�")]public Image locationImage;
    [Tooltip("���a���Ť������")] public Image lockImage;
    [Tooltip("�}�l���s")]public Button confirmLocationButton;
    [Header("�ѼƸ�T����")]
    public TextMeshProUGUI rankAmount;
    public TextMeshProUGUI rankSpeed;
    public TextMeshProUGUI rankPatience;
    private string location;
    private int currentLocationIndex = 0;
    private List<SceneParameter_SO> locationSceneParameter;
    private void Start()
    {
        locationSceneParameter = new List<SceneParameter_SO>();
    }
    public void RegionSelect(int indexSwitch)
    {
        currentLocationIndex += indexSwitch;
        if (currentLocationIndex > locationSceneParameter.Count - 1) currentLocationIndex -= locationSceneParameter.Count;
        else if (currentLocationIndex < 0) currentLocationIndex += locationSceneParameter.Count;
        SoundManager.instance.PlaySound(MapManager.instance.locationSound);
        confirmLocationButton.onClick.RemoveAllListeners();
        LocationInfo();
        MapManager.instance.locationInfoPanel.SetActive(true);
    }
    private void LocationInfo()
    {
        #region ������T
        bool playerLevelLacking = player.playerLevel < locationSceneParameter[currentLocationIndex].sceneRequireLevel;
        if (locationSceneParameter[currentLocationIndex].sceneRequireLevel == 0) lockImage.enabled = false;
        else lockImage.enabled = playerLevelLacking;
        confirmLocationButton.gameObject.SetActive(playerUnlockedAScene());
        confirmLocationButton.onClick.AddListener(GameManager.instance.LoadPromptScene);
        #region �q�X�����W��
        regionName.text = locationSceneParameter[currentLocationIndex].sceneName.ToString();
        #endregion �q�X�����W��
        #region �q�X�����Ϥ�
        if (locationSceneParameter[currentLocationIndex].sceneSprite == null) locationImage.enabled = false;
        else locationImage.enabled = true;
        locationImage.sprite = locationSceneParameter[currentLocationIndex].sceneSprite;
        if (locationSceneParameter[currentLocationIndex].sceneBossSceneOrNot)
        {
            if (locationSceneParameter[currentLocationIndex].sceneBossSprite == null) locationImage.enabled = false;
            else locationImage.enabled = true;
            locationImage.sprite = locationSceneParameter[currentLocationIndex].sceneBossSprite;
        }
        #endregion �q�X�����Ϥ�
        #region �p�G�����OMarket
        if (locationSceneParameter[currentLocationIndex].sceneName == SceneName.Market)
        {
            confirmLocationButton.onClick.RemoveAllListeners();
            confirmLocationButton.onClick.AddListener(GameManager.instance.LoadMarketScene);
        }
        #endregion �p�G�����OMarket
        #region �q�X�����O�_���H��
        sceneIsRandomPromptOrNot.enabled = locationSceneParameter[currentLocationIndex].scenePromptsRandomOrNot;
        #endregion �q�X�����O�_���H�� 
        #region �O�_�q�X����Prompt
        scenePromptInfo.SetActive(locationSceneParameter[currentLocationIndex].sceneShowPrompt);
        promptTitle.gameObject.SetActive(locationSceneParameter[currentLocationIndex].sceneShowPrompt);
        for (int i = 0; i < Utilities.GameMaxPrompts; i++)
        {
            if (!locationSceneParameter[currentLocationIndex].sceneShowPrompt) break;
            if (i >= locationSceneParameter[currentLocationIndex].scenePrompts.promptsItem.Count)
            {
                scenePromptInfo.GetComponentsInChildren<Image>()[i].enabled = false;
                continue;
            }
            scenePromptInfo.GetComponentsInChildren<Image>()[i].enabled = true;
            scenePromptInfo.GetComponentsInChildren<Image>()[i].sprite = locationSceneParameter[currentLocationIndex].scenePrompts.promptsItem[i].GetComponent<SpriteRenderer>().sprite;
            scenePromptInfo.GetComponentsInChildren<Image>()[i].color = locationSceneParameter[currentLocationIndex].scenePrompts.promptsItem[i].GetComponent<SpriteRenderer>().color;
        }
        #endregion �O�_�q�X����Prompt
        #region �O�_�q�X�O�XInput
        thisScenePromptNumber.gameObject.SetActive(locationSceneParameter[currentLocationIndex].sceneShowInputAmount);
        if (locationSceneParameter[currentLocationIndex].sceneShowInputAmount) thisScenePromptNumber.text = $"{locationSceneParameter[currentLocationIndex].scenePromptTypeNumber}-Prompt";
        #endregion �O�_�q�X�O�XInput
        #endregion ������T
        #region �ѼƸ�T
        rankAmount.text = rankingAmount(locationSceneParameter[currentLocationIndex].sceneAbundance);
        rankSpeed.text = rankingSpeed(locationSceneParameter[currentLocationIndex].scenePromptEveryPopTime);
        rankPatience.text = rankingPatience(locationSceneParameter[currentLocationIndex].scenePatience);
        #endregion �ѼƸ�T
        chosenSceneParameter.Initialize(allSceneParameter.Find(n => n.sceneName == locationSceneParameter[currentLocationIndex].sceneName));
    }
    private string rankingAmount(float abundance)
    {
        if (abundance <= 5f) return "C";
        else if (abundance <= 20f) return "B";
        else if (abundance <= 50f) return "A";
        else return "S";
    }
    private string rankingSpeed(float speed)
    {
        if (speed <= 0.15f) return "S";
        else if (speed <= 0.3f) return "A";
        else if (speed <= 0.5f) return "B";
        else return "C";
    }
    private string rankingPatience(float patience)
    {
        if (patience <= 1000f) return "C";
        else if (patience <= 5000f) return "B";
        else if (patience <= 10000f) return "A";
        else return "S";
    }
    private bool playerUnlockedAScene()
    {
        if (player.playerLevel >= locationSceneParameter[currentLocationIndex].sceneRequireLevel)
        {
            locationSceneParameter[currentLocationIndex].sceneIsUnlockedOrNot = true;
        }
        else locationSceneParameter[currentLocationIndex].sceneIsUnlockedOrNot = false;
        return locationSceneParameter[currentLocationIndex].sceneIsUnlockedOrNot;
    }
    private void Update()
    {
        if (InventoryManager.instance.inventoryOnOff.activeSelf) return;
        if (Utilities.hit.collider != null)
        {
            location = Utilities.hit.collider.name.ToUpper();
            MapManager.instance.locationName.text = location;
            locationNameInPanel.text = location;
            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.instance.PlaySound(MapManager.instance.locationSound);
                confirmLocationButton.onClick.RemoveAllListeners();
                locationSceneParameter = allSceneParameter.FindAll(n => n.sceneLocationName.ToString() == location);
                MapManager.instance.locationInfoPanel.SetActive(true);
                currentLocationIndex = 0;
                LocationInfo();
            }
        }
    }
}