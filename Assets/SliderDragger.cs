using Unity.VisualScripting;
using UnityEngine;

public class SliderDragger : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public LayerMask layerMask;
    private BoxCollider2D thisboxCollider;

    // 网格大小
    public float gridSize = 1f;

    private void Awake()
    {
        // 获取该物体上的BoxCollider2D组件
        thisboxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        isDragging = true;
        //initialPosition = transform.position;//鼠标按下时记录初始位置
        offset = transform.position - GetMouseWorldPos();//鼠标与对象坐标位置差
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;//目标点为鼠标位置与位置差之和
            Vector3 snappedPosition = new Vector3(
                Mathf.Round(targetPos.x / gridSize) * gridSize,
                Mathf.Round(targetPos.y / gridSize) * gridSize,
                transform.position.z
            );
            /*
             * 是否移动的判断
             */
            //if鼠标当前拖拽点!=Slider当前坐标，且未发生碰撞，则移动至目标点、更新初始位置
            //if鼠标当前拖拽点!=Slider当前坐标，且发生碰撞，则不移动
            if (snappedPosition!=transform.position)//鼠标存在移动
            {
                //目标点不存在碰撞（阻挡）
                // + thisboxCollider.offset非常重要！！！
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
        // 检测给定物体的BoxCollider2D区域内是否有其他碰撞体
        // 使用bounds来获取碰撞器的世界空间中的边界框
        // 使用OverlapBox来检测区域内的碰撞体，参数分别是区域的中心点、大小和旋转角度
        Collider2D[] hits = Physics2D.OverlapBoxAll(detectCenter, detectBounds.size, 0, layerMask);

        // 检查是否有碰撞体，且排除自身的碰撞体
        foreach (var hit in hits)
        {
            if (hit != thisboxCollider)
            {
                //print("detectCenter"+detectCenter);
                //print("detectBounds.size"+detectBounds.size);
                return false; // 找到了其他的碰撞体，返回false
            }
        }

        return true; // 没有找到其他碰撞体，返回true
    }
}
