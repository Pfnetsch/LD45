using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Maps : MonoBehaviour
{
    public uint[,] startMap = new uint[50,50]; // create two dimensional array for hex map

    void Start()
    {
        LoadMapFromCSV();
    }

    private void LoadMapFromCSV()
    {
        StreamReader strReader = new StreamReader("Map_Matrix.csv");
        bool endOfFile = false;

        int row = 0;
        int col = 0;

        while (!endOfFile)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }

            var dataValues = dataString.Split(';');

            for (col = 0; col < dataValues.Length; col++)
            {
                uint.TryParse(dataValues[col], out startMap[row, col]);
            }

            row++;
        }

        Debug.Log(startMap.Length);
    }

    // Update is called once per frame
    void BuildToHexMap()
    {
        
    }

    void GenerateRandomStones(uint numOfStones)
    {

    }
}
