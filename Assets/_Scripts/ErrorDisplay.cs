using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorDisplay : MonoBehaviour
{
    private static ErrorDisplay s_Instance;
    public TextMeshProUGUI m_ErrorTextField;
    // Start is called before the first frame update
    void Start()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            DisplayErrorInternal("Not enough Cow dung");
        }
    }

    public static void DisplayError(string message) 
    {
        s_Instance.DisplayErrorInternal(message);
    }

    private void DisplayErrorInternal(string message) 
    {
        m_ErrorTextField.text = message;
        StopAllCoroutines();
        StartCoroutine(Co_ErrorFade());
    }

    IEnumerator Co_ErrorFade() 
    {
        Color temp = m_ErrorTextField.color;
        temp.a = 1;
        m_ErrorTextField.color = temp;
        while (temp.a > 0) 
        {
            temp.a -= Time.deltaTime/2f;
            m_ErrorTextField.color = temp;
            yield return new WaitForSeconds(0);
        }
    }
}
