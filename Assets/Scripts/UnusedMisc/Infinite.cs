using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

internal class iTile
{
    public GameObject theiTile;
    public float creationTime;

    public iTile(GameObject t, float ct)
    {
        theiTile = t;
        creationTime = ct;
    }
}

public class Infinite : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;

    private int planeSize = 10;
    private int halfiTilesX = 5;
    private int halfiTilesZ = 5;

    private Vector3 startPose;

    private Hashtable tiles = new Hashtable();

    // Start is called before the first frame update
    private void Start()
    {
        this.gameObject.transform.position = Vector3.zero;
        startPose = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        for (int x = -halfiTilesX; x < halfiTilesX; x++)
        {
            for (int z = -halfiTilesZ; z < halfiTilesZ; z++)
            {
                Vector3 pos = new Vector3((x * planeSize + startPose.x), 0, (z * planeSize + startPose.z));

                GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                string tilename = "iTile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                t.name = tilename;
                iTile tile = new iTile(t, updateTime);
                tiles.Add(tilename, tile);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // determine how dar player moved

        int xMove = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
        int zMove = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

        if (Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize)
        {
            float updateTime = Time.realtimeSinceStartup;

            //force integer position and round to nearest tilesize
            int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

            for (int x = -halfiTilesX; x < halfiTilesX; x++)
            {
                for (int z = -halfiTilesZ; z < halfiTilesZ; z++)
                {
                    Vector3 pos = new Vector3((x * planeSize + playerX), 0, (z * planeSize + playerZ));

                    string tilename = "iTile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    if (!tiles.ContainsKey(tilename))
                    {
                        GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                        t.name = tilename;
                        iTile tile = new iTile(t, updateTime);
                        tiles.Add(tilename, tile);
                    }
                    else
                    {
                        (tiles[tilename] as iTile).creationTime = updateTime;
                    }
                }
            }

            //destroy all tiles and make new ones, while keeping old ones
            Hashtable newTerrain = new Hashtable();
            foreach (iTile tls in tiles.Values)
            {
                if (tls.creationTime != updateTime)
                {
                    //delete gameObject
                    Destroy(tls.theiTile);
                }
                else
                {
                    newTerrain.Add(tls.theiTile.name, tls);
                }
            }

            //copy new hashtable contents to the workinng hashtable
            tiles = newTerrain;

            startPose = player.transform.position;
        }
    }
}