## 🍉 엉망진창 수박게임

<img width="500" alt="수박게임 스크린샷" src="https://github.com/user-attachments/assets/3abd95c5-74cc-49ab-9680-90f5d12cc438" />

개발 기간 : 2024.10 ~ 2024.12 (개인 프로젝트)  
개발 도구 : Unity(6000.0.26f1), C#, SourceTree, Google Play Console, Visual Studio Code


### [프로젝트 개요]  
Unity 기반의 물리 퍼즐 과일 합성 캐주얼 게임입니다.  
떨어지는 과일을 합쳐 더 큰 과일을 만들고 점수를 획득합니다.  
Google 계정 연동을 통해 리더보드에서 다른 플레이어와 경쟁할 수 있습니다.  


### [실행 및 자료]  
- 플레이 영상 : [YouTube](https://www.youtube.com/watch?v=K2gH6z2vjAY)  
- 개발 문서 : [Google Drive](https://drive.google.com/file/d/15Mg2I1wjyAy22UUDBt-d4SBGXm5E9URf/view?usp=sharing)  
- 다운로드 : [Google Play](https://play.google.com/store/apps/details?id=com.fffgames.watermelon&hl=ko&gl=kr)  


### [주요 구현 내용]  
- GPGS(Google Play Game Services) 연동으로 Google 로그인 및 리더보드 시스템을 구현하여 서버 없이도 로그인과 순위 경쟁 기능을 제공
- Object Pool을 적용해 Instantiate와 Destroy 반복을 최소화하여 가비지 컬렉션 비용을 줄이고 성능을 최적화
- 팩토리 패턴으로 생성 로직을 통합 관리하여 코드 중복을 최소화하고 유지보수성을 강화
- DongleCenter를 도입해 동글 생성 과정을 일관되게 관리해 코드 중복을 줄이고 새로운 오브젝트 추가 시 확장이 용이하도록 개선
