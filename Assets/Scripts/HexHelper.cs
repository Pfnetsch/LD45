using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class HexHelper
    {
        static Vector3Int[] cube_directions =
        {
            new Vector3Int(+1, -1, 0), 
            new Vector3Int(+1, 0, -1), 
            new Vector3Int(0, +1, -1), 
            new Vector3Int(-1, +1, 0), 
            new Vector3Int(-1, 0, +1), 
            new Vector3Int(0, -1, +1)
        };
        
        public static Vector3Int axialToCube(Vector2Int pos)
        {
            return new Vector3Int(pos.x, -pos.x - pos.y, pos.y);
        }

        public static Vector2Int cubeToAxial(Vector3Int pos)
        {
            return new Vector2Int(pos.x, pos.z);
        }

        public static Vector3Int offsetToCube(Vector2Int pos)
        {
            int col = pos.x;
            int row = pos.y;
            int x = col - (row + (row & 1)) / 2;
            int z = row;
            int y = -x - z;
            
            return new Vector3Int(x, y, z);
            //return new Vector3Int(pos.x, - pos.y - pos.x, pos.y);
        }

        public static Vector2Int cubeToOffset(Vector3Int pos)
        {
            int col = pos.x + (pos.z + (pos.z & 1)) / 2;
            int row = pos.z;
            
            return new Vector2Int(col, row);
            //return new Vector2Int(pos.x , pos.z);
        }

        public static Vector3Int cubeDirection(int direction)
        {
            return cube_directions[direction];
        }


        public static List<Vector3Int> cubeRing(Vector3Int center, int radius)
        {
            List<Vector3Int> results = new List<Vector3Int>();

            Vector3Int cube = center + cubeDirection(4); //* radius;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    results.Add(cube);
                    cube = cubeNeighbour(cube, i);
                }
            }
            
            return results;
        }
        
        public static Vector3Int cubeNeighbour(Vector3Int pos, int direction)
        {
            return pos + cubeDirection(direction);
        }

        public static List<Vector3Int> cubeSpiral(Vector3Int center, int radius)
        {
            List<Vector3Int> results = new List<Vector3Int>();
            //results.Add(center);
            for (int k = 0; k <= radius; k++)
            {
                results.AddRange(cubeRing(center, k));
            }

            return results;
        }
    }
}