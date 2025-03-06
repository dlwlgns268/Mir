using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Conversation : MonoBehaviour
{
    public Dialogue[] Dialogues;
    public int currentIndex;
    public Dialogue currentDialogue;
    public TextMeshProUGUI dia;
    public TextMeshProUGUI mainName;
    public TextMeshProUGUI oppoName;
    public Image MainPortrait;
    public Image OppoPortrait;
    public GameObject dialogueModal;
    public GameObject mainModal;
    public GameObject oppoModal;
    void Start()
    {
        currentIndex = 0;
        
        DoTalk();
    }

    public void DoTalk()
    {
        dialogueModal.SetActive(true);
        mainModal.SetActive(false);
        oppoModal.SetActive(false);
        StartCoroutine(TalkFlow());
    }
    void Update()
    {
        
    }

    private IEnumerator TalkFlow()
    {
        while (currentIndex <= Dialogues.Length)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("출력");
                currentDialogue = Dialogues[currentIndex];
                if (currentDialogue.isMainCharacter)
                {
                    mainModal.SetActive(true);
                    oppoModal.SetActive(false);
                    mainName.text = currentDialogue.playerName;
                    MainPortrait.sprite = currentDialogue.MainCharacterPortrait;
                }
                else
                {
                    mainModal.SetActive(false);
                    oppoModal.SetActive(true);
                    oppoName.text = currentDialogue.opponentName;
                    OppoPortrait.sprite = currentDialogue.OpponentCharacterPortrait;
                }
                dia.text = currentDialogue.dia;
                currentIndex++;
            }
            yield return null;
        }
    }
}
