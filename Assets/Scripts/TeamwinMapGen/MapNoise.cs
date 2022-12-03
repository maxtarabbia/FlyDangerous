using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class MapNoise
{
    public static Vector3[,] GenerateNoiseMap(int mapSize, Vector2 Offset, NoiseData[] NoiseArray)
    {

        Vector3[,] noiseMap = new Vector3[mapSize, mapSize];
        float halfsize = mapSize / 2f;

        AnimationCurve[] curvearray = new AnimationCurve[NoiseArray.Length];

        for (int i = 0; i < NoiseArray.Length; i++)
        {
            curvearray[i] = NoiseArray[i].getAnimationCurve();
        }


            for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                
                
                float sampleX = x + Offset.x - halfsize;
                float sampleY = y - Offset.y - halfsize;

                /*
                Profiler.BeginSample("Calculating Noise");
                noiseMap[x, y].y = OctavedNoise(VND.seed, VND.frequency, VND.frequencyScale, VND.persistance, VND.octaves, new Vector2(sampleX, sampleY));

                noiseMap[x, y].x = FlatDispcurve.Evaluate(noiseMap[x,y].y) * (OctavedNoise(HND.seed + 50, HND.frequency, HND.frequencyScale, HND.persistance, HND.octaves, new Vector2(sampleX, sampleY))-0.5f)
                    * HND.amplitude * HND.frequency;

                noiseMap[x, y].z = FlatDispcurve.Evaluate(noiseMap[x, y].y) * (OctavedNoise(HND.seed, HND.frequency, HND.frequencyScale, HND.persistance, HND.octaves, new Vector2(sampleX, sampleY))-0.5f)
                    * HND.amplitude * HND.frequency;

                noiseMap[x,y].y = Heightcurve.Evaluate(noiseMap[x,y].y) * VND.amplitude * VND.frequency;
                Profiler.EndSample();
                */
                for(int i = 0; i < NoiseArray.Length;i++)
                {
                    if (NoiseArray[i].isActive)
                    {

                       

                        var freq = NoiseArray[i].frequency;
                        var seed = NoiseArray[i].seed;
                        var freqscale = NoiseArray[i].frequencyScale;
                        var amp = NoiseArray[i].amplitude;
                        var pers = NoiseArray[i].persistance;
                        var oct = NoiseArray[i].octaves;
                        AnimationCurve curve = curvearray[i];



                        if (NoiseArray[i].isHorizontal)
                        {


                            float noiseHeightX = OctavedNoise(seed, freq, freqscale, pers, oct, new Vector2(sampleX, sampleY));
                            float noiseHeightY = OctavedNoise(seed + 50, freq, freqscale, pers, oct, new Vector2(sampleX, sampleY));

                            noiseHeightX -= 0.5f;
                            noiseHeightY -= 0.5f;

                            if (NoiseArray[i].UseVertical)
                            {
                                noiseHeightX *= curve.Evaluate(noiseMap[x, y].y);
                                noiseHeightY *= curve.Evaluate(noiseMap[x, y].y);
                            }
                            else
                            {
                                noiseHeightX = curve.Evaluate(noiseHeightX);
                                noiseHeightY = curve.Evaluate(noiseHeightY);
                            }

                            if (NoiseArray[i].noiseType == NoiseData.NoiseType.Constant)
                            {
                                noiseHeightX = curve.Evaluate(noiseMap[x, y].x);
                                noiseHeightY = curve.Evaluate(noiseMap[x, y].z);
                            }
                            if (NoiseArray[i].noiseType == NoiseData.NoiseType.Voronoi_Edge || NoiseArray[i].noiseType == NoiseData.NoiseType.Voronoi_Center)
                            {
                                noiseHeightX = curve.Evaluate(VorField(new Vector2(sampleX-500, sampleY + 300), freq, NoiseArray[i].noiseType == NoiseData.NoiseType.Voronoi_Edge));
                                noiseHeightY = curve.Evaluate(VorField(new Vector2(sampleX, sampleY), freq, true));
                            }

                            noiseHeightX *= amp;
                            noiseHeightY *= amp;



                            if (NoiseArray[i].maskMode == NoiseData.MaskMode.Multiply)
                            {
                                noiseMap[x,y].x *= noiseHeightX;
                                noiseMap[x,y].z *= noiseHeightY;
                            }
                            else if(NoiseArray[i].maskMode == NoiseData.MaskMode.Add)
                            {
                                noiseMap[x, y].x += noiseHeightX;
                                noiseMap[x, y].z += noiseHeightY;
                            }

                        }
                        else
                        { 
                            float noiseHeight = OctavedNoise(seed, freq, freqscale, pers, oct, new Vector2(sampleX, sampleY));


                            if (NoiseArray[i].UseVertical)
                            {
                                noiseHeight -= 0.5f;
                                noiseHeight *= curve.Evaluate(noiseMap[x, y].y);
                            }
                            else
                            {
                                noiseHeight = curve.Evaluate(noiseHeight);
                            }

                            if (NoiseArray[i].noiseType == NoiseData.NoiseType.Constant)
                                noiseHeight = curve.Evaluate(noiseMap[x, y].y);

                            if (NoiseArray[i].noiseType == NoiseData.NoiseType.Voronoi_Edge || NoiseArray[i].noiseType == NoiseData.NoiseType.Voronoi_Center)

                                noiseHeight = curve.Evaluate(VorField(new Vector2(sampleX, sampleY), freq, NoiseArray[i].noiseType == NoiseData.NoiseType.Voronoi_Edge));

                            noiseHeight *= amp;



                            if (NoiseArray[i].maskMode == NoiseData.MaskMode.Multiply)
                            {
                                noiseMap[x, y].y *= noiseHeight;

                            }
                            else if (NoiseArray[i].maskMode == NoiseData.MaskMode.Add)
                            {
                                noiseMap[x, y].y += noiseHeight;

                            }
                            UnityEngine.Profiling.Profiler.EndSample();
                        }
                        UnityEngine.Profiling.Profiler.EndSample();
                    }
                }
            }
        }

        return noiseMap;
    }
    [System.Serializable]
    public class NoiseData
    {
        public bool isActive;
        public int seed;
        public float frequency;
        public float frequencyScale = 1;
        public float amplitude;
        public bool isHorizontal;
        [Range(0f, 1f)]
        public float persistance;
        public int octaves;
        public AnimationCurve heightCurve;
        public bool UseVertical;
        public enum NoiseType {Perlin, Constant, Voronoi_Edge, Voronoi_Center}
        public NoiseType noiseType;
        public enum MaskMode {Add, Multiply};
        public MaskMode maskMode;
        
        public NoiseData(int seed,float frequency,float amplitude,float persistance,int octaves,AnimationCurve heightCurve)
        {
            this.seed = seed;
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.persistance = persistance;
            this.octaves = octaves;
            this.heightCurve = heightCurve;
        }
        public AnimationCurve getAnimationCurve()
        {
            return new (heightCurve.keys);
        }
    }
    public static float VorField(Vector2 Coord, float frequency, bool IsEdge)
    {
        Coord = Coord / frequency;
        Vector2 quantizedCoord = new Vector2( Mathf.Round(Coord.x),Mathf.Round(Coord.y));
        quantizedCoord -= new Vector2(1, 1);
        Vector2[] points = new Vector2[9];
        
        for (int i = 0; i < 3; i++)
        {
            points[i * 3] = quantizedCoord + new Vector2(0,i);
            points[i * 3 + 1] = quantizedCoord + new Vector2(1, i);
            points[i * 3 + 2] = quantizedCoord + new Vector2(2, i);
        }

        Endlessterrain.PRNG rng;
        float[] dists = new float[9];
        for (int i = 0; i < 9; i++)
        {
            rng = new Endlessterrain.PRNG(Mathf.RoundToInt(points[i].magnitude + points[i].x*1.5231f + points[i].y * 12.51f));
            rng.NextFloat();
            points[i] += new Vector2(rng.NextFloat() - 0.5f, rng.NextFloat() - 0.5f);
            dists[i] = Vector2.Distance(points[i],Coord);
        }
        Array.Sort(dists);
        if(IsEdge)
        {
            return dists[1] - dists[0];
        }
        else
        {
            return dists[0];
        }
        
    }
    public static float OctavedNoise(int seed, float inFrequency, float Xscale , float persistance, int octaves, Vector2 Coord)
    {
        UnityEngine.Profiling.Profiler.BeginSample("VariableDeclaration");
        float amplitude = 1;

        float maxPossibleHeight = 0;

        Vector2[] octaveOffsets = new Vector2[octaves];

        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("SettingOffsets");
        int offsetY = seed * 20;
        int offsetX = seed;
        for (int i = 0; i < octaves; i++)
        {
            offsetX = (seed << i + 1 & 8562861 | seed ^ offsetY) ^ (seed << 24);
            offsetY = (seed << i + 1 | 18728387 & seed ^ offsetX) ^ (seed << 14);


            octaveOffsets[i] = new Vector2(offsetX >> 10, offsetY >> 10);
            

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("PerlinCalculationLoop");
        amplitude = 1;
        float frequency = 1;
        float noiseheight = 0;

        
        for (int i = 0; i < octaves; i++)
        {
            float sampleX = (Coord.x + octaveOffsets[i].x) / inFrequency * frequency * Xscale;
            float sampleY = (Coord.y + octaveOffsets[i].y) / inFrequency * frequency;


            
            noiseheight += Mathf.PerlinNoise(sampleX, sampleY) * amplitude;

            amplitude *= persistance;
            frequency *= 2;
        }
        
        float height = noiseheight/maxPossibleHeight;
        UnityEngine.Profiling.Profiler.EndSample();
        return height;

    }
}

