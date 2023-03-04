using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Jelly jelly; // 젤리 스크립트

    public Text level_text; // 강화레벨 텍스트
    public Text upgradeLevel_text; // 연구레벨 텍스트

    public Image jelly_Image; // 젤리 이미지
    public Image jelly_UnlockImage; // 해금 팝업 창 젤리 이미지
    public Text jelly_Name; // 젤리 이름
    public Text jelly_LevelUp_Price; // 젤리 강화 가격
    public Text jelly_Upgrade_Price; // 젤리 연구 가격

    public Text market_item_1; // 상점 1번 물품 구매 가격
    public Text market_item_2; // 상점 2번 물품 구매 가격
    public Text market_item_3; // 상점 3번 물품 구매 가격

    public GameObject unlockPanel; // 해금 팝업창
    public GameObject marketPanel; // 상점 창

    private void Awake()
    {
        // 데이터 불러오기
        jelly.level = PlayerPrefs.GetInt("Level");
        jelly.jelly_Class = PlayerPrefs.GetInt("Upgrade");

        jelly_Image.sprite = jelly.jelly_spritelist[jelly.jelly_Class];
        jelly_Name.text = jelly.jelly_namelist[jelly.jelly_Class]; // 젤리 이름 변경

        jelly_LevelUp_Price.text = string.Format("{0:n0}", jelly.jelly_LevelUp_pricelist[jelly.level]); // 강화 가격 변경
        jelly_Upgrade_Price.text = string.Format("{0:n0}", jelly.jelly_Upgrade_pricelist[jelly.jelly_Class]); // 연구 가격 변경

        market_item_1.text = string.Format("{0:n0}", jelly.market_item_pricelist[0]); // 상점 1번 물품 가격 변경
        market_item_2.text = string.Format("{0:n0}", jelly.market_item_pricelist[1]); // 상점 2번 물품 가격 변경
        market_item_3.text = string.Format("{0:n0}", jelly.market_item_pricelist[2]); // 상점 3번 물품 가격 변경
    }

    private void Update()
    {
        TextRenewal();
    }

    // 젤리 강화
    public void JellyLevelUp()
    {
        if (jelly.gm.gold < jelly.jelly_LevelUp_pricelist[jelly.level])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        jelly.gm.gold -= jelly.jelly_LevelUp_pricelist[jelly.level]; // 결제

        jelly.level += 1;
        SoundManager.instance.PlaySound("BuyItem");
        PlayerPrefs.SetInt("Level", jelly.level); // 강화 레벨 저장

        jelly_LevelUp_Price.text = string.Format("{0:n0}", jelly.jelly_LevelUp_pricelist[jelly.level]); // 강화 가격 변경
    }

    // 젤리 연구
    public void UpgradeJelly()
    {
        if (jelly.gm.jelatin < jelly.jelly_Upgrade_pricelist[jelly.jelly_Class])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        jelly.gm.jelatin -= jelly.jelly_Upgrade_pricelist[jelly.jelly_Class]; // 결제

        jelly.jelly_Class += 1;
        SoundManager.instance.PlaySound("BuyItem");
        SoundManager.instance.PlaySound("UpgradeJelly");
        PlayerPrefs.SetInt("Upgrade", jelly.jelly_Class); // 연구 레벨 저장

        jelly_Image.sprite = jelly.jelly_spritelist[jelly.jelly_Class]; // 젤리 이미지 변경
        jelly_UnlockImage.sprite = jelly.jelly_spritelist[jelly.jelly_Class]; // 해금 팝업 창 젤리 이미지 변경
        jelly_Name.text = jelly.jelly_namelist[jelly.jelly_Class]; // 젤리 이름 변경
        jelly_Upgrade_Price.text = string.Format("{0:n0}", jelly.jelly_Upgrade_pricelist[jelly.jelly_Class]); // 연구 가격 변경

        unlockPanel.gameObject.SetActive(true);
    }

    // 텍스트 리뉴얼
    private void TextRenewal()
    {
        level_text.text = "레벨 " + (jelly.level + 1).ToString();
        upgradeLevel_text.text = "레벨 " + (jelly.jelly_Class + 1).ToString();

    }

    // 연구 팝업 창 닫기
    public void ClosePopUp()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        unlockPanel.gameObject.SetActive(false);
    }

    // 상점 팝업 창 열기
    public void OpenMarket()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        marketPanel.gameObject.SetActive(true);
    }

    // 상점 팝업 창 닫기
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
        jelly.gm.gold -= jelly.market_item_pricelist[0]; // 결제
        jelly.gm.jelatin += 10; // 10 젤라틴 지급
    }

    public void BuyItem2()
    {
        if (jelly.gm.gold < jelly.market_item_pricelist[1])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        SoundManager.instance.PlaySound("BuyItem");
        jelly.gm.gold -= jelly.market_item_pricelist[1]; // 결제
        jelly.gm.jelatin += 100; // 100 젤라틴 지급
    }

    public void BuyItem3()
    {
        if (jelly.gm.gold < jelly.market_item_pricelist[2])
        {
            SoundManager.instance.PlaySound("BuyFail");
            return;
        }

        SoundManager.instance.PlaySound("BuyItem");
        jelly.gm.gold -= jelly.market_item_pricelist[2]; // 결제
        jelly.gm.jelatin += 1000; // 1000 젤라틴 지급
    }
}
