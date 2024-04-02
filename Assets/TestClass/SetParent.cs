using UnityEngine;

public class SetParent : MonoBehaviour
{
    // ��Inspector��������Щ����
    public GameObject parentObject; // ������
    public GameObject childObject;  // ����Ϊ������Ķ���

    void Start()
    {
        // ��childObject����ΪparentObject��������
        if (parentObject != null && childObject != null)
        {
            /*
             * ��δ����е�SetParent������������������parent���µĸ������Transform�����worldPositionStays��һ������ֵ�����ڿ�������������������Ƿ񱣳ֲ��䡣
             * ���worldPositionStays����Ϊfalse��������ľֲ�����ᱻ����Ϊ(0, 0, 0)����������תҲ�ᱻ���ã�ʹ���������븸��������λ�ú���ת����һ�¡�
             */
            childObject.transform.SetParent(parentObject.transform, true);
        }
    }
}
