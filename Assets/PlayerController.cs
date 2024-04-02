using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Vector2 movdir;
    public LayerMask mask;//mask������ӵ�����
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//��������GetKey()����֡��սʿ������
            movdir = Vector2.up;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            movdir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            movdir = Vector2.down;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            movdir = Vector2.right;
        if(movdir!=Vector2.zero)
        {
            if(IsAbleToDir(movdir))
            {
                Move(movdir);
            }
        }
        movdir = Vector2.zero;//����Ҫ
    }

    private void Move(Vector2 movdir)
    {
        this.transform.Translate(movdir);        
        //throw new NotImplementedException();
    }

    private bool IsAbleToDir(Vector2 movdir)//player���ƶ��ж�
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, movdir,1f , mask);
        //RaycastHit2D hit=Physics2D.Raycast(this.transform.position + (Vector3)movdir * 0.5f, movdir, 0.5f);
        if (!hit) return true;
        //if(hit)   
        if (hit.collider.GetComponent<BoxBehavior>() != null)//hit.collider���ص������ߴ򵽵�����
            return hit.collider.GetComponent<BoxBehavior>().IsAbleToDir(movdir);
        else return false;
        //throw new NotImplementedException();
    }
}
