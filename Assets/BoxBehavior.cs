using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    public Color finishColor;
    public Color originColor;
    public LayerMask mask;//mask������֣�ȷ�����߼�ⲻ��⵽tilemap����
    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;//��ȡ������ɫ
    }
    public bool IsAbleToDir(Vector2 movdir)//���ӵ��ƶ��ж�
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + (Vector3)movdir * 0.5f, movdir, 0.5f,mask);

        //Console.WriteLine(transform.position);
        if (!hit)
        {
            this.transform.Translate(movdir);
            return true;
        }
        else return false;
    }
    //spriteRenderer��TilemapRenderer�����ͬ������������layer���ȼ�
    private void OnTriggerEnter2D(Collider2D collision)//�÷����ǳ���Ҫ--���ڶԵ�ͼԪ�أ���¡�ŵȣ��ļ��
    {
        if (collision.CompareTag("target"))
        {
            GetComponent<SpriteRenderer>().color = finishColor;
        }
    }
}
