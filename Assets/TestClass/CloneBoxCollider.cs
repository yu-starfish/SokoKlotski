using UnityEngine;

public class CloneBoxCollider : MonoBehaviour
{
    public GameObject objectToClone;
    public Vector2 spawnPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnWithSpecificComponents();
        }
    }

    void SpawnWithSpecificComponents()
    {
        if (objectToClone != null)
        {
            // 创建一个新的空游戏对象
            GameObject newObj = new GameObject("ClonedObject");

            // 复制位置信息
            newObj.transform.position = spawnPosition;

            // 如果需要，也可以复制旋转和缩放
            // newObj.transform.rotation = objectToClone.transform.rotation;
            // newObj.transform.localScale = objectToClone.transform.localScale;

            // 获取源对象的BoxCollider2D组件
            BoxCollider2D originalCollider = objectToClone.GetComponent<BoxCollider2D>();

            if (originalCollider != null)
            {
                // 向新对象添加BoxCollider2D组件
                BoxCollider2D newCollider = newObj.AddComponent<BoxCollider2D>();

                // 复制BoxCollider2D的属性
                newCollider.size = originalCollider.size;
                newCollider.offset = originalCollider.offset;
                newCollider.isTrigger = originalCollider.isTrigger;
                newCollider.usedByEffector = originalCollider.usedByEffector;
                // 根据需要复制更多属性
            }
        }
    }
}
