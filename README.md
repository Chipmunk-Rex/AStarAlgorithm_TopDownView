# AStarAlgorithm_TopDownView
유니티 2D 탑다운뷰 게임에 적용 가능한 AStar 알고리즘 코드.

설명 : 

    시작지점, 도착지점을 입력받고 시작지점에서 도착지점으로 이동하는 가장 짧은 경로를 찾아준다.

    충돌감지를 하여 충돌되는 위치는 이동경로에 포함되지 않는다.

    이 코드는 이동할 길의 좌표를 List<Vector2Int>형태로 반환해준다

    그 좌표에 따라 움직는 코드는 직접 구현해야한다.(DM보내면 만들어줌)


사용방법 : 

    1. AStarAlgorithm.cs 파일을 다운받고 Unity 프로젝트에 넣어준다.

    2. 경로를 구할 코드에 using AStarAlgorithm;을 적는다

    3. AStar (원하는 변수이름) = new AStar();로 AStar클래스 객체 생성

    4. 맵 설정 방법
      SetMap 메소드 사용
        사용방법 :
            (3번에서 생성한 변수이름).SetMap(맵의 왼쪽 끝 좌표,맵의 오른쪽 위끝 좌표)

    5. 길찾기 방법
      FindPath 메소드 사용
        사용방법 :
         (3번에서 생성한 변수이름).FindPath(출발좌표,도착좌표)

    6. 추가 설명
        1. FindPath()는 List<Vector2Int>를 반환한다.

        2.  리스트의 첫번째값에는 출발좌표
            리스트의 첫번째부터 마지막 사이는 순서대로 시작위치부터 도착위치까지 가는 길의 좌표
            리스트의 마지막값에는 도착좌표 가 들어있다

주의사항 : 

    도착지점에 도달할 수 있는 길이 없다면 Null을 반환한다.
    만약 벽 위치가 바뀌었다면 SetMap 메소드를 다시 실행해줘야 제대로 길을 찾아준다
    SetMap메소드에 맵 크기를 너무 크게 넣어주면 실행 속도가 느려질 수 있다.


                        사용방법이 이해가 안되거나 질문이 있다면 디스코드 닉네임 rexgood11로 DM보내주세요.