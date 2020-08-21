using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiLanguage : MonoBehaviour
{
	private TMP_Text tmp;

	[TextArea]
	public string[] texts;

	private void Start()
	{
		tmp = GetComponent<TMP_Text>();
		tmp.text = texts[(int)SettingsManager.Instance.Settings.Language];
	}

	private void OnEnable()
	{
		SettingsManager.Instance.Settings.LanguageChanged += Settings_LanguageChanged;
	}

	private void OnDisable()
	{
		//SettingsManager.Instance.Settings.LanguageChanged -= Settings_LanguageChanged;
	}

	private void Settings_LanguageChanged(object sender, System.EventArgs e)
	{
		tmp.text = texts[(int)SettingsManager.Instance.Settings.Language];
	}
}
