# 🍉 엉망진창 수박게임

<img width="500" alt="수박게임 플레이 화면" src="https://github.com/user-attachments/assets/3abd95c5-74cc-49ab-9680-90f5d12cc438" />

Unity로 개발한 **Android 기반 물리 퍼즐 캐주얼 게임**입니다.

떨어지는 과일을 합쳐 더 큰 과일을 만들고 점수를 획득하며 Google Play Games Services를 통해 계정 로그인과 리더보드 기능을 제공합니다.

**Factory 기반 생성 로직 분리, Object Pool을 통한 오브젝트 재사용, 모바일 화면 비율 대응**에 중점을 두었으며 Google Play 출시까지 진행한 개인 프로젝트입니다.

🎬 [플레이 영상](https://www.youtube.com/watch?v=K2gH6z2vjAY) · 📄 [개발 문서](https://drive.google.com/file/d/15Mg2I1wjyAy22UUDBt-d4SBGXm5E9URf/view?usp=sharing)

> Google Play 출시 이력이 있으나 현재는 업데이트 중단으로 스토어 노출이 해제된 상태입니다.

---

## 🗂️ 프로젝트 정보

| 항목 | 내용 |
|---|---|
| 개발 기간 | 2024.10 ~ 2024.12 |
| 개발 인원 | 1인 개발 |
| 개발 환경 | Unity 6000.0.26f1, C#, Visual Studio Code |
| 플랫폼 | Android |
| 주요 기술 | Google Play Games Services, Factory 패턴, Object Pool |
| 버전 관리 | Git, SourceTree |
| 배포 | Google Play Console |

## 💻 핵심 구현

※ 코드에서는 과일 오브젝트를 `Dongle`로 표현했습니다.

### 1. Factory 기반 동글 생성 로직 분리

동글 생성 요청부에서 개별 초기화 과정을 직접 관리하지 않도록 `DongleFactory`로 생성 및 초기화 로직을 분리했습니다.

- 생성 요청부는 동글 생성 요청만 담당
- Factory에서 동글 레벨 설정과 애니메이션 초기화 처리
- 생성 시 필요한 초기화 로직을 한곳에서 관리하도록 구성

🔗 [DongleFactory.cs](Assets/Scripts/Pattern/DongleFactory.cs)

---

### 2. Object Pool 기반 오브젝트 재사용

게임 중 반복적으로 생성되고 제거되는 동글에 Object Pool을 적용해 오브젝트를 재사용하도록 구성했습니다.

- 사용이 끝난 동글을 풀에 반환하고 필요할 때 재사용
- 반복적인 `Instantiate()` 및 `Destroy()` 호출 감소
- 오브젝트 생성과 제거에 따른 런타임 비용을 줄이도록 설계

🔗 [ObjectPool.cs](Assets/Scripts/Pattern/ObjectPool.cs)

---

### 3. Google Play Games Services 연동

별도의 게임 서버를 구축하지 않고 로그인과 점수 경쟁 기능을 제공하기 위해 Google Play Games Services를 연동했습니다.

- Google 계정 기반 로그인 구현
- 리더보드에 최고 점수 등록 및 순위 확인
- Google Play Console을 통한 Android 게임 출시

---

### 4. 모바일 화면 비율 대응

기기별 화면 비율 차이로 핵심 플레이 영역이 달라지는 문제를 줄이기 위해 카메라 크기 보정 로직을 구현했습니다.

- 화면 비율에 따른 카메라 크기 보정
- 다양한 Android 화면 비율에서 핵심 플레이 영역이 유지되도록 구성

<img width="500" height="368" alt="스크린샷 2026-10-16 오후 5 17 08" src="https://github.com/user-attachments/assets/0f632e66-4ada-4842-a82f-d067c1737aa0" />

🔗 [CameraScaler.cs](Assets/Scripts/Utility/CameraScaler.cs)
