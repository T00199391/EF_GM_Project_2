using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private GameManager gm;
    private Button[] buttons;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        buttons = GetComponentsInChildren<Button>();
    }

    void Update()
    {
        SetButtonActive();
    }

    public void SetButtonActive()
    {
        for (int i = 0; i < gm.GetLevelNumber(); i++)
        {
            buttons[i].interactable = true;
        }
    }
}
