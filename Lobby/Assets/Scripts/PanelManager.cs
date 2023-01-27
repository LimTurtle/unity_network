using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject modePanel;
    public GameObject optionPanel;
    public GameObject matchButton;
    public GameObject matchCancleButton;
    public GameObject soloButton;
    public GameObject duoButton;
    public GameObject squadButton;

    public Text matchText;
    public Text soloText;
    public Text duoText;
    public Text squadText;

    private bool isMatching = false;

    public bool[] seletedModes = { true, false, false };

    public void MatchSelect()
    {
        if (!isMatching)
        {
            isMatching = true;
            matchCancleButton.SetActive(true);
            StartCoroutine(ChangeMatchText());
            StartCoroutine(NetworkManager.networkManager.UnityWebRequestPost(seletedModes));
        }
    }

    IEnumerator ChangeMatchText()
    {
        while(true)
        {
            matchText.text = "��ġ ��.";
            yield return new WaitForSeconds(1f);
            matchText.text = "��ġ ��..";
            yield return new WaitForSeconds(1f);
            matchText.text = "��ġ ��...";
            yield return new WaitForSeconds(1f);
        }
    }


    public void MatchCancle()
    {
        isMatching = false;
        matchText.text = "��ġ����ŷ";
        matchCancleButton.SetActive(false);
        StopAllCoroutines();
    }

    public void ModeSelect()
    {
        menuPanel.SetActive(false);
        modePanel.SetActive(true);
    }

    public void OptionSelect()
    {
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void BackFromMode()
    {
        menuPanel.SetActive(true);
        modePanel.SetActive(false);
    }

    public void BackFromOption()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void SoloOnClick()
    {
        if(!seletedModes[0])
        {
            soloText.text = "���õ�";
            seletedModes[0] = true;
        }
        else
        {
            soloText.text = "���ܵ�";
            seletedModes[0] = false;
        }
    }

    public void DuoOnClick()
    {
        if (!seletedModes[1])
        {
            duoText.text = "���õ�";
            seletedModes[1] = true;
        }
        else
        {
            duoText.text = "���ܵ�";
            seletedModes[1] = false;
        }
    }

    public void SquadOnClick()
    {
        if (!seletedModes[2])
        {
            squadText.text = "���õ�";
            seletedModes[2] = true;
        }
        else
        {
            squadText.text = "���ܵ�";
            seletedModes[2] = false;
        }
    }
}
