using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AstarNode
{
    public bool canPass;
    public AstarNode parentNode;
    public Vector2Int position;
    public int F{
        get{ return G + H; }
    }
    public int G;
    public int H;
    public AstarNode(Vector2Int position){
        // this.canPass = canPass;
        this.position = position;
    }
}
public static class Astar
{
    public static List<Vector2Int> FindPath(Vector2 firstPos, Vector2 targetPos){
        Debug.Log(targetPos);
        AstarNode startNode = new AstarNode(Vector2Int.RoundToInt(firstPos));
        AstarNode endNode = new AstarNode(Vector2Int.RoundToInt(targetPos));
        AstarNode currentNode;
        List<Vector2Int> path = new();
        List<AstarNode> openList = new();
        List<AstarNode> closeList = new();
        // if(Physics2D.Raycast(startPos.position, Vector2.zero, 0.1f).collider != null)    

        openList.Add(startNode);
        currentNode = startNode;
        int x = 0;
        while(openList.Count > 0){//마지막 위치가 도착지점이 될때까지 실행
            currentNode = openList[0];
            foreach(AstarNode node in openList){
                if(currentNode.G +currentNode.H >= node.G + node.H &&currentNode.H > node.H)
                    currentNode = node;
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, 1), endNode, ref openList, closeList);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, -1), endNode, ref openList, closeList);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(1, 0), endNode, ref openList, closeList);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(-1, 0), endNode, ref openList, closeList);

            if(currentNode.position == endNode.position) break;

            x++;
            if(x>100){
                return null;
                // break;
                // throw new Exception("오류 발생 (100회 이상 반복됨)");
            } 
        }

        AstarNode endCurNode = currentNode;
        while(endCurNode.parentNode != null) {
            path.Add(endCurNode.position);
            endCurNode = endCurNode.parentNode;
        }
        path.Add(startNode.position);
        path.Reverse();
        Debug.Log(path.Count);
        return path;
    }
    public static List<Vector2Int> FindPath(Vector2 firstPos, Vector2 targetPos, LayerMask layerMask){
        AstarNode startNode = new AstarNode(Vector2Int.RoundToInt(firstPos));
        AstarNode endNode = new AstarNode(Vector2Int.RoundToInt(targetPos));
        AstarNode currentNode;
        List<Vector2Int> path = new();
        List<AstarNode> openList = new();
        List<AstarNode> closeList = new();
        // if(Physics2D.Raycast(startPos.position, Vector2.zero, 0.1f).collider != null)    

        openList.Add(startNode);
        currentNode = startNode;
        int x = 0;
        while(openList.Count > 0){//마지막 위치가 도착지점이 될때까지 실행
            currentNode = openList[0];
            foreach(AstarNode node in openList){
                if(currentNode.G +currentNode.H >= node.G + node.H &&currentNode.H > node.H)
                    currentNode = node;
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, 1), endNode, ref openList, closeList, layerMask);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, -1), endNode, ref openList, closeList, layerMask);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(1, 0), endNode, ref openList, closeList, layerMask);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(-1, 0), endNode, ref openList, closeList, layerMask);

            if(currentNode.position == endNode.position) break;

            x++;
            if(x>100){
                return null;
                // break;
                // throw new Exception("오류 발생 (100회 이상 반복됨)");
            }
        }

        AstarNode endCurNode = currentNode;
        while(endCurNode.parentNode != null) {
            path.Add(endCurNode.position);
            endCurNode = endCurNode.parentNode;
        }
        path.Add(startNode.position);
        path.Reverse();
        Debug.Log(path.Count);
        return path;
    }
    public static List<Vector2Int> FindPathNear(Vector2 firstPos, Vector2 targetPos){
        AstarNode startNode = new AstarNode(Vector2Int.RoundToInt(firstPos));
        AstarNode endNode = new AstarNode(Vector2Int.RoundToInt(targetPos));
        AstarNode currentNode;
        List<Vector2Int> path = new();
        List<AstarNode> openList = new();
        List<AstarNode> closeList = new();

        openList.Add(startNode);
        currentNode = startNode;
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

            if(Mathf.Abs(currentNode.position.x - endNode.position.x) <= 1 && Mathf.Abs(currentNode.position.y - endNode.position.y) <= 1) break;

            x++;
            if(x>100){
                return null;
                // throw new Exception("오류 발생 (100회 이상 반복됨)");
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
    public static List<Vector2Int> FindPathNear(Vector2 firstPos, Vector2 targetPos, LayerMask layerMask){
        AstarNode startNode = new AstarNode(Vector2Int.RoundToInt(firstPos));
        AstarNode endNode = new AstarNode(Vector2Int.RoundToInt(targetPos));
        AstarNode currentNode;
        List<Vector2Int> path = new();
        List<AstarNode> openList = new();
        List<AstarNode> closeList = new();

        openList.Add(startNode);
        currentNode = startNode;
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

            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, 1), endNode, ref openList, closeList, layerMask);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(0, -1), endNode, ref openList, closeList, layerMask);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(1, 0), endNode, ref openList, closeList, layerMask);
            AddOpenList(currentNode, currentNode.position + new Vector2Int(-1, 0), endNode, ref openList, closeList, layerMask);

            if(Mathf.Abs(currentNode.position.x - endNode.position.x) <= 1 && Mathf.Abs(currentNode.position.y - endNode.position.y) <= 1) break;

            x++;
            if(x>100){
                return null;
                // throw new Exception("오류 발생 (100회 이상 반복됨)");
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
    static ref List<AstarNode> AddOpenList(AstarNode currentNode, Vector2Int checkPos, AstarNode endNode, ref List<AstarNode> openList, List<AstarNode> closeList, LayerMask layerMask){
        AstarNode newNode = new AstarNode(checkPos);
        if(Physics2D.Raycast(checkPos, Vector2.zero, 0.1f, layerMask).collider == null)
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
