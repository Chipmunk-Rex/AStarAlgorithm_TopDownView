using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar
{
    // public static List<Vector2Int> FindPath(Vector2 firstPos, Vector2 targetPos, LayerMask layerMask){
    //     Vector2Int startPos = Vector2Int.RoundToInt(firstPos);
    //     Vector2Int endPos = Vector2Int.RoundToInt(targetPos);
    //     Vector2Int currentPos;
    //     List<Vector2Int> path = new List<Vector2Int>();
    //     List<Vector2Int> openList = new List<Vector2Int>();
    //     List<Vector2Int> closeList = new List<Vector2Int>();
    //     // Dictionary<Vector2Int, AstarNode> map = new List<AstarNode>();
        
    //     // for(int x = startPos.x; x < endPos.x; x++)
    //     //     for(int y = startPos.y; y < endPos.y; y++){
    //     //         map.Add(new Vector2Int(x, y), new AstarNode(Physics2D.Raycast(new Vector2(x, y), Vector2.zero, 0.1f, layerMask).collider != null, new Vector2Int(x, y)));
    //     //     }
    //     if(Physics2D.Raycast(startPos, Vector2.zero, 0.1f, layerMask).collider != null)
    //         openList.Add(startPos);

    //     currentPos = startPos;
    //     int curG = path.Count + 1;
    //     int curH = endPos.x - currentPos.x + endPos.y - currentPos.y;
    //     int F = curG + curH; 
    //     while(path[path.Count - 1] != endPos){//마지막 위치가 도착지점이 될때까지 실행
    //         currentPos = CheckPriority(curG, curH, currentPos + new Vector2Int(1,0), currentPos, endPos);
    //         currentPos = CheckPriority(curG, curH, currentPos + new Vector2Int(-1,0), currentPos, endPos);
    //         currentPos = CheckPriority(curG, curH, currentPos + new Vector2Int(0,1), currentPos, endPos);
    //         currentPos = CheckPriority(curG, curH, currentPos + new Vector2Int(0,-1), currentPos, endPos);

    //         path.Add(currentPos);
    //     }
    //     return path;
    // }

    public static List<Vector2Int> FindPath(Vector2 firstPos, Vector2 targetPos){
        AstarNode startNode = new AstarNode(Vector2Int.RoundToInt(firstPos));
        AstarNode endNode = new AstarNode(Vector2Int.RoundToInt(targetPos));
        AstarNode currentNode;
        List<Vector2Int> path = new();
        List<AstarNode> openList = new();
        List<AstarNode> closeList = new();
        // if(Physics2D.Raycast(startPos.position, Vector2.zero, 0.1f).collider != null)    

            openList.Add(startNode);
        currentNode = startNode;
        path.Add(currentNode.position);
        int x = 0;
        while(openList.Count > 0){//마지막 위치가 도착지점이 될때까지 실행
            currentNode = openList[0];
            foreach(AstarNode node in openList){
                if(currentNode.G +currentNode.H >= node.G + node.H &&currentNode.H > node.H)
                    currentNode = node;
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);


            Debug.Log(currentNode.G + " G");

            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, 1), endNode, ref openList, closeList);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, -1), endNode, ref openList, closeList);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(1, 0), endNode, ref openList, closeList);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(-1, 0), endNode, ref openList, closeList);

            if(currentNode.position == endNode.position) break;

            x++;
            if(x>100){
                Debug.Log("중간탈출!");
            return path;
            } 

            Debug.Log(openList.Count);
        }

        AstarNode endCurNode = currentNode;
        while(endCurNode != startNode) {
            path.Add(endCurNode.position);
            endCurNode = endCurNode.parentNode;
        }
        path.Add(startNode.position);
        path.Reverse();
        return path;
    }
    static ref List<AstarNode> AddOpenList(AstarNode currentNode, Vector2Int checkPos, AstarNode endNode, ref List<AstarNode> openList, List<AstarNode> closeList){
        AstarNode newNode = new AstarNode(checkPos);
        if(Physics2D.Raycast(checkPos, Vector2.zero, 0.1f).collider == null)
            if(!closeList.Contains(newNode)){
                int G = currentNode.G + 1;
                int H = Mathf.Abs(endNode.position.x - checkPos.x) + Mathf.Abs(endNode.position.y - checkPos.y);
                newNode.G = G;
                newNode.H = H;
                newNode.parentNode = currentNode;
                openList.Add(newNode);
            }
        return ref openList;
    }
    static bool CheckPriority(AstarNode currentPos, AstarNode checkPos){
        Debug.Log($"G : {checkPos.G} H : {checkPos.H} currentPos.G : {currentPos.G} currentPos.H : {currentPos.H}");
        Debug.Log("bool " + (currentPos.F >= checkPos.F && currentPos.H < checkPos.H));
        if(currentPos.G + currentPos.H >= checkPos.G + checkPos.H && currentPos.H > checkPos.H)
            return true;
        else
            return false;
    }
}
