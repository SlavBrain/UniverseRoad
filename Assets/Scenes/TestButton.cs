using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        //_button.onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        Debug.Log("ButtonClicked");
    }
}
