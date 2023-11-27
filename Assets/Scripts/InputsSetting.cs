using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InputsSetting : MonoBehaviour
{
    [SerializeField] SettingState settingState;
    public TextMeshProUGUI enterInput;
    public Button gameContinue;
    public Button settingConfirmed;
    public Button settingCancel;
    [Space(10)]
    [Header("(注意:前四個一定要是button_Input)")]
    public Button[] buttonInputs;
    private int currentInput;
    private void Start()
    {        
        ChangeState(SettingState.None);
        if (buttonInputs != null)
        {
            for (int i = 0; i < buttonInputs.Length; i++)
            {
                buttonInputs[i].GetComponentInChildren<TextMeshProUGUI>().text = InputsManager.instance.defaultKeyBinding[i];
            }
        }
    }
    private void ChangeState(SettingState state)
    {
        settingState = state;
        switch (state)
        {
            case SettingState.SettingInput:
                enterInput.text = "Please Enter Your Input";
                enterInput.color = Color.white;
                enterInput.gameObject.SetActive(true);
                FocusInput(false);
                ChangeState(SettingState.EnterInput);
                break;
            case SettingState.DoneInput:
                FocusInput(true);
                break;
            default:
                break;
        }
    }
    public void WaitingForInput(int index)
    {
        currentInput = index;
        buttonInputs[index].GetComponentInChildren<TextMeshProUGUI>().text = "";
        ChangeState(SettingState.SettingInput);
    }
    private void FocusInput(bool active)
    {
        for(int i = 0; i < buttonInputs.Length;i++)
        {
            if (i == currentInput) continue;
            buttonInputs[i].interactable = active;
        }
    }
    public KeyCode GetPlayerInput()
    {
        string input = Input.inputString.ToLower();
        if (!InputsManager.instance.validInput.Contains(input)) return KeyCode.None;
        if (input.Length != 1) return KeyCode.None;
        if (!string.IsNullOrEmpty(input))
        {
            KeyCode keycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), input.ToUpper());
            return keycode;
        }
        return KeyCode.None;
    }
    public void PlayerEnterInput()
    {
        settingCancel.interactable = InputsManager.instance.invalidInputs();
        settingConfirmed.interactable = InputsManager.instance.invalidInputs();
        #region 判斷是否為有效輸入,然後將輸入轉成大寫後寫入buttonInputs[]成員的text
        if (InputsManager.instance.validInput.Contains(Input.inputString))
        {
            buttonInputs[currentInput].GetComponentInChildren<TextMeshProUGUI>().text = Input.inputString.ToUpper();
        }
        #endregion 判斷是否為有效輸入,然後將輸入轉成大寫後寫入buttonInputs[]成員的text
        #region 把玩家的輸入傳給Inputs
        EventHandler.CallSettingInput(currentInput, GetPlayerInput(), Input.inputString.ToUpper());
        #endregion 把玩家的輸入傳給Inputs
        #region 當輸入成功
        #region 輸入為一個值時
        if (buttonInputs[currentInput].GetComponentInChildren<TextMeshProUGUI>().text.Length == 1)
        {
            #region 如果有重複輸入的話
            for (int i = 0; i < buttonInputs.Length; i++)
            {
                if (i == currentInput) continue;
                if (InputsManager.instance.defaultKeyBinding[i] == buttonInputs[currentInput].GetComponentInChildren<TextMeshProUGUI>().text)
                {
                    settingCancel.interactable = false;
                    settingConfirmed.interactable = false;
                    enterInput.text = "Input Already Been Used";
                    enterInput.color = Color.green;
                    return;
                }
            }
            #endregion 如果有重複輸入的話
            enterInput.gameObject.SetActive(false);
            settingCancel.interactable = InputsManager.instance.invalidInputs();
            settingConfirmed.interactable = InputsManager.instance.invalidInputs();
            ChangeState(SettingState.DoneInput);
        }
        #endregion 輸入為一個值時
        #region 輸入為兩個值時
        else if (buttonInputs[currentInput].GetComponentInChildren<TextMeshProUGUI>().text.Length > 1)
        {
            enterInput.text = "Please Enter Properly";
            enterInput.color = Color.green;
            buttonInputs[currentInput].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        #endregion 輸入為兩個值時
        #endregion 當輸入成功
    }
    private void Update()
    {
        gameContinue.interactable = File.Exists(Application.persistentDataPath + "/DATA/data.sav");
        if (settingState == SettingState.EnterInput)
        {
            PlayerEnterInput();
        }
    }
    public void SaveInput()
    {
        PlayerPrefs.SetString("Input0", InputsManager.instance.defaultKeyBinding[0]);
        PlayerPrefs.SetString("Input1", InputsManager.instance.defaultKeyBinding[1]);
        PlayerPrefs.SetString("Input2", InputsManager.instance.defaultKeyBinding[2]);
        PlayerPrefs.SetString("Input3", InputsManager.instance.defaultKeyBinding[3]);
        PlayerPrefs.SetString("Inventory", InputsManager.instance.defaultKeyBinding[4]);
        PlayerPrefs.SetString("SecondaryEquipmentInput0", InputsManager.instance.defaultKeyBinding[5]);
        PlayerPrefs.SetString("SecondaryEquipmentInput1", InputsManager.instance.defaultKeyBinding[6]);
        PlayerPrefs.SetString("SecondaryEquipmentInput2", InputsManager.instance.defaultKeyBinding[7]);
        PlayerPrefs.SetString("SecondaryEquipmentInput3", InputsManager.instance.defaultKeyBinding[8]);
        PlayerPrefs.Save();
        for (int i = 0; i < buttonInputs.Length; i++)
        {
            InputsManager.instance.keyCodeBinding[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputsManager.instance.defaultKeyBinding[i]);
        }
    }
    public void CancelInput()
    {
        InputsManager.instance.defaultKeyBinding[0] = PlayerPrefs.GetString("Input0");
        InputsManager.instance.defaultKeyBinding[1] = PlayerPrefs.GetString("Input1");
        InputsManager.instance.defaultKeyBinding[2] = PlayerPrefs.GetString("Input2");
        InputsManager.instance.defaultKeyBinding[3] = PlayerPrefs.GetString("Input3");
        InputsManager.instance.defaultKeyBinding[4] = PlayerPrefs.GetString("Inventory");
        InputsManager.instance.defaultKeyBinding[5] = PlayerPrefs.GetString("SecondaryEquipmentInput0");
        InputsManager.instance.defaultKeyBinding[6] = PlayerPrefs.GetString("SecondaryEquipmentInput1");
        InputsManager.instance.defaultKeyBinding[7] = PlayerPrefs.GetString("SecondaryEquipmentInput2");
        InputsManager.instance.defaultKeyBinding[8] = PlayerPrefs.GetString("SecondaryEquipmentInput3");
        for (int i = 0; i < buttonInputs.Length; i++)
        {
            buttonInputs[i].GetComponentInChildren<TextMeshProUGUI>().text = InputsManager.instance.defaultKeyBinding[i];
            InputsManager.instance.keyCodeBinding[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputsManager.instance.defaultKeyBinding[i]);
        }
    }
}