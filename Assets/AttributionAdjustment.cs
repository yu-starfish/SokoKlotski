using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttributionAdjustment : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private Vector3 lastPosition; // ��һ֡��λ��
    void Start()
    {
        // ��ȡ����� BoxCollider2D ���
        myCollider = GetComponent<BoxCollider2D>();
        // �ڿ�ʼʱ��¼��ʼλ��
        lastPosition = transform.position;
    }
    void Update()
    {
        Vector3 currentPosition = transform.position;
        // ���λ���Ƿ����仯
        if (currentPosition != lastPosition)
        {
            // �����ײ�������Ӹ�����ϵ
            CheckCollisionsAndAdjust();
            // ������һ֡��λ��Ϊ��ǰλ��
            lastPosition = currentPosition;
        }
    }

    void CheckCollisionsAndAdjust()
    {
        // ��ȡ��������ײ��������ײ��
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, myCollider.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            // print(collider);
            // �����ײ�岻������������ײ�岻������ĸ�����
            if (myCollider != collider && collider.gameObject != transform.parent.gameObject)
            {
                // print(collider.transform);
                // ��������ĸ�����Ϊ��⵽����ײ�����
                transform.SetParent(collider.transform);
            }
        }
    }
}
