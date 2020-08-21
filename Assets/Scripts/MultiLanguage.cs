using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiLanguage : MonoBehaviour
{
    TextMeshProUGUI textMeshGUI;
    TextMeshPro textMesh;

    bool GUI;
    [TextArea]
    public string[] texts;

    private void Start()
    {
        if(gameObject.TryGetComponent(out TextMeshProUGUI tmg))
        {
            textMeshGUI = tmg;
            textMeshGUI.text = texts[(int)SettingsManager.Instance.Settings.Language];
            GUI = true;
        }
        else
        {
            textMesh = gameObject.GetComponent<TextMeshPro>();
            textMesh.text = texts[(int)SettingsManager.Instance.Settings.Language];
            GUI = false;
        }

        
        
    }

    private void OnEnable()
    {
        SettingsManager.Instance.Settings.LanguageChanged += Settings_LanguageChanged;
    }

    private void Settings_LanguageChanged(object sender, System.EventArgs e)
    {
        if (GUI)
        {
            Debug.Log("Gui");
            textMeshGUI.text = texts[(int)SettingsManager.Instance.Settings.Language];
        }

        else
        {
            Debug.Log("Mesh");
            textMesh.text = texts[(int)SettingsManager.Instance.Settings.Language];
        }
            
    }
}
