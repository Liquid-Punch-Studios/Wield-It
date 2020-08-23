using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Health health;
    Image hpBar;


    private void OnEnable()
    {
        health.HpChanged += Health_HpChanged;
    }
    private void OnDisable()
    {
        health.HpChanged -= Health_HpChanged;
    }

    private void Health_HpChanged(object sender, System.EventArgs e)
    {
        hpBar.fillAmount = health.Hp / health.MaxHp;
    }

    void Start()
    {
        hpBar = GetComponent<Image>();
    }
}
