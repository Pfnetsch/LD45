using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Maps 
{
    public Maps() { }

    public void CreateNewStartMap()
    {
        LoadMapFromCSV();
    }

    public uint getTileAtPosition(int x, int y)
    {
        if ( (x > 50) || (y > 50) ||
             (x < 0 ) || (y < 0 )    )
        {
            Debug.Log("Cannot access this coordinates of the map!");
            return 0;
        }

        return startMap[x,y];
    }

    private uint[,] startMap = new uint[50, 50]; // create two dimensional array for hex map

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

    void GenerateRandomStones(uint numOfStones)
    {

    }
}
