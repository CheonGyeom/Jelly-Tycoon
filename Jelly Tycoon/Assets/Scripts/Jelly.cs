using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jelly : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameManager gm;

    public int level; // ���� ��ȭ ����
    public int jelly_Class; // ���� ���� ����

    public Sprite[] jelly_spritelist; // ���� ��������Ʈ �迭
    public string[] jelly_namelist; // ���� �̸� �迭
    public int[] jelly_LevelUp_pricelist; // ��ȭ ���� �迭
    public int[] jelly_Upgrade_pricelist; // ���� ���� �迭
    public int[] market_item_pricelist; // ���� ��ǰ ���� �迭

    public float anim_size; // Ŭ�� �ִϸ��̼� ������


    public void OnPointerDown(PointerEventData data)
    {
        transform.localScale = new Vector3(anim_size + 0.05f, anim_size + 0.05f, 1);
    }

    public void OnPointerUp(PointerEventData data)
    {
        gm.gold += Mathf.Floor((level + 1) + ((level + 1) * (jelly_Class * 10))); // ����������
        SoundManager.instance.PlaySound("jellyTouch");

        PlayerPrefs.SetFloat("Gold", gm.gold); // ��� ����
        PlayerPrefs.SetFloat("Jelatin", gm.jelatin); // ����ƾ ����

        transform.localScale = new Vector3(anim_size, anim_size, 1);
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("Level");
        //PlayerPrefs.DeleteKey("Upgrade"); // ����׿� ���� X
    }
}
