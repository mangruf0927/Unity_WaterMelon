# 🍉 엉망진창 수박게임

## 프로젝트 소개

Unity 기반의 물리 퍼즐 과일 합성 캐주얼 게임입니다.  
떨어지는 과일을 합쳐 더 큰 과일을 만들고 점수를 획득하며 Google 계정 연동을 통해 리더보드에서 다른 플레이어와 경쟁할 수 있도록 구현했습니다.

<img width="500" alt="수박게임 스크린샷" src="https://github.com/user-attachments/assets/3abd95c5-74cc-49ab-9680-90f5d12cc438" />

## 프로젝트 정보

| 항목 | 내용 |
|---|---|
| 개발 기간 | 2024.10 ~ 2024.12 |
| 참여 인원 | 1인 |
| 개발 환경 | Unity 6000.0.26f1, C#, Visual Studio Code |
| 버전 관리 | SourceTree |
| 플랫폼 및 배포 | Android / Google Play Console |

## 실행 및 자료

- 플레이 영상 : [YouTube](https://www.youtube.com/watch?v=K2gH6z2vjAY)
- 개발 문서 : [Google Drive](https://drive.google.com/file/d/15Mg2I1wjyAy22UUDBt-d4SBGXm5E9URf/view?usp=sharing)
- 다운로드 : Google Play 출시 이력이 있으나 현재는 업데이트 중단으로 스토어 노출이 해제된 상태입니다.

## 구현 내용

### 1. 동글 생성 구조 설계

> `DongleFactory`를 통해 동글 생성 로직을 분리하고 생성 시 레벨 설정과 애니메이션 초기화가 함께 이루어지도록 구성했습니다.  
> 이를 통해 동글 생성 요청부와 실제 생성/초기화 로직을 분리했습니다.
>
> [코드 바로가기](https://github.com/mangruf0927/Unity_WaterMelon/blob/main/Assets/Scripts/Pattern/DongleFactory.cs)

### 2. Object Pool

> 반복적으로 생성되고 제거되는 동글 오브젝트에 Object Pool을 적용했습니다.  
> `Instantiate`와 `Destroy` 호출을 줄여 런타임 중 발생할 수 있는 가비지 컬렉션 비용을 최소화했습니다.
>
> [코드 바로가기](https://github.com/mangruf0927/Unity_WaterMelon/blob/main/Assets/Scripts/Pattern/ObjectPool.cs)

### 3. Google Play Game Services 연동

> Google Play Game Services를 연동해 Google 로그인과 리더보드 기능을 구현했습니다.  
> 별도 서버를 구축하지 않고도 최고 점수 저장과 순위 경쟁 기능을 제공할 수 있도록 구성했습니다.

### 4. 모바일 화면 비율 대응

> 기기별 화면 비율에 따라 카메라 크기를 보정해 핵심 플레이 영역이 유지되도록 개선했습니다.
>
> [코드 바로가기](https://github.com/mangruf0927/Unity_WaterMelon/blob/main/Assets/Scripts/Utility/CameraScaler.cs)
