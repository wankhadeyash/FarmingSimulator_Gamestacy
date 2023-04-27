using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Responsible for displaying Messages like NO SEEDS ARE PRESENT
public class MessageDisplay : MonoBehaviour
{
    private static MessageDisplay s_Instance;
    public TextMeshProUGUI m_MessageTextField;
    // Start is called before the first frame update
    void Start()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);
    }

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
