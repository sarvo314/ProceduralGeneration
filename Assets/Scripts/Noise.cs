using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int map_width, int map_height, float scale)
    {
        float[,] noise_map = new float[map_width, map_height];

        if (scale <= 0)
        {
            scale = 1.1f;
        }

        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                float sample_x = x / scale;
                float sample_y = y / scale;

                float perline_value = Mathf.PerlinNoise(sample_x, sample_y);

                noise_map[x, y] = perline_value;
            }
        }
        return noise_map;
    }
}
