using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chunk_Manager : MonoBehaviour
{
    public float size = 4; // use mult of 400;

    public GameObject chunk;
    public GameObject terrUi;
    public GameObject territoryPanel;


    // Start is called before the first frame update
    void Start()
    {
        int chunkSize = chunk.GetComponent<Chunk>().size;

        float rowcol = Mathf.Sqrt(size);

        float xStart = transform.position.x - chunkSize * rowcol;
        float zStart = transform.position.z - chunkSize * rowcol;
        float yStart = transform.position.y;

        GameObject temp;

        for (int j = 4; j < 13; j++)
        {   
            for (int i = 0; i < rowcol; i++)
            {
                chunk.name = "chunk " + i + " " + j;
                Vector3 position = new Vector3(xStart + chunkSize + i * chunkSize*2, yStart , zStart + chunkSize + j * chunkSize*2);
                temp = Instantiate(chunk, position,transform.rotation);
                temp.transform.parent = gameObject.transform;

                GameObject tempUi = Instantiate(terrUi);
                tempUi.transform.SetParent(territoryPanel.transform, false);
                tempUi.GetComponent<territoryUI>().territory = temp;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


}
