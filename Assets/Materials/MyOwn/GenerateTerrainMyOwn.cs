using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainMyOwn : MonoBehaviour
{
    public int heightScale = 10;
    public float detailScale = 10.0f;
  /*  List<GameObject> myTrees = new List<GameObject>();
    List<GameObject> myWater = new List<GameObject>();
    List<GameObject> mySoap = new List<GameObject>(); */



    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices.Length; v++)
        {
            vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale,
                                              (vertices[v].z + this.transform.position.z) / detailScale) * heightScale;

            //generate soap
            /* if(vertices[v].y > 4 && Mathf.PerlinNoise((vertices[v].x + 5) / 10, (vertices[v].z + 5) / 10) * 10 > 4.5)
             {
                 GameObject newSoap = SoapPool.getSoap();
                 Vector3 soapPos = new Vector3(Random.Range(vertices[v].x + this.transform.position.x, 5f),
                                               Random.Range(0f, vertices[v].y)+5,
                                               Random.Range(vertices[v].z + this.transform.position.z, 5f));
                 newSoap.transform.position = soapPos;
                 newSoap.SetActive(true);
                 myTrees.Add(newSoap);
             } */


            //generate trees
            /*  if (vertices[v].y >3 && vertices[v].y<4  && Mathf.PerlinNoise((vertices[v].x + 5) / 10, (vertices[v].z + 5) / 10) * 10 > 4.6 )
              {
                  GameObject newTree = TreePool.getTree();
                  Vector3 treePos = new Vector3(vertices[v].x + this.transform.position.x,
                                                vertices[v].y,
                                                vertices[v].z + this.transform.position.z);
                  newTree.transform.position = treePos;
                  newTree.SetActive(true);
                  myTrees.Add(newTree);

                  newTree.gameObject.transform.localScale = transform.localScale * 0.5f;


              } 


              if (vertices[v].y > 4  && Mathf.PerlinNoise((vertices[v].x + 5) / 10, (vertices[v].z + 5) / 10) * 10 > 4.9)
              {
                  GameObject newTree = TreePool.getTree();
                  Vector3 treePos = new Vector3(vertices[v].x + this.transform.position.x,
                                                vertices[v].y,
                                                vertices[v].z + this.transform.position.z);
                  newTree.transform.position = treePos;
                  newTree.SetActive(true);
                  myTrees.Add(newTree);

                      newTree.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 255f);
                      newTree.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 255f);
                      newTree.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 255f);
                      newTree.gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 255f);
                      newTree.gameObject.transform.GetChild(4).GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 255f);

                      newTree.gameObject.transform.localScale = transform.localScale * 1.5f;

              } 


               if (vertices[v].y < 1.5 && Random.Range(1,10)<10 )
              {
                  GameObject newWater = WaterPool.getWater();
                  Vector3 WaterPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                vertices[v].y,
                                                vertices[v].z + this.transform.position.z);
                  newWater.transform.position = WaterPos;
                  newWater.SetActive(true);
                  myWater.Add(newWater);
              } 
          }
          */



            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            this.gameObject.AddComponent<MeshCollider>();
        }

     /*    void OnDestroy()
        {
            for (int i = 0; i < myTrees.Count; i++)
            {
                if (myTrees[i] != null)
                {
                    myTrees[i].SetActive(false);
                }
            }
            myTrees.Clear();

            for (int u = 0; u < myWater.Count; u++)
            {
                if (myWater[u] != null)
                {
                    myWater[u].SetActive(false);
                }
            }
            myWater.Clear();

            for (int o = 0; o < mySoap.Count; o++)
            {
                if (mySoap[o] != null)
                {
                    mySoap[o].SetActive(false);
                }
            }
            mySoap.Clear();

        } */

    }
}
        

    
       



