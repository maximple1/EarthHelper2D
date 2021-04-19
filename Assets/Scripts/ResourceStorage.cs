using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public static int wood;
    public static int stone;
    public static int gold;
    public static int diamond;
    [SerializeField]public static TMP_Text woodText;
    [SerializeField] public static TMP_Text stoneText;
    [SerializeField] public static TMP_Text goldText;
    [SerializeField] public static TMP_Text diamondText;

    private void Awake()
    {
        woodText = GameObject.Find("WoodCounter").GetComponent<TMP_Text>();
        stoneText = GameObject.Find("StoneCounter").GetComponent<TMP_Text>();
        goldText = GameObject.Find("GoldCounter").GetComponent<TMP_Text>();
        diamondText = GameObject.Find("DiamondCounter").GetComponent<TMP_Text>();
        woodText.text = wood.ToString();
        stoneText.text = stone.ToString();
        goldText.text = gold.ToString();
        diamondText.text = diamond.ToString();
    }
    public static void AddWood(int additionalWood)
    {
        wood += additionalWood;
        woodText.text = wood.ToString();
    }
    public static void AddStone(int additionalStone)
    {
        stone += additionalStone;
        stoneText.text = stone.ToString();
    }
    public static void AddGold(int additionalGold)
    {
        gold += additionalGold;
        goldText.text = gold.ToString();
    }
    public static void AddDiamond(int additionalDiamond)
    {
        diamond += additionalDiamond;
        diamondText.text = diamond.ToString();
    }

}
