using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundFillerAndRemover : MonoBehaviour
{
    public Tilemap tilemap; // 待操作的 Tilemap
    //private Collider2D colliderToRemove; // 要操作的区域的 Collider ！！！！！！会随物体移动而动态变化！！！！！！
    private Bounds colliderBounds;//！！！！！！直接存储要操作的区域！！！！！！
    public TileBase tile; // 要填充的瓷砖
    //public Camera mainCamera; // 主摄像机
    TileBase[] deletedTiles;// 临时删除的tile数组
    private Vector3 lastPosition; // 上一帧的位置

    private void Start()
    {
        RemoveTilesInColliderBounds();
        // 在开始时记录初始位置
        lastPosition = transform.position;
    }

    void FixedUpdate()//或者Update()？？？
    {
        //先填补整个map的Water（背景）
        //FillTilemapInView();！！！此方法有重大性能问题！！！
        Vector3 currentPosition = transform.position;

        // 检测位置是否发生变化
        if (currentPosition != lastPosition)
        {
            // 先填补移动前的Slider区域
            FillTilesOfPrevious();
            // 后删除Slider区域的tile
            RemoveTilesInColliderBounds();
            // 更新上一帧的位置为当前位置
            lastPosition = currentPosition;
        }
    }

    //void FillTilemapInView()
    //{
    //    // 获取主摄像机的视口范围
    //    Rect viewportRect = mainCamera.rect;
    //    Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(viewportRect.x, viewportRect.y, mainCamera.nearClipPlane));
    //    Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(viewportRect.xMax, viewportRect.yMax, mainCamera.nearClipPlane));

    //    // 将视口范围转换为 Tilemap 的单元格坐标
    //    Vector3Int bottomLeftCell = tilemap.WorldToCell(bottomLeft);
    //    Vector3Int topRightCell = tilemap.WorldToCell(topRight);

    //    // 在视口范围内填充瓷砖
    //    tilemap.BoxFill(bottomLeftCell, tile, bottomLeftCell.x, bottomLeftCell.y, topRightCell.x, topRightCell.y);
    //}

    void FillTilesOfPrevious()
    {
        //// 获取要填充区域的世界边界框
        //Bounds colliderBounds = colliderToRemove.bounds;

        // 将世界边界框转换为 Tilemap 坐标系下的边界框
        Vector3Int minTile = tilemap.WorldToCell(colliderBounds.min);//print(minTile);
        Vector3Int maxTile = tilemap.WorldToCell(colliderBounds.max);

        // 填充删除的tile
        tilemap.SetTilesBlock(new BoundsInt(minTile.x, minTile.y, 0, maxTile.x - minTile.x + 1, maxTile.y - minTile.y + 1, 1), deletedTiles);
    }
    void RemoveTilesInColliderBounds()
    {
        //tilemap = GameObject.Find("Water/Background").GetComponent<Tilemap>();
        Collider2D colliderToRemove = GetComponent<BoxCollider2D>();

        // 获取要删除区域的世界边界框
        colliderBounds = colliderToRemove.bounds;

        // 将世界边界框转换为 Tilemap 坐标系下的边界框
        Vector3Int minTile = tilemap.WorldToCell(colliderBounds.min);//print(minTile);
        Vector3Int maxTile = tilemap.WorldToCell(colliderBounds.max);

        // 在指定区域内获取所有瓷砖，并将其全部设置为空瓷砖
        //deletedTiles = tilemap.GetTilesBlock(ConvertToBoundsInt(colliderBounds));
        deletedTiles = new TileBase[(maxTile.x - minTile.x + 1) * (maxTile.y - minTile.y + 1)];
        Array.Fill(deletedTiles, tile);
        TileBase[] emptyTiles = new TileBase[(maxTile.x - minTile.x + 1) * (maxTile.y - minTile.y + 1)];
        tilemap.SetTilesBlock(new BoundsInt(minTile.x, minTile.y, 0, maxTile.x - minTile.x + 1, maxTile.y - minTile.y + 1, 1), emptyTiles);
    }
    //BoundsInt ConvertToBoundsInt(Bounds bounds)
    //{
    //    // 使用 BoundsInt 构造函数将 Bounds 转换为 BoundsInt
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
