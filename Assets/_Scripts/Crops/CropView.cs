using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CropView
{
    public Text nameText;
    public Text descriptionText;
    public Button m_Button;
    public Image image;
    public Text quantityText;
    // Add any additional UI elements here

    public void UpdateView(Resource crop)
    {
        nameText.text = crop.name;
        // Update any additional UI elements here
    }
}
