using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jelly : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameManager gm;

    public int level; // 젤리 강화 레벨
    public int jelly_Class; // 젤리 연구 레벨

    public Sprite[] jelly_spritelist; // 젤리 스프라이트 배열
    public string[] jelly_namelist; // 젤리 이름 배열
    public int[] jelly_LevelUp_pricelist; // 강화 가격 배열
    public int[] jelly_Upgrade_pricelist; // 연구 가격 배열
    public int[] market_item_pricelist; // 상점 물품 가격 배열

    public float anim_size; // 클릭 애니메이션 사이즈


    public void OnPointerDown(PointerEventData data)
    {
        transform.localScale = new Vector3(anim_size + 0.05f, anim_size + 0.05f, 1);
    }

    public void OnPointerUp(PointerEventData data)
    {
        gm.gold += Mathf.Floor((level + 1) + ((level + 1) * (jelly_Class * 10))); // 레벨디자인
        SoundManager.instance.PlaySound("jellyTouch");

        PlayerPrefs.SetFloat("Gold", gm.gold); // 골드 저장
        PlayerPrefs.SetFloat("Jelatin", gm.jelatin); // 젤라틴 저장

        transform.localScale = new Vector3(anim_size, anim_size, 1);
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("Level");
        //PlayerPrefs.DeleteKey("Upgrade"); // 디버그용 저장 X
    }
}
