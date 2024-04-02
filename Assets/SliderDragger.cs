using Unity.VisualScripting;
using UnityEngine;

public class SliderDragger : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public LayerMask layerMask;
    private BoxCollider2D thisboxCollider;

    // �����С
    public float gridSize = 1f;

    private void Awake()
    {
        // ��ȡ�������ϵ�BoxCollider2D���
        thisboxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        isDragging = true;
        //initialPosition = transform.position;//��갴��ʱ��¼��ʼλ��
        offset = transform.position - GetMouseWorldPos();//������������λ�ò�
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;//Ŀ���Ϊ���λ����λ�ò�֮��
            Vector3 snappedPosition = new Vector3(
                Mathf.Round(targetPos.x / gridSize) * gridSize,
                Mathf.Round(targetPos.y / gridSize) * gridSize,
                transform.position.z
            );
            /*
             * �Ƿ��ƶ����ж�
             */
            //if��굱ǰ��ק��!=Slider��ǰ���꣬��δ������ײ�����ƶ���Ŀ��㡢���³�ʼλ��
            //if��굱ǰ��ק��!=Slider��ǰ���꣬�ҷ�����ײ�����ƶ�
            if (snappedPosition!=transform.position)//�������ƶ�
            {
                //Ŀ��㲻������ײ���赲��
                // + thisboxCollider.offset�ǳ���Ҫ������
                if (IsColliderAreaFree((Vector2)snappedPosition + thisboxCollider.offset, thisboxCollider.bounds) == true)
                {
                    transform.position = snappedPosition;
                }
                //else if (IsColliderAreaFree((Vector2)snappedPosition + thisboxCollider.offset, thisboxCollider.bounds) == false)
                //{
                //    print("Colliding!");
                //    print("targetPos" + targetPos);
                //    print("snappedPosition" + snappedPosition);
                //    print("MousePos" + GetMouseWorldPos());
                //}
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    bool IsColliderAreaFree(Vector3 detectCenter,Bounds detectBounds)
    {
        // �����������BoxCollider2D�������Ƿ���������ײ��
        // ʹ��bounds����ȡ��ײ��������ռ��еı߽��
        // ʹ��OverlapBox����������ڵ���ײ�壬�����ֱ�����������ĵ㡢��С����ת�Ƕ�
        Collider2D[] hits = Physics2D.OverlapBoxAll(detectCenter, detectBounds.size, 0, layerMask);

        // ����Ƿ�����ײ�壬���ų��������ײ��
        foreach (var hit in hits)
        {
            if (hit != thisboxCollider)
            {
                //print("detectCenter"+detectCenter);
                //print("detectBounds.size"+detectBounds.size);
                return false; // �ҵ�����������ײ�壬����false
            }
        }

        return true; // û���ҵ�������ײ�壬����true
    }
}
