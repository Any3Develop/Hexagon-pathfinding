using UnityEngine;

public static class HexMetrices
{
    public const float OuterRadius = 10f;

    public const float InnerRadius = OuterRadius * 0.866025404f;

    public const float ElevationStep = 5f;

    public static readonly Vector3[] Corners = 
    {
        new Vector3(0f, 0f, OuterRadius),
        new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(0f, 0f, -OuterRadius),
        new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(0f, 0f, OuterRadius)
    };

    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return Corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return Corners[(int)direction + 1];
    }

    public static HexEdgeType GetEdgeType(int elevation1, int elevation2)
    {
        if (elevation1 == elevation2)
        {
            return HexEdgeType.Flat;
        }
        int delta = elevation2 - elevation1;
        if (delta == 1 || delta == -1)
        {
            return HexEdgeType.Slope;
        }
        return HexEdgeType.Cliff;
    }
}