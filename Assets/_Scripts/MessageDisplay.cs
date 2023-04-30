using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Responsible for displaying Messages like NO SEEDS ARE PRESENT
public class MessageDisplay : Singleton<MessageDisplay>
{
    public TextMeshProUGUI m_MessageTextField;

    public static void DisplayMessage(string message) 
    {
        s_Instance.DisplayMessageInternal(message);
    }

    private void DisplayMessageInternal(string message) 
    {
        m_MessageTextField.text = message;
        StopAllCoroutines();
        StartCoroutine(Co_MessageFade());
    }

    IEnumerator Co_MessageFade() 
    {
        Color temp = m_MessageTextField.color;
        temp.a = 1;
        m_MessageTextField.color = temp;
        while (temp.a > 0) 
        {
            temp.a -= Time.deltaTime/2f;
            m_MessageTextField.color = temp;
            yield return new WaitForSeconds(0);
        }
    }
}
