using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttributionAdjustment : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private Vector3 lastPosition; // 上一帧的位置
    void Start()
    {
        // 获取自身的 BoxCollider2D 组件
        myCollider = GetComponent<BoxCollider2D>();
        // 在开始时记录初始位置
        lastPosition = transform.position;
    }
    void Update()
    {
        Vector3 currentPosition = transform.position;
        // 检测位置是否发生变化
        if (currentPosition != lastPosition)
        {
            // 检测碰撞并更改子父级关系
            CheckCollisionsAndAdjust();
            // 更新上一帧的位置为当前位置
            lastPosition = currentPosition;
        }
    }

    void CheckCollisionsAndAdjust()
    {
        // 获取与自身碰撞的所有碰撞体
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, myCollider.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            // print(collider);
            // 如果碰撞体不是自身，并且碰撞体不是自身的父对象
            if (myCollider != collider && collider.gameObject != transform.parent.gameObject)
            {
                // print(collider.transform);
                // 更改自身的父对象为检测到的碰撞体对象
                transform.SetParent(collider.transform);
            }
        }
    }
}
