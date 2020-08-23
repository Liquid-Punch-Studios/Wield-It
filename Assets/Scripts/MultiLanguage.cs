using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiLanguage : MonoBehaviour
{
	private bool oldChanged;
	public static bool changed;
	private TMP_Text tmp;

	[TextArea]
	public string[] texts;

    private void OnEnable()
    {
		//SettingsManager.Instance.Settings.LanguageChanged += Settings_LanguageChanged;
	}

    private void Start()
	{
		tmp = GetComponent<TMP_Text>();
		tmp.text = texts[(int)SettingsManager.Instance.Settings.Language];
		oldChanged = changed;
	}

    private void Update()
    {
        if (changed != oldChanged)
        {
			tmp.text = texts[(int)SettingsManager.Instance.Settings.Language];
			oldChanged = changed;
		}
    }
	/*
	private void Settings_LanguageChanged(object sender, System.EventArgs e)
	{
		tmp.text = texts[(int)SettingsManager.Instance.Settings.Language];
	}*/
}
