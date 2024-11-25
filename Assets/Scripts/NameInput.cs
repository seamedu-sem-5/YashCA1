using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    public void EnterPlayerName(TMP_InputField input)
    {
        GameManager.Instance.SetPlayerName(input.text);
    }

}
