using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponRack : MonoBehaviour
{
    public LayerMask triggerLayerMask;
    public GameObject button;
    public Sprite sprite;
    public GameObject prefab;

    private bool playerInCollider;
    private Controls controls;
    private Animator buttonAnim;
    private RadialMenu radial;
    private GameObject di;
    private GameObject player;

    public enum Weapons // your custom enumeration
    {
        NinjaStar,
        Kunai,
        Granade,
        Spear
    };
    public Weapons weaponType = Weapons.Spear;

    private void Start()
    {
        controls = GameManager.Instance.controls;
        buttonAnim = button.GetComponent<Animator>();
        radial = GameObject.Find("RadialMenu").GetComponent<RadialMenu>();
        player = GameObject.Find("Player");
        di = (GameObject)Resources.Load("Indicator");
    }

    private void FixedUpdate()
    {
        if (playerInCollider)
        {
            if (controls.Player.Interaction.triggered)
            {
                bool flag = false;
                foreach(var item in radial.menu)
                {
                    if (item.name == weaponType.ToString())
                    {
                        if (item.maxAmount - item.Amount > 0)
                            di.transform.Find("Text").GetComponent<TextMeshPro>().text = "+" + (item.maxAmount - item.Amount) + " " + weaponType.ToString();
                        else
                            di.transform.Find("Text").GetComponent<TextMeshPro>().text = "Inventory Full";
                        Instantiate(di, player.transform.Find("DISpawn").transform.position, Quaternion.identity);
                        item.Amount = item.maxAmount;
                        flag = true;
                    }
                }

                if (!flag)
                {
                    radial.AddWeapon(sprite, prefab, weaponType.ToString());
                    di.transform.Find("Text").GetComponent<TextMeshPro>().text = weaponType.ToString() + " refilled";
                    Instantiate(di, player.transform.Find("DISpawn").transform.position, Quaternion.identity);
                }
                radial.Reload();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
        {
            button.SetActive(true);
            playerInCollider = true;
            buttonAnim.SetBool("isSet", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & triggerLayerMask.value) != 0)
        {
            button.SetActive(false);
            playerInCollider = false;
            buttonAnim.SetBool("isSet", false);
        }
    }
}
