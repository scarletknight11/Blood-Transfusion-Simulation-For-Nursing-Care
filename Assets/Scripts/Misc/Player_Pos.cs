using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]

public class Player_Pos : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material originalMaterial;

    [SerializeField]
    private Material highlightedMaterial;
    
    static bool result;
    private GameObject player_obj = null;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();



        EnableHighlight(false);
        
    }

    public void EnableHighlight(bool onOff)
    {
        // 5
        if (meshRenderer != null && originalMaterial != null &&
            highlightedMaterial != null)
        {
            // 6
            meshRenderer.material = onOff ? highlightedMaterial : originalMaterial;
        }
    }

    private void distance_Check()
    {
        if(player_obj == null)
        {
            player_obj = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        
        Vector3 player_pos = player_obj.transform.position;
     Vector3 object_pos = this.transform.position;
    float distance = Vector3.Distance(player_pos, object_pos);

        if (distance < 20.0)
        {
            EnableHighlight(false);
        }
        else
        {
            EnableHighlight(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        distance_Check();
    }
}
