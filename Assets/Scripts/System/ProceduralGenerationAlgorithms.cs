using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    
    public static HashSet<Vector3Int> SimpleRandomWalk(Vector3Int startPosition, int walkLength)
    {
        HashSet<Vector3Int> path = new HashSet<Vector3Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector3Int> RandomWalkCorridor(Vector3Int startPosition, int corridorLength)
    {
        List<Vector3Int> corridor = new List<Vector3Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.z >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.z >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }else if(room.size.x >= minWidth && room.size.z >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.z >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.z >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.z, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.z, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.z, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.z);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.z + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.z - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector3Int> cardinalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0,1), //UP
        new Vector3Int(1,0), //RIGHT
        new Vector3Int(0, -1), // DOWN
        new Vector3Int(-1, 0) //LEFT
    };

    public static List<Vector3Int> diagonalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(1,1), //UP-RIGHT
        new Vector3Int(1,-1), //RIGHT-DOWN
        new Vector3Int(-1, -1), // DOWN-LEFT
        new Vector3Int(-1, 1) //LEFT-UP
    };

    public static List<Vector3Int> eightDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0,1), //UP
        new Vector3Int(1,1), //UP-RIGHT
        new Vector3Int(1,0), //RIGHT
        new Vector3Int(1,-1), //RIGHT-DOWN
        new Vector3Int(0, -1), // DOWN
        new Vector3Int(-1, -1), // DOWN-LEFT
        new Vector3Int(-1, 0), //LEFT
        new Vector3Int(-1, 1) //LEFT-UP

    };

    public static Vector3Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[UnityEngine.Random.Range(0, cardinalDirectionsList.Count)];
    }
}