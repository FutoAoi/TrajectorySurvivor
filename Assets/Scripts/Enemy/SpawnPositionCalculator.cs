using UnityEngine;

public static class SpawnPositionCalculator
{
    /// <summary>
    /// プレイヤー中心の円周上、カメラ視界の外側にランダムな座標を返す
    /// </summary>
    public static Vector3 GetOffScreenPosition(Transform player, Camera cam, float margin = 2f)
    {
        // カメラのオルソグラフィックサイズや視野角から、画面に映る範囲の半径を算出
        float screenRadius = GetScreenCoverRadius(cam);
        float spawnRadius = screenRadius + margin;

        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * spawnRadius;

        return player.position + offset;
    }

    private static float GetScreenCoverRadius(Camera cam)
    {
        if (cam.orthographic)
        {
            // オルソカメラの場合、画面に映る範囲の対角線の半分を半径とする
            float height = cam.orthographicSize;
            float width = height * cam.aspect;
            return Mathf.Sqrt(width * width + height * height);
        }
        else
        {
            // 透視投影カメラの場合、カメラ高さと視野角から見える範囲を概算
            float camHeight = cam.transform.position.y;
            float halfFov = cam.fieldOfView * 0.5f * Mathf.Deg2Rad;
            float visibleRadius = camHeight * Mathf.Tan(halfFov);
            return visibleRadius * 1.2f; // 斜め視点の誤差を吸収する余裕係数
        }
    }
}