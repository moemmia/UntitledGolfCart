﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

public class TerrainFitter : MonoBehaviour {

    Terrain terr; // terrain to modify
    int hmWidth; // heightmap width
    int hmHeight; // heightmap height

    int posXInTerrain; // position of the game object in terrain width (x axis)
    int posYInTerrain; // position of the game object in terrain height (z axis)

    int size = 3; // the diameter of terrain portion that will raise under the game object

  public GameObject OuterMesh;

    // Use this for initialization
    public void DoYourThing () {

        terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapWidth;
        hmHeight = terr.terrainData.heightmapHeight;
        OuterMesh.GetComponent<MeshFilter>().sharedMesh.vertices.ToList().ForEach(x => SetHeight(x));

    }

    private void SetHeight(Vector3 v)
    {
        Vector3 pos = transform.TransformPoint(v);
        // get the normalized position of this game object relative to the terrain
        Vector3 tempCoord = (pos - terr.gameObject.transform.position);
        Vector3 coord;
        coord.x = tempCoord.x / terr.terrainData.size.x;
        coord.y = tempCoord.y / terr.terrainData.size.y;
        coord.z = tempCoord.z / terr.terrainData.size.z;

        // get the position of the terrain heightmap where this game object is
        posXInTerrain = (int)(coord.x * hmWidth);
        posYInTerrain = (int)(coord.z * hmHeight);

        // we set an offset so that all the raising terrain is under this game object
        int offset = size / 2;

        // get the heights of the terrain under this game object
        float[,] heights = terr.terrainData.GetHeights(posXInTerrain - offset, posYInTerrain - offset, size, size);

        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                heights[i, j] = coord.y - ((i+j)/ (size*4));

        // set the new height
        terr.terrainData.SetHeights(posXInTerrain - offset, posYInTerrain - offset, heights);
    }

}
