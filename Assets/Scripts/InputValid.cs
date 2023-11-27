using UnityEngine;
public class InputValid<T> : PersistentSingleton<T> where T : InputValid<T>
{
    [HideInInspector]public string[] validInput;
    protected override void Awake()
    {
        base.Awake();
        validInput = new string[52]
        {
            "a","b",
            "c","d",
            "e","f",
            "g","h",
            "i","j",
            "k","l",
            "m","n",
            "o","p",
            "q","r",
            "s","t",
            "u","v",
            "w","x",
            "y","z",
            "A","B",
            "C","D",
            "E","F",
            "G","H",
            "I","J",
            "K","L",
            "M","N",
            "O","P",
            "Q","R",
            "S","T",
            "U","V",
            "W","X",
            "Y","Z"
        };
    }
}