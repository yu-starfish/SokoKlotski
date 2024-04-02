using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    // ָ��Ҫ����Layer
    public LayerMask layerMask;

    private BoxCollider2D thisboxCollider;

    private void Awake()
    {
        // ��ȡ�������ϵ�BoxCollider2D���
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
        // �����������BoxCollider2D�������Ƿ���������ײ��
        // ʹ��boxCollider.bounds����ȡ��ײ��������ռ��еı߽��
        Bounds bounds = thisboxCollider.bounds;
        // ʹ��OverlapBox����������ڵ���ײ�壬�����ֱ�����������ĵ㡢��С����ת�Ƕ�
        Collider2D[] hits = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0, layerMask);

        // ����Ƿ�����ײ�壬���ų��������ײ��
        foreach (var hit in hits)
        {
            if (hit != thisboxCollider)
            {
                return false; // �ҵ�����������ײ�壬����false
            }
        }

        return true; // û���ҵ�������ײ�壬����true
    }
}
