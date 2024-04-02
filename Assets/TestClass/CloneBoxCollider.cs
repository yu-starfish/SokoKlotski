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
            // ����һ���µĿ���Ϸ����
            GameObject newObj = new GameObject("ClonedObject");

            // ����λ����Ϣ
            newObj.transform.position = spawnPosition;

            // �����Ҫ��Ҳ���Ը�����ת������
            // newObj.transform.rotation = objectToClone.transform.rotation;
            // newObj.transform.localScale = objectToClone.transform.localScale;

            // ��ȡԴ�����BoxCollider2D���
            BoxCollider2D originalCollider = objectToClone.GetComponent<BoxCollider2D>();

            if (originalCollider != null)
            {
                // ���¶������BoxCollider2D���
                BoxCollider2D newCollider = newObj.AddComponent<BoxCollider2D>();

                // ����BoxCollider2D������
                newCollider.size = originalCollider.size;
                newCollider.offset = originalCollider.offset;
                newCollider.isTrigger = originalCollider.isTrigger;
                newCollider.usedByEffector = originalCollider.usedByEffector;
                // ������Ҫ���Ƹ�������
            }
        }
    }
}
