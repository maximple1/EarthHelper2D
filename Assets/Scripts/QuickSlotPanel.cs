using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotPanel : MonoBehaviour
{
    public CharacterController2D characterScript;
    public WeaponCharacteristics weaponCharacteristics;
    public enum CurrentWeapon { Sword, Axe, Pickaxe};
    public static CurrentWeapon currentWeapon = CurrentWeapon.Sword;
    public enum SwordTypes { Wooden, Metal, Gold, Diamond};
    public SwordTypes swordType = SwordTypes.Wooden;
    public enum AxeTypes { Wooden, Metal, Gold, Diamond };
    public AxeTypes axeType = AxeTypes.Wooden;
    public enum PickaxeTypes { Wooden, Metal, Gold, Diamond };
    public PickaxeTypes pickaxeType = PickaxeTypes.Wooden;

    [SerializeField] private Transform weaponParent;

    [SerializeField] private Image swordImage;
    [SerializeField] private Image axeImage;
    [SerializeField] private Image pickaxeImage;

    [SerializeField] private Image UswordImage;
    [SerializeField] private Image UaxeImage;
    [SerializeField] private Image UpickaxeImage;

    [SerializeField] public Color woodenColor = new Color32(183, 149, 63, 255);
    [SerializeField] public Color metalColor = new Color32(255, 255, 255, 255);
    [SerializeField] public Color goldColor = new Color32(245, 255, 0, 255);
    [SerializeField] public Color diamondColor = new Color32(0, 255, 227, 255);
    // Start is called before the first frame update
    void Start()
    {
        UpdateWeaponType();
    }
    public void UpdateWeaponType()
    {
        
        switch (swordType)
        {
            case SwordTypes.Wooden:
                swordImage.color = woodenColor;
                UswordImage.color = woodenColor;
                break;
            case SwordTypes.Metal:
                swordImage.color = metalColor;
                UswordImage.color = metalColor;

                break;
            case SwordTypes.Gold:
                swordImage.color = goldColor;
                UswordImage.color = goldColor;

                break;
            case SwordTypes.Diamond:
                swordImage.color = diamondColor;
                UswordImage.color = diamondColor;

                break;
        }
        switch (axeType)
        {
            case AxeTypes.Wooden:
                axeImage.color = woodenColor;
                UaxeImage.color = woodenColor;

                break;
            case AxeTypes.Metal:
                axeImage.color = metalColor;
                UaxeImage.color = metalColor;

                break;
            case AxeTypes.Gold:
                axeImage.color = goldColor;
                UaxeImage.color = goldColor;

                break;
            case AxeTypes.Diamond:
                axeImage.color = diamondColor;
                UaxeImage.color = diamondColor;

                break;
        }
        switch (pickaxeType)
        {
            case PickaxeTypes.Wooden:
                pickaxeImage.color = woodenColor;
                UpickaxeImage.color = woodenColor;

                break;
            case PickaxeTypes.Metal:
                pickaxeImage.color = metalColor;
                UpickaxeImage.color = metalColor;

                break;
            case PickaxeTypes.Gold:
                pickaxeImage.color = goldColor;
                UpickaxeImage.color = goldColor;

                break;
            case PickaxeTypes.Diamond:
                pickaxeImage.color = diamondColor;
                UpickaxeImage.color = diamondColor;

                break;
        }
        switch(currentWeapon)
        {
            case CurrentWeapon.Sword:
                SwitchSword();
                break;
            case CurrentWeapon.Axe:
                SwitchAxe();
                break;
            case CurrentWeapon.Pickaxe:
                SwitchPickaxe();
                break;
        }
    }

    public void SwitchSword()
    {
        currentWeapon = CurrentWeapon.Sword;
        characterScript.damage = weaponCharacteristics.swordDamage;
        for (int i = 0; i < weaponParent.childCount; i++)
        {
            weaponParent.GetChild(i).gameObject.SetActive(false);
        }
        switch (swordType)
        {
            case SwordTypes.Wooden:
                weaponParent.Find("WoodenSword").gameObject.SetActive(true);
                break;
            case SwordTypes.Metal:
                weaponParent.Find("MetalSword").gameObject.SetActive(true);
                break;
            case SwordTypes.Gold:
                weaponParent.Find("GoldSword").gameObject.SetActive(true);
                break;
            case SwordTypes.Diamond:
                weaponParent.Find("DiamondSword").gameObject.SetActive(true);
                break;
        }
    }
    public void SwitchAxe()
    {
        currentWeapon = CurrentWeapon.Axe;
        characterScript.damage = weaponCharacteristics.axeDamage;
        for (int i = 0; i < weaponParent.childCount; i++)
        {
            weaponParent.GetChild(i).gameObject.SetActive(false);
        }
        switch (axeType)
        {
            case AxeTypes.Wooden:
                weaponParent.Find("WoodenAxe").gameObject.SetActive(true);
                break;
            case AxeTypes.Metal:
                weaponParent.Find("MetalAxe").gameObject.SetActive(true);
                break;
            case AxeTypes.Gold:
                weaponParent.Find("GoldAxe").gameObject.SetActive(true);
                break;
            case AxeTypes.Diamond:
                weaponParent.Find("DiamondAxe").gameObject.SetActive(true);
                break;
        }
    }
    public void SwitchPickaxe()
    {
        currentWeapon = CurrentWeapon.Pickaxe;
        characterScript.damage = weaponCharacteristics.pickaxeDamage;
        for (int i = 0; i < weaponParent.childCount; i++)
        {
            weaponParent.GetChild(i).gameObject.SetActive(false);
        }
        switch (pickaxeType)
        {
            case PickaxeTypes.Wooden:
                weaponParent.Find("WoodenPickaxe").gameObject.SetActive(true);
                break;
            case PickaxeTypes.Metal:
                weaponParent.Find("MetalPickaxe").gameObject.SetActive(true);
                break;
            case PickaxeTypes.Gold:
                weaponParent.Find("GoldPickaxe").gameObject.SetActive(true);
                break;
            case PickaxeTypes.Diamond:
                weaponParent.Find("DiamondPickaxe").gameObject.SetActive(true);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchSword();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchAxe();   
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchPickaxe();
        }
    }
}
