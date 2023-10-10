using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{

    public Tilemap map;
    public TileBase tile;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(map.origin);
        Debug.Log(map.size);
        map.SetTile(new Vector3Int(0,0,0),tile);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
