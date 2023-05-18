using UnityEngine;
using System.Collections.Generic;

public class Voronoi
{
    public struct Region
    {
        public List<Vector2> Points;
    }

    public List<Region> GenerateVoronoi(List<Vector2> points, Rect bounds)
    {
        List<Region> regions = new List<Region>();

        // Iterate over each pixel in the bounds
        for (int x = Mathf.RoundToInt(bounds.xMin); x < Mathf.RoundToInt(bounds.xMax); x++)
        {
            for (int y = Mathf.RoundToInt(bounds.yMin); y < Mathf.RoundToInt(bounds.yMax); y++)
            {
                Vector2 pixel = new Vector2(x, y);
                float minDistance = Mathf.Infinity;
                Region closestRegion = new Region();

                // Find the closest point to the current pixel
                foreach (Vector2 point in points)
                {
                    float distance = Vector2.Distance(pixel, point);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestRegion = new Region { Points = new List<Vector2> { pixel } };
                    }
                    else if (distance == minDistance)
                    {
                        closestRegion.Points.Add(pixel);
                    }
                }

                regions.Add(closestRegion);
            }
        }

        return regions;
    }
}