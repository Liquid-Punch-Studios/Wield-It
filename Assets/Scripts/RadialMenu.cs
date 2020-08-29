using System;
using System.CodeDom;
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
    Handler handler;
    Health playerHealth;
    TextMeshProUGUI throwableRemain;

    public static int segment;
    float segmentAngle;

    bool menuOpened = false;

    bool isDead = false;

    public bool IsDead
    {
        get { return isDead; }
        set
        {
            var old = isDead;
            if (old != value)
            {
                isDead = value;
                if (!isDead)
                    ChangeWeapon();
            }
        }
    }

    //GameObject[] baseTexts;

    //public string[] menu;

    [System.Serializable]
    public class Weapon
    {
        public string name;
        public Sprite sprite;
        public GameObject prefab;

        private int amount = 5;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

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
        controls = GameManager.Instance.controls;
        player = GameObject.Find("Player");
        handler = player.GetComponent<Handler>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        baseTextObj = GameObject.Find("BaseTexts");
        menuObj = GameObject.Find("Menu");
        highlight = GameObject.Find("Highlight").GetComponent<Image>();
        throwableRemain = GameObject.Find("ThrowableRemaining").GetComponent<TextMeshProUGUI>();
        throwableRemain.enabled = false;
        Reload();
        ChangeWeapon();
        menuObj.SetActive(false);
    }

    public void FixedUpdate()
    {
        IsDead = playerHealth.Hp <= 0;
        if (!menuOpened && controls.UI.Tab.triggered && !IsDead)
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
        if (menuOpened && controls.UI.TabRelease.triggered)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            menuObj.SetActive(false);
            menuOpened = false;
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;

            ChangeWeapon();

        }
        else if (menuOpened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.25f;
            menuObj.SetActive(true);
            Vector2 mousePos = controls.UI.Mouse.ReadValue<Vector2>();
            mousePos.x -= Screen.width / 2f;
            mousePos.y -= Screen.height / 2f;
            float angle = 0;

            if (mousePos != Vector2.zero && mousePos.magnitude > Screen.height / 20f)
            {
                angle = Mathf.Atan2(mousePos.y, -mousePos.x) * 180 / Mathf.PI;
                angle += 90;
                if (angle < 0)
                    angle += 360;
                segment = (int)Mathf.Floor(angle / segmentAngle);
            }
            

            highlight.transform.rotation = Quaternion.Euler(0, 0, -segment * segmentAngle);
        }

        else 
        {
            if(menu[segment].Amount <= 0)
            {
                segment = 0;
                ChangeWeapon();
            }
        }
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

    public void ChangeWeapon()
    {
        if (menu[segment].Amount >= 0)
        {
            foreach (var a in GameObject.FindGameObjectsWithTag("MainWeapon"))
                Destroy(a);
            var weapon = Instantiate(menu[segment].prefab);
            Debug.Log("Segment: " + segment + "\tWeapon Name: " + weapon.name);
            weapon.transform.position = player.transform.Find("Hand").position;
            weapon.transform.rotation = player.transform.Find("Hand").rotation;
            weapon.GetComponent<Sword>().user = player;
            throwableRemain.enabled = weapon.GetComponent<Sword>().throwable;
            weapon.GetComponent<ConfigurableJoint>().connectedBody = player.transform.Find("Hand").GetComponent<Rigidbody>();
            handler.weapon = weapon;
            handler.WeaponRb = weapon.GetComponent<Rigidbody>();

            if (weapon.TryGetComponent(out Mace _))
            {
                for (int i = weapon.transform.childCount-1; i >= 0 ; i--)
                    weapon.transform.GetChild(i).parent = null;
            }
        }
    }
}
