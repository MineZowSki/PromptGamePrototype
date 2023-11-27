using System.Collections.Generic;
using UnityEngine;
public class InputsManager : InputValid<InputsManager> 
{
    [HideInInspector] public List<string> defaultKeyBinding;
    [HideInInspector] public List<KeyCode> keyCodeBinding;
    private void OnEnable()
    {
        EventHandler.settingInput += OnSettingInput;
    }
    private void OnDisable()
    {
        EventHandler.settingInput -= OnSettingInput;
    }
    private void Start()
    {
        defaultKeyBinding = new List<string>()
        {
            "W","P","E","O","Q","Z","X","C","V"
        };
        keyCodeBinding = new List<KeyCode>()
        {
            KeyCode.W,
            KeyCode.P,
            KeyCode.E,
            KeyCode.O,
            KeyCode.Q,
            KeyCode.Z,
            KeyCode.X,
            KeyCode.C,
            KeyCode.V,
        };
        #region 如果是新遊戲的話(沒有SetString過)
        if (PlayerPrefs.GetString("Input0") == string.Empty || PlayerPrefs.GetString("Inventory") == string.Empty || PlayerPrefs.GetString("SecondaryEquipmentInput0") == string.Empty)
        {
            PlayerPrefs.SetString("Input0", defaultKeyBinding[0]);
            PlayerPrefs.SetString("Input1", defaultKeyBinding[1]);
            PlayerPrefs.SetString("Input2", defaultKeyBinding[2]);
            PlayerPrefs.SetString("Input3", defaultKeyBinding[3]);
            PlayerPrefs.SetString("Inventory", defaultKeyBinding[4]);
            PlayerPrefs.SetString("SecondaryEquipmentInput0", defaultKeyBinding[5]);
            PlayerPrefs.SetString("SecondaryEquipmentInput1", defaultKeyBinding[6]);
            PlayerPrefs.SetString("SecondaryEquipmentInput2", defaultKeyBinding[7]);
            PlayerPrefs.SetString("SecondaryEquipmentInput3", defaultKeyBinding[8]);
            PlayerPrefs.Save();
            return;
        }
        #endregion 如果是新遊戲的話(沒有SetString過)
        defaultKeyBinding[0] = PlayerPrefs.GetString("Input0");
        defaultKeyBinding[1] = PlayerPrefs.GetString("Input1");
        defaultKeyBinding[2] = PlayerPrefs.GetString("Input2");
        defaultKeyBinding[3] = PlayerPrefs.GetString("Input3");
        defaultKeyBinding[4] = PlayerPrefs.GetString("Inventory");
        defaultKeyBinding[5] = PlayerPrefs.GetString("SecondaryEquipmentInput0");
        defaultKeyBinding[6] = PlayerPrefs.GetString("SecondaryEquipmentInput1");
        defaultKeyBinding[7] = PlayerPrefs.GetString("SecondaryEquipmentInput2");
        defaultKeyBinding[8] = PlayerPrefs.GetString("SecondaryEquipmentInput3");
        for (int i = 0; i < defaultKeyBinding.Count; i++)
        {
            keyCodeBinding[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), defaultKeyBinding[i]);
        }
    }
    public void OnSettingInput(int index, KeyCode keycode, string input)
    {
        defaultKeyBinding[index] = input;
        keyCodeBinding[index] = keycode;
    }
    public bool invalidInputs()
    {
        foreach (KeyCode keyCode in keyCodeBinding)
        {
            if (keyCode == KeyCode.None) return false;
        }
        foreach (string str in defaultKeyBinding)
        {
            if (str == string.Empty) return false;
        }
        return true;
    }
}