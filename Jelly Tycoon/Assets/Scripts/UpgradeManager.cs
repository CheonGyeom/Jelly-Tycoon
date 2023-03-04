using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Jelly jelly; // ���� ��ũ��Ʈ

    public Text level_text; // ��ȭ���� �ؽ�Ʈ
    public Text upgradeLevel_text; // �������� �ؽ�Ʈ

    public Image jelly_Image; // ���� �̹���
    public Image jelly_UnlockImage; // �ر� �˾� â ���� �̹���
    public Text jelly_Name; // ���� �̸�
    public Text jelly_LevelUp_Price; // ���� ��ȭ ����
    public Text jelly_Upgrade_Price; // ���� ���� ����

    public Text market_item_1; // ���� 1�� ��ǰ ���� ����
    public Text market_item_2; // ���� 2�� ��ǰ ���� ����
    public Text market_item_3; // ���� 3�� ��ǰ ���� ����

    public GameObject unlockPanel; // �ر� �˾�â
    public GameObject marketPanel; // ���� â

    private void Awake()
    {
        // ������ �ҷ�����
        jelly.level = PlayerPrefs.GetInt("Level");
        jelly.jelly_Class = PlayerPrefs.GetInt("Upgrade");

        jelly_Image.sprite = jelly.jelly_spritelist[jelly.jelly_Class];
        jelly_Name.text = jelly.jelly_namelist[jelly.jelly_Class]; // ���� �̸� ����

        jelly_LevelUp_Price.text = string.Format("{0:n0}", jelly.jelly_LevelUp_pricelist[jelly.level]); // ��ȭ ���� ����
        jelly_Upgrade_Price.text = string.Format("{0:n0}", jelly.jelly_Upgrade_pricelist[jelly.jelly_Class]); // ���� ���� ����

        market_item_1.text = string.Format("{0:n0}", jelly.market_item_pricelist[0]); // ���� 1�� ��ǰ ���� ����
        market_item_2.text = string.Format("{0:n0}", jelly.market_item_pricelist[1]); // ���� 2�� ��ǰ ���� ����
        market_item_3.text = string.Format("{0:n0}", jelly.market_item_pricelist[2]); // ���� 3�� ��ǰ ���� ����
    }

    private void Update()
    {
        TextRenewal();
    }

    // ���� ��ȭ
    public void JellyLevelUp()
    {
        if (jelly.gm.gold < jelly.jelly_LevelUp_pricelist[jelly.level])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        jelly.gm.gold -= jelly.jelly_LevelUp_pricelist[jelly.level]; // ����

        jelly.level += 1;
        SoundManager.instance.PlaySound("BuyItem");
        PlayerPrefs.SetInt("Level", jelly.level); // ��ȭ ���� ����

        jelly_LevelUp_Price.text = string.Format("{0:n0}", jelly.jelly_LevelUp_pricelist[jelly.level]); // ��ȭ ���� ����
    }

    // ���� ����
    public void UpgradeJelly()
    {
        if (jelly.gm.jelatin < jelly.jelly_Upgrade_pricelist[jelly.jelly_Class])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        jelly.gm.jelatin -= jelly.jelly_Upgrade_pricelist[jelly.jelly_Class]; // ����

        jelly.jelly_Class += 1;
        SoundManager.instance.PlaySound("BuyItem");
        SoundManager.instance.PlaySound("UpgradeJelly");
        PlayerPrefs.SetInt("Upgrade", jelly.jelly_Class); // ���� ���� ����

        jelly_Image.sprite = jelly.jelly_spritelist[jelly.jelly_Class]; // ���� �̹��� ����
        jelly_UnlockImage.sprite = jelly.jelly_spritelist[jelly.jelly_Class]; // �ر� �˾� â ���� �̹��� ����
        jelly_Name.text = jelly.jelly_namelist[jelly.jelly_Class]; // ���� �̸� ����
        jelly_Upgrade_Price.text = string.Format("{0:n0}", jelly.jelly_Upgrade_pricelist[jelly.jelly_Class]); // ���� ���� ����

        unlockPanel.gameObject.SetActive(true);
    }

    // �ؽ�Ʈ ������
    private void TextRenewal()
    {
        level_text.text = "���� " + (jelly.level + 1).ToString();
        upgradeLevel_text.text = "���� " + (jelly.jelly_Class + 1).ToString();

    }

    // ���� �˾� â �ݱ�
    public void ClosePopUp()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        unlockPanel.gameObject.SetActive(false);
    }

    // ���� �˾� â ����
    public void OpenMarket()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        marketPanel.gameObject.SetActive(true);
    }

    // ���� �˾� â �ݱ�
    public void CloseMarket()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        marketPanel.gameObject.SetActive(false);
    }

    public void BuyItem1()
    {
        if (jelly.gm.gold < jelly.market_item_pricelist[0])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        SoundManager.instance.PlaySound("BuyItem");
        jelly.gm.gold -= jelly.market_item_pricelist[0]; // ����
        jelly.gm.jelatin += 10; // 10 ����ƾ ����
    }

    public void BuyItem2()
    {
        if (jelly.gm.gold < jelly.market_item_pricelist[1])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        SoundManager.instance.PlaySound("BuyItem");
        jelly.gm.gold -= jelly.market_item_pricelist[1]; // ����
        jelly.gm.jelatin += 100; // 100 ����ƾ ����
    }

    public void BuyItem3()
    {
        if (jelly.gm.gold < jelly.market_item_pricelist[2])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        SoundManager.instance.PlaySound("BuyItem");
        jelly.gm.gold -= jelly.market_item_pricelist[2]; // ����
        jelly.gm.jelatin += 1000; // 1000 ����ƾ ����
    }
}
