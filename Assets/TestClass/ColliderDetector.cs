using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    // 指定要检测的Layer
    public LayerMask layerMask;

    private BoxCollider2D thisboxCollider;

    private void Awake()
    {
        // 获取该物体上的BoxCollider2D组件
        thisboxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsColliderAreaFree())
        {
            Debug.Log("Collider area is free of other colliders.");
        }
        else
        {
            Debug.Log("Collider area is overlapping with another collider.");
        }
    }

    bool IsColliderAreaFree()
    {
        // 检测给定物体的BoxCollider2D区域内是否有其他碰撞体
        // 使用boxCollider.bounds来获取碰撞器的世界空间中的边界框
        Bounds bounds = thisboxCollider.bounds;
        // 使用OverlapBox来检测区域内的碰撞体，参数分别是区域的中心点、大小和旋转角度
        Collider2D[] hits = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0, layerMask);

        // 检查是否有碰撞体，且排除自身的碰撞体
        foreach (var hit in hits)
        {
            if (hit != thisboxCollider)
            {
                return false; // 找到了其他的碰撞体，返回false
            }
        }

        return true; // 没有找到其他碰撞体，返回true
    }
}
