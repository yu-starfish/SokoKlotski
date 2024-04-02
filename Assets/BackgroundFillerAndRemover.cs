using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundFillerAndRemover : MonoBehaviour
{
    public Tilemap tilemap; // �������� Tilemap
    //private Collider2D colliderToRemove; // Ҫ����������� Collider ���������������������ƶ�����̬�仯������������
    private Bounds colliderBounds;//������������ֱ�Ӵ洢Ҫ���������򣡣���������
    public TileBase tile; // Ҫ���Ĵ�ש
    //public Camera mainCamera; // �������
    TileBase[] deletedTiles;// ��ʱɾ����tile����
    private Vector3 lastPosition; // ��һ֡��λ��

    private void Start()
    {
        RemoveTilesInColliderBounds();
        // �ڿ�ʼʱ��¼��ʼλ��
        lastPosition = transform.position;
    }

    void FixedUpdate()//����Update()������
    {
        //�������map��Water��������
        //FillTilemapInView();�������˷������ش��������⣡����
        Vector3 currentPosition = transform.position;

        // ���λ���Ƿ����仯
        if (currentPosition != lastPosition)
        {
            // ����ƶ�ǰ��Slider����
            FillTilesOfPrevious();
            // ��ɾ��Slider�����tile
            RemoveTilesInColliderBounds();
            // ������һ֡��λ��Ϊ��ǰλ��
            lastPosition = currentPosition;
        }
    }

    //void FillTilemapInView()
    //{
    //    // ��ȡ����������ӿڷ�Χ
    //    Rect viewportRect = mainCamera.rect;
    //    Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(viewportRect.x, viewportRect.y, mainCamera.nearClipPlane));
    //    Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(viewportRect.xMax, viewportRect.yMax, mainCamera.nearClipPlane));

    //    // ���ӿڷ�Χת��Ϊ Tilemap �ĵ�Ԫ������
    //    Vector3Int bottomLeftCell = tilemap.WorldToCell(bottomLeft);
    //    Vector3Int topRightCell = tilemap.WorldToCell(topRight);

    //    // ���ӿڷ�Χ������ש
    //    tilemap.BoxFill(bottomLeftCell, tile, bottomLeftCell.x, bottomLeftCell.y, topRightCell.x, topRightCell.y);
    //}

    void FillTilesOfPrevious()
    {
        //// ��ȡҪ������������߽��
        //Bounds colliderBounds = colliderToRemove.bounds;

        // ������߽��ת��Ϊ Tilemap ����ϵ�µı߽��
        Vector3Int minTile = tilemap.WorldToCell(colliderBounds.min);//print(minTile);
        Vector3Int maxTile = tilemap.WorldToCell(colliderBounds.max);

        // ���ɾ����tile
        tilemap.SetTilesBlock(new BoundsInt(minTile.x, minTile.y, 0, maxTile.x - minTile.x + 1, maxTile.y - minTile.y + 1, 1), deletedTiles);
    }
    void RemoveTilesInColliderBounds()
    {
        //tilemap = GameObject.Find("Water/Background").GetComponent<Tilemap>();
        Collider2D colliderToRemove = GetComponent<BoxCollider2D>();

        // ��ȡҪɾ�����������߽��
        colliderBounds = colliderToRemove.bounds;

        // ������߽��ת��Ϊ Tilemap ����ϵ�µı߽��
        Vector3Int minTile = tilemap.WorldToCell(colliderBounds.min);//print(minTile);
        Vector3Int maxTile = tilemap.WorldToCell(colliderBounds.max);

        // ��ָ�������ڻ�ȡ���д�ש��������ȫ������Ϊ�մ�ש
        //deletedTiles = tilemap.GetTilesBlock(ConvertToBoundsInt(colliderBounds));
        deletedTiles = new TileBase[(maxTile.x - minTile.x + 1) * (maxTile.y - minTile.y + 1)];
        Array.Fill(deletedTiles, tile);
        TileBase[] emptyTiles = new TileBase[(maxTile.x - minTile.x + 1) * (maxTile.y - minTile.y + 1)];
        tilemap.SetTilesBlock(new BoundsInt(minTile.x, minTile.y, 0, maxTile.x - minTile.x + 1, maxTile.y - minTile.y + 1, 1), emptyTiles);
    }
    //BoundsInt ConvertToBoundsInt(Bounds bounds)
    //{
    //    // ʹ�� BoundsInt ���캯���� Bounds ת��Ϊ BoundsInt
    //    BoundsInt boundsInt = new BoundsInt(
    //        Mathf.RoundToInt(bounds.min.x),
    //        Mathf.RoundToInt(bounds.min.y),
    //        Mathf.RoundToInt(bounds.min.z),
    //        Mathf.CeilToInt(bounds.size.x),
    //        Mathf.CeilToInt(bounds.size.y),
    //        Mathf.CeilToInt(bounds.size.z)
    //    );

    //    return boundsInt;
    //}
}
