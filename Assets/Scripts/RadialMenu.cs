using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    Image highlight;

    GameObject menuObj;
    GameObject baseTextObj;
    Controls controls;

    int segment;
    float segmentAngle;

    bool menuOpened = false;

    GameObject[] baseTexts;

    public string[] menu;

    void Start()
    {
        baseTextObj = GameObject.Find("BaseTexts");
        menuObj = GameObject.Find("Menu");
        highlight = GameObject.Find("Highlight").GetComponent<Image>();
        baseTexts = new GameObject[menu.Length];

        for (int i = 0; i < baseTexts.Length; i++)
        {
            baseTexts[i] = Object.Instantiate(new GameObject(), baseTextObj.transform);
            baseTexts[i].name = menu[i];
            baseTexts[i].AddComponent(typeof(TextMeshProUGUI));
            baseTexts[i].GetComponent<TextMeshProUGUI>().text = menu[i];
            baseTexts[i].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            var tetha = 360f / baseTexts.Length;
            var radian = (-90 - (tetha * i) - (tetha / 2)) * Mathf.PI / 180;
            baseTexts[i].transform.localPosition = new Vector3(150 * Mathf.Cos(radian),150 * Mathf.Sin(radian));
            baseTexts[i].transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(baseTexts[i].transform.localPosition.y , baseTexts[i].transform.localPosition.x))* 180 / Mathf.PI);
        }

        segmentAngle = (360 / menu.Length);
        highlight.fillAmount = segmentAngle / 360f;
        menuObj.SetActive(false);
    }

    void FixedUpdate()
    {
        if (controls.UI.Tab.triggered)
            menuOpened = !menuOpened;

        if (menuOpened)
        {
            menuObj.SetActive(true);
            Vector2 mousePos = controls.UI.Mouse.ReadValue<Vector2>();
            mousePos.x -= Screen.width / 2f;
            mousePos.y -= Screen.height / 2f;
            float angle = 0;

            if (mousePos != Vector2.zero)
            {
                angle = Mathf.Atan2(mousePos.y, -mousePos.x) * 180 / Mathf.PI;
                angle += 90;
                if (angle < 0)
                    angle += 360;
            }

            segment = (int)Mathf.Floor(angle / segmentAngle);
            highlight.transform.rotation = Quaternion.Euler(0, 0, -segment * segmentAngle);

            if (controls.UI.MouseRelease.triggered)
            {
                Debug.Log(menu[segment]);
                menuObj.SetActive(false);
                menuOpened = false;
            }
        }
        else
            menuObj.SetActive(false);
    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
