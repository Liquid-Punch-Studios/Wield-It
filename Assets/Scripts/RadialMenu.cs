﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{


    Image highlight;

    GameObject player;
    GameObject menuObj;
    GameObject baseTextObj;
    Controls controls;

    Health playerHealth;

    int segment;
    float segmentAngle;

    bool menuOpened = false;

    //GameObject[] baseTexts;

    //public string[] menu;

    [System.Serializable]
    public class Weapon
    {
        public string name;
        public Sprite sprite;
        public GameObject prefab;

        private Image image;

        private GameObject holder;
        public GameObject Holder
        {
            get { return holder; }
            set
            {
                holder = value;
            }
        }
    };

    public List<Weapon> menu;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        baseTextObj = GameObject.Find("BaseTexts");
        menuObj = GameObject.Find("Menu");
        highlight = GameObject.Find("Highlight").GetComponent<Image>();
        Reload();
        menuObj.SetActive(false);
    }

    public void FixedUpdate()
    {
        var isDead = playerHealth.Hp <= 0;
        if (!menuOpened && controls.UI.Tab.triggered && !isDead)
        {
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            menuOpened = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (menuOpened && controls.UI.Tab.triggered)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            menuObj.SetActive(false);
            menuOpened = false;
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        }
        if (menuOpened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
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
                foreach (var a in GameObject.FindGameObjectsWithTag("MainWeapon"))
                    Destroy(a);
                var weapon = Instantiate(menu[segment].prefab);
                weapon.transform.position = player.transform.Find("Hand").position;
                weapon.transform.rotation = player.transform.Find("Hand").rotation;
                player.GetComponent<Handler>().weapon = weapon;
                weapon.GetComponent<Sword>().user = player;
                weapon.GetComponent<ConfigurableJoint>().connectedBody = player.transform.Find("Hand").GetComponent<Rigidbody>();
                

                menuObj.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                menuOpened = false;
                InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;

            }

        }
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

    public void Reload()
    {
        segmentAngle = (360 / menu.Count);
        highlight.fillAmount = (360 - segmentAngle) / 360f;
        
        foreach ( Transform tr in baseTextObj.transform)
        {
            Destroy(tr.gameObject);
        }

        foreach (var weapon in menu)
        {
            weapon.Holder = new GameObject(weapon.name);
            weapon.Holder.transform.parent = baseTextObj.transform;
            weapon.Holder.AddComponent(typeof(Image));
            /*
            weapon.Holder.AddComponent(typeof(TextMeshProUGUI));
            var textMesh = weapon.Holder.GetComponent<TextMeshProUGUI>();
            textMesh.text = weapon.name;
            textMesh.fontSize = 24;
            textMesh.alignment = TextAlignmentOptions.Center;
            */
            var Image = weapon.Holder.GetComponent<Image>();
            Image.sprite = weapon.sprite;
            Image.preserveAspect = true;

            var radian = (-90 - (segmentAngle * menu.IndexOf(weapon)) - (segmentAngle / 2)) * Mathf.PI / 180;

            Image.transform.localPosition = new Vector3(150 * Mathf.Cos(radian), 150 * Mathf.Sin(radian));
            Image.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(weapon.Holder.transform.localPosition.y, weapon.Holder.transform.localPosition.x)) * 180 / Mathf.PI);
        }
    }
}