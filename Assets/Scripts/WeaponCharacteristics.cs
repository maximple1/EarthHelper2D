using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeaponCharacteristics : MonoBehaviour
{
    public QuickSlotPanel quickSlotPanel;
    public CharacterController2D characterScript;

    [Header("Sprites")]
    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite stoneSprite;
    [SerializeField] private Sprite goldSprite;
    [SerializeField] private Sprite diamondSprite;

    [Header("ResourceIconReferences")]
    [SerializeField] private Image swordResourceIcon;
    [SerializeField] private Image axeResourceIcon;
    [SerializeField] private Image pickaxeResourceIcon;

    [Header("LVL texts")]
    public TMP_Text swordLvlText;
    public TMP_Text axeLvlText;
    public TMP_Text pickaxeLvlText;

    [Header("Damage texts")]
    public TMP_Text swordDamageText;
    public TMP_Text axeDamageText;
    public TMP_Text pickaxeDamageText;

    [Header("Resource texts")]
    public TMP_Text swordResourceText;
    public TMP_Text axeResourceText;
    public TMP_Text pickaxeResourceText;

    [Header("Sword")]
    public int swordDamage;
    public int swordLvl;
    public int amountToUpgradeSword;

    [Header("Axe")]
    public int axeDamage;
    public int axeLvl;
    public int amountToUpgradeAxe;

    [Header("Pickaxe")]
    public int pickaxeDamage;
    public int pickaxeLvl;
    public int amountToUpgradePickaxe;

    private void Start()
    {
        swordLvlText.text = "LVL: " + swordLvl;
        axeLvlText.text = "LVL: " + axeLvl;
        pickaxeLvlText.text = "LVL: " + pickaxeLvl;

        swordDamageText.text = "Damage: " + swordDamage;
        axeDamageText.text = "Damage: " + axeDamage;
        pickaxeDamageText.text = "Damage: " + pickaxeDamage;

        swordResourceText.text = amountToUpgradeSword.ToString();
        axeResourceText.text = amountToUpgradeAxe.ToString();
        pickaxeResourceText.text = amountToUpgradePickaxe.ToString();
    }
    public void ChangeSwordLvl()
    {
        switch (quickSlotPanel.swordType)
        {
            case QuickSlotPanel.SwordTypes.Wooden:
                if(amountToUpgradeSword <= ResourceStorage.wood)
                {
                    ResourceStorage.AddWood(-amountToUpgradeSword);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.SwordTypes.Metal:
                if (amountToUpgradeSword <= ResourceStorage.stone)
                {
                    ResourceStorage.AddStone(-amountToUpgradeSword);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.SwordTypes.Gold:
                if (amountToUpgradeSword <= ResourceStorage.gold)
                {
                    ResourceStorage.AddGold(-amountToUpgradeSword);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.SwordTypes.Diamond:
                if (amountToUpgradeSword <= ResourceStorage.diamond)
                {
                    ResourceStorage.AddDiamond(-amountToUpgradeSword);
                }
                else
                {
                    return;
                }
                break;
        }
        swordLvl++;
        swordDamage *= 2;
        amountToUpgradeSword = Mathf.RoundToInt((float)amountToUpgradeSword * 2.5f);
        swordLvlText.text = "LVL: " + swordLvl;
        swordDamageText.text = "Damage: " + swordDamage;
        swordResourceText.text = amountToUpgradeSword.ToString();
        //characterScript.damage = swordDamage;
        if (swordLvl >= 3 && swordLvl < 6)
        {
            quickSlotPanel.swordType = QuickSlotPanel.SwordTypes.Metal;
            swordResourceIcon.sprite = stoneSprite;
            // change to metal
        }
        else if (swordLvl >= 6 && swordLvl < 9)
        {
            quickSlotPanel.swordType = QuickSlotPanel.SwordTypes.Gold;
            swordResourceIcon.sprite = goldSprite;
            swordResourceIcon.color = quickSlotPanel.goldColor;
            //change to gold
        }
        else if(swordLvl >= 12)
        {
            quickSlotPanel.swordType = QuickSlotPanel.SwordTypes.Diamond;
            swordResourceIcon.sprite = diamondSprite;
            swordResourceIcon.color = quickSlotPanel.diamondColor;
            //change to diamond
        }
        quickSlotPanel.UpdateWeaponType();
    }
   
    public void ChangeAxeLvl()
    {
        switch (quickSlotPanel.axeType)
        {
            case QuickSlotPanel.AxeTypes.Wooden:
                if (amountToUpgradeAxe <= ResourceStorage.wood)
                {
                    ResourceStorage.AddWood(-amountToUpgradeAxe);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.AxeTypes.Metal:
                if (amountToUpgradeAxe <= ResourceStorage.stone)
                {
                    ResourceStorage.AddStone(-amountToUpgradeAxe);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.AxeTypes.Gold:
                if (amountToUpgradeAxe <= ResourceStorage.gold)
                {
                    ResourceStorage.AddGold(-amountToUpgradeAxe);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.AxeTypes.Diamond:
                if (amountToUpgradeAxe <= ResourceStorage.diamond)
                {
                    ResourceStorage.AddDiamond(-amountToUpgradeAxe);
                }
                else
                {
                    return;
                }
                break;
        }
        axeLvl++;
        axeDamage *= 2;
        amountToUpgradeAxe = Mathf.RoundToInt((float)amountToUpgradeAxe * 2.5f);
        axeLvlText.text = "LVL: " + axeLvl;
        axeDamageText.text = "Damage: " + axeDamage;
        axeResourceText.text = amountToUpgradeAxe.ToString();
        //characterScript.damage = swordDamage;
        if (axeLvl >= 3 && axeLvl < 6)
        {
            quickSlotPanel.axeType = QuickSlotPanel.AxeTypes.Metal;
            axeResourceIcon.sprite = stoneSprite;
            // change to metal
        }
        else if (axeLvl >= 6 && axeLvl < 9)
        {
            quickSlotPanel.axeType = QuickSlotPanel.AxeTypes.Gold;
            axeResourceIcon.sprite = goldSprite;
            axeResourceIcon.color = quickSlotPanel.goldColor;
            //change to gold
        }
        else if (axeLvl >= 12)
        {
            quickSlotPanel.axeType = QuickSlotPanel.AxeTypes.Diamond;
            axeResourceIcon.sprite = diamondSprite;
            axeResourceIcon.color = quickSlotPanel.diamondColor;
            //change to diamond
        }
        quickSlotPanel.UpdateWeaponType();

    }
    public void ChangePickaxeLvl()
    {
        switch (quickSlotPanel.pickaxeType)
        {
            case QuickSlotPanel.PickaxeTypes.Wooden:
                if (amountToUpgradePickaxe <= ResourceStorage.wood)
                {
                    ResourceStorage.AddWood(-amountToUpgradePickaxe);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.PickaxeTypes.Metal:
                if (amountToUpgradePickaxe <= ResourceStorage.stone)
                {
                    ResourceStorage.AddStone(-amountToUpgradePickaxe);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.PickaxeTypes.Gold:
                if (amountToUpgradePickaxe <= ResourceStorage.gold)
                {
                    ResourceStorage.AddGold(-amountToUpgradePickaxe);
                }
                else
                {
                    return;
                }
                break;
            case QuickSlotPanel.PickaxeTypes.Diamond:
                if (amountToUpgradePickaxe <= ResourceStorage.diamond)
                {
                    ResourceStorage.AddDiamond(-amountToUpgradePickaxe);
                }
                else
                {
                    return;
                }
                break;
        }
        pickaxeLvl++;
        pickaxeDamage *=  2;
        amountToUpgradePickaxe = Mathf.RoundToInt((float)amountToUpgradePickaxe * 2.5f);
        pickaxeLvlText.text = "LVL: " + pickaxeLvl;
        pickaxeDamageText.text = "Damage: " + pickaxeDamage;
        pickaxeResourceText.text = amountToUpgradePickaxe.ToString();
        if (pickaxeLvl >= 3 && pickaxeLvl < 6)
        {
            quickSlotPanel.pickaxeType = QuickSlotPanel.PickaxeTypes.Metal;
            pickaxeResourceIcon.sprite = stoneSprite;
            // change to metal
        }
        else if (pickaxeLvl >= 6 && pickaxeLvl < 9)
        {
            quickSlotPanel.pickaxeType = QuickSlotPanel.PickaxeTypes.Gold;
            pickaxeResourceIcon.sprite = goldSprite;
            pickaxeResourceIcon.color = quickSlotPanel.goldColor;
            //change to gold
        }
        else if (pickaxeLvl >= 12)
        {
            quickSlotPanel.pickaxeType = QuickSlotPanel.PickaxeTypes.Diamond;
            pickaxeResourceIcon.sprite = diamondSprite;
            pickaxeResourceIcon.color = quickSlotPanel.diamondColor;
            //change to diamond
        }
        quickSlotPanel.UpdateWeaponType();
    }
}
