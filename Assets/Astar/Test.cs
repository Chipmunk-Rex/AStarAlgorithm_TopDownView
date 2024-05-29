using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Test : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            List<Vector2Int> path = Astar.FindPath(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(path == null){
                Debug.Log("길이 없습니다.");
                return;
            }
            lineRenderer.positionCount = path.Count-1;
            for(int count = 0; count < path.Count - 1; count++){
                Debug.Log("길" + path[count]);
                lineRenderer.SetPosition(count, new Vector2(path[count].x, path[count].y));
            }
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition) + " 클릭");
        }
    }
}
