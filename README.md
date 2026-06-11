# 점프맵 게임

## 프로젝트 소개
플레이어가 점프를 이용하여 장애물을 피하고 맵에 배치된 모든 아이템을 수집하는 3D 점프맵 게임입니다.

---

## 조작 방법

- W : 전진
- A : 왼쪽 이동
- S : 후진
- D : 오른쪽 이동
- Space : 점프

---

## 구현 기능

### 1. 플레이어 이동

Unity Input System을 사용하여 키보드 입력을 처리하였습니다.

Vector3와 normalized를 사용하여 이동 방향을 계산하고 플레이어를 이동시켰습니다.

### 2. 점프 기능

Raycast를 사용하여 바닥 여부를 확인하고, 플레이어가 지면에 있을 때만 점프할 수 있도록 구현하였습니다.

### 3. 장애물 감지 및 회전

Vector3.Dot을 사용하여 플레이어의 시야각(FOV) 내에 장애물이 존재하는지 판별하였습니다.

Quaternion.LookRotation과 Quaternion.Slerp를 사용하여 장애물 방향으로 부드럽게 회전하도록 구현하였습니다.

### 4. 거리 판정

sqrMagnitude를 사용하여 플레이어와 장애물 사이의 거리를 계산하였습니다.

장애물이 일정 거리 이내로 접근하면 감지하도록 구현하였습니다.

### 5. 아이템 획득

OnTriggerEnter 물리 이벤트를 사용하여 플레이어가 아이템과 접촉했을 때 아이템을 획득하도록 구현하였습니다.

아이템 획득 시 점수가 증가하며 해당 아이템은 삭제됩니다.

### 6. 게임 매니저

GameManager를 통해 다음 기능을 관리합니다.

- 점수 표시
- 제한 시간 표시
- 게임 클리어
- 게임 오버
- 낙사 메시지 출력

### 7. 노드 기반 경로 이동

노드(Node)를 이용하여 장애물이 지정된 경로를 따라 이동하도록 구현하였습니다.

Node1과 Node2 사이를 반복적으로 왕복 이동하며 움직이는 장애물 역할을 수행합니다.

---

## 자료구조 사용

### List

```csharp
public List<GameObject> items = new List<GameObject>();
```

아이템의 생성 및 삭제가 자주 발생하므로 List를 사용하여 관리하였습니다.

### Queue

```csharp
private Queue<Transform> pathQueue = new Queue<Transform>();
```

노드를 순서대로 관리하기 위해 Queue 자료구조를 사용하였습니다.

FIFO(First In First Out) 구조를 통해 경로를 순차적으로 방문할 수 있습니다.

---

## 사용 기술

- Unity 6
- C#
- Input System
- Rigidbody
- Collider / Trigger
- Raycast
- Vector3
- Quaternion
- List
- Queue

---

## 실행 화면

플레이어는 점프를 이용하여 장애물을 피하고 모든 아이템을 수집하여 게임을 클리어할 수 있습니다.