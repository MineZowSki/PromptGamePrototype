using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public static bool input0 => Input.GetKeyDown(InputsManager.instance.keyCodeBinding[0]);
    public static bool input1 => Input.GetKeyDown(InputsManager.instance.keyCodeBinding[1]);
    public static bool input2 => Input.GetKeyDown(InputsManager.instance.keyCodeBinding[2]);
    public static bool input3 => Input.GetKeyDown(InputsManager.instance.keyCodeBinding[3]);
    public static bool inventoryInput => Input.GetKeyDown(InputsManager.instance.keyCodeBinding[4]);
    public static bool mainEquipmentInput => Input.GetKey(KeyCode.Space);
    public static bool secondaryEquipmentInput0 => Input.GetKey(InputsManager.instance.keyCodeBinding[5]);
    public static bool secondaryEquipmentInput1 => Input.GetKey(InputsManager.instance.keyCodeBinding[6]);
    public static bool secondaryEquipmentInput2 => Input.GetKey(InputsManager.instance.keyCodeBinding[7]);
    public static bool secondaryEquipmentInput3 => Input.GetKey(InputsManager.instance.keyCodeBinding[8]);
    public static bool leaveGameInput => Input.GetKeyDown(KeyCode.Escape);
}