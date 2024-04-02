using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    public Color finishColor;
    public Color originColor;
    public LayerMask mask;//mask存放遮罩，确保射线检测不检测到tilemap本身
    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;//获取箱子颜色
    }
    public bool IsAbleToDir(Vector2 movdir)//箱子的移动判断
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
    //spriteRenderer与TilemapRenderer类别相同，都可以设置layer优先级
    private void OnTriggerEnter2D(Collider2D collision)//该方法非常重要--用于对地图元素（克隆门等）的检测
    {
        if (collision.CompareTag("target"))
        {
            GetComponent<SpriteRenderer>().color = finishColor;
        }
    }
}
