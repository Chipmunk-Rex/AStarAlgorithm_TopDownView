using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace AStarAlgorithm{
public class AStarNode
{
    public bool isCanPass;
    public Vector2Int worldPosition;
    public AStarNode parentNode;
    public int G,H,F;
    public AStarNode(bool walkAble,Vector2Int worldPosition){
        this.isCanPass = walkAble;
        this.worldPosition = worldPosition;
        
    }


    
}

public class AStar
{
    public Vector2Int buttomLeftPos,topRightPos,startPos,targetPos;
        [SerializeField] public List<Vector2Int> path;
    int aStarmapSizeX, aStarmapSizeY;
    AStarNode[,] aStarMap;
    AStarNode startNode, endNode,curNode;
    private List<AStarNode> openList,closeList;
    public void SetMap(Vector2Int mapXYMinPos, Vector2Int mapXYMaxPos){
        this.buttomLeftPos = mapXYMinPos;
        this.topRightPos = mapXYMaxPos;
        aStarmapSizeX = mapXYMaxPos.x -mapXYMinPos.x + 1;
        aStarmapSizeY = mapXYMaxPos.y -mapXYMinPos.y + 1;
        aStarMap = new AStarNode[aStarmapSizeX,aStarmapSizeY];

        for(int x = 0; x < aStarmapSizeX; x++){
            for(int y = 0; y < aStarmapSizeY; y++){
                bool isCanPass = true;
            foreach(Collider2D collider2D in Physics2D.OverlapCircleAll(new Vector2(x + mapXYMinPos.x, y + mapXYMinPos.y),0.1f)){
                if(collider2D.gameObject != null){
                    isCanPass = false;
                    }
                }   
                aStarMap[x,y] = new AStarNode(isCanPass,new Vector2Int(x + mapXYMinPos.x,y + mapXYMinPos.y));
            }
        }
    }
    
    public void SetMap(Vector2Int mapXYMinPos, Vector2Int mapXYMaxPos, string layerName){
        if(aStarMap==null){
            Debug.Log("맵 설정이 안되어있습니다. : AStarAlgorithm");
        }
        this.buttomLeftPos = mapXYMinPos;
        this.topRightPos = mapXYMaxPos;
        aStarmapSizeX = mapXYMaxPos.x -mapXYMinPos.x + 1;
        aStarmapSizeY = mapXYMaxPos.y -mapXYMinPos.y + 1;
        aStarMap = new AStarNode[aStarmapSizeX,aStarmapSizeY];

        for(int x = 0; x < aStarmapSizeX; x++){
            for(int y = 0; y < aStarmapSizeY; y++){
                bool isCanPass = true;
            foreach(Collider2D collider2D in Physics2D.OverlapCircleAll(new Vector2(x + mapXYMinPos.x, y + mapXYMinPos.y),0.1f)){
                if(collider2D.gameObject.layer == LayerMask.NameToLayer(layerName)){
                    isCanPass = false;
                    }
                }   
                aStarMap[x,y] = new AStarNode(isCanPass,new Vector2Int(x + mapXYMinPos.x,y + mapXYMinPos.y));
            }
        }
    }

    public void SetMap(Vector2Int mapXYMinPos,Vector2Int mapXYMaxPos, float objectRadius){
        this.buttomLeftPos = mapXYMinPos;
        this.topRightPos = mapXYMaxPos;
        aStarmapSizeX = mapXYMaxPos.x -mapXYMinPos.x + 1;
        aStarmapSizeY = mapXYMaxPos.y -mapXYMinPos.y + 1;
        aStarMap = new AStarNode[aStarmapSizeX,aStarmapSizeY];

        for(int x = 0; x < aStarmapSizeX; x++){
            for(int y = 0; y < aStarmapSizeY; y++){
                bool isCanPass = true;
            foreach(Collider2D collider2D in Physics2D.OverlapCircleAll(new Vector2(x + mapXYMinPos.x, y + mapXYMinPos.y),objectRadius)){
                if(collider2D.gameObject != null){
                    isCanPass = false;
                    }
                }   
                aStarMap[x,y] = new AStarNode(isCanPass,new Vector2Int(x + mapXYMinPos.x,y + mapXYMinPos.y));
            }
        }
    }
    
    public void SetMap(Vector2Int mapXYMinPos, Vector2Int mapXYMaxPos, string layerName, float objectRadius){
        this.buttomLeftPos = mapXYMinPos;
        this.topRightPos = mapXYMaxPos;
        aStarmapSizeX = mapXYMaxPos.x -mapXYMinPos.x + 1;
        aStarmapSizeY = mapXYMaxPos.y -mapXYMinPos.y + 1;
        aStarMap = new AStarNode[aStarmapSizeX,aStarmapSizeY];

        for(int x = 0; x < aStarmapSizeX; x++){
            for(int y = 0; y < aStarmapSizeY; y++){
                bool isCanPass = true;
            foreach(Collider2D collider2D in Physics2D.OverlapCircleAll(new Vector2(x + mapXYMinPos.x, y + mapXYMinPos.y),objectRadius)){
                if(collider2D.gameObject.layer == LayerMask.NameToLayer(layerName)){
                    isCanPass = false;
                    }
                }   
                aStarMap[x,y] = new AStarNode(isCanPass,new Vector2Int(x + mapXYMinPos.x,y + mapXYMinPos.y));
            }
        }
    }

    public List<Vector2Int> FindPath(Vector2Int start,Vector2Int end){
        startNode = aStarMap[start.x - buttomLeftPos.x,start.y - buttomLeftPos.y];
        endNode = aStarMap[end.x - buttomLeftPos.x, end.y - buttomLeftPos.y];
        openList = new List<AStarNode>(){startNode};
        closeList = new List<AStarNode>();
        path = new List<Vector2Int>();
        while(openList.Count > 0){
            curNode = openList[0];
            for(int openListCounter = 1; openListCounter < openList.Count; openListCounter++){
                if(openList[openListCounter].F <= curNode.F && openList[openListCounter].H < curNode.H){
                    curNode = openList[openListCounter];
                }
            }
            openList.Remove(curNode);
            closeList.Add(curNode);
                
            if(curNode == endNode){
            AStarNode curEndNode = curNode;
            while(curEndNode != startNode){
                path.Add(curEndNode.worldPosition);
                curEndNode = curEndNode.parentNode;
            }
            path.Add(startNode.worldPosition);
            path.Reverse();
            return path;
            }
                OpenListAdd(new Vector2Int(curNode.worldPosition.x + 1,curNode.worldPosition.y));
                OpenListAdd(new Vector2Int(curNode.worldPosition.x - 1,curNode.worldPosition.y));
                OpenListAdd(new Vector2Int(curNode.worldPosition.x,curNode.worldPosition.y + 1));
                OpenListAdd(new Vector2Int(curNode.worldPosition.x,curNode.worldPosition.y - 1));

        }
        return null;
    }

    private void OpenListAdd(Vector2Int nodePos){ 
        if(nodePos.x >= buttomLeftPos.x && nodePos.x <= topRightPos.x &&
           nodePos.y >= buttomLeftPos.y && nodePos.y <= topRightPos.y &&
           aStarMap[nodePos.x - buttomLeftPos.x,nodePos.y - buttomLeftPos.y].isCanPass &&
           !closeList.Contains(aStarMap[nodePos.x - buttomLeftPos.x,nodePos.y - buttomLeftPos.y])){
           
           AStarNode getNode = aStarMap[nodePos.x - buttomLeftPos.x,nodePos.y - buttomLeftPos.y];
           int movedCount = curNode.G + 1;
           if(movedCount < getNode.G||!openList.Contains(getNode)){
            getNode.G = movedCount;
            getNode.H = Mathf.Abs(nodePos.x - endNode.worldPosition.x) + Mathf.Abs(nodePos.y - endNode.worldPosition.y);
            getNode.F = getNode.G + getNode.H;
            getNode.parentNode = curNode;
            openList.Add(getNode);
           }

        }

    }
    }
}
