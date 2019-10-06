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

    public uint GetTileAtPosition(int x, int y)
    {
        if ( (x > 50) || (y > 50) ||
             (x < 0 ) || (y < 0 )    )
        {
            Debug.Log("Cannot access this coordinates of the map!");
            return 0;
        }
        else
        {
            return startMap[x, y];
        }
    }

    private uint[,] startMap = new uint[50, 50]; // create two dimensional array for hex map

    private void LoadMapFromCSV()
    {
        // read csv file
        StreamReader strReader = new StreamReader("Map_Matrix.csv");
        bool endOfFile = false;

        int row = 0;
        int col = 0;

        while (!endOfFile)
        {
            // read one line into a string
            string dataString = strReader.ReadLine();
            if (dataString == null) // eof
            {
                endOfFile = true;
                break;
            }

            var dataValues = dataString.Split(';');

            for (col = 0; col < dataValues.Length; col++)
            {
<<<<<<< Updated upstream
                // values are written into 2D-map-array, note that y-axis is inverted as in csv file
=======
>>>>>>> Stashed changes
                uint.TryParse(dataValues[col], out startMap[col, row]);
            }

            row++;
        }

        Debug.Log(startMap.Length);
    }
}
