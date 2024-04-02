using UnityEngine;

public class SetParent : MonoBehaviour
{
    // 在Inspector中设置这些对象
    public GameObject parentObject; // 父物体
    public GameObject childObject;  // 将成为子物体的对象

    void Start()
    {
        // 将childObject设置为parentObject的子物体
        if (parentObject != null && childObject != null)
        {
            /*
             * 这段代码中的SetParent函数接受两个参数：parent是新的父对象的Transform组件，worldPositionStays是一个布尔值，用于控制子物体的世界坐标是否保持不变。
             * 如果worldPositionStays设置为false，子物体的局部坐标会被重置为(0, 0, 0)，并且其旋转也会被重置，使得子物体与父物体的相对位置和旋转保持一致。
             */
            childObject.transform.SetParent(parentObject.transform, true);
        }
    }
}
