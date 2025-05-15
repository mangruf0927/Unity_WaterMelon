# 🍉 엉망진창 수박게임

<img width="500" alt="스크린샷 2025-05-15 오후 3 06 22" src="https://github.com/user-attachments/assets/3abd95c5-74cc-49ab-9680-90f5d12cc438" />

Unity 기반의 **물리 퍼즐 과일 합성 캐주얼 게임**입니다.  
떨어지는 과일을 합쳐 더 큰 과일을 만들어 점수를 획득하고  
Google 계정 연동을 통해 리더보드에서 다른 플레이어와 경쟁할 수 있습니다.

유니티 버전: **Unity(6000.0.26f1)**

[🎮 플레이 영상](https://www.youtube.com/watch?v=K2gH6z2vjAY)    
[📄 개발 문서](https://drive.google.com/file/d/15Mg2I1wjyAy22UUDBt-d4SBGXm5E9URf/view?usp=sharing)

## 🔧 주요 구현 내용
- GPGS 연동으로 로그인, 점수 등록, 순위 조회 구현 *(코드 보안상 깃허브에 포함되지 않음)*    
- Object Pool로 과일, 이펙트 등의 오브젝트 재사용  
- Factory 패턴으로 생성 및 초기화 로직 정리  
- DongleCenter로 생성 과정을 일괄 관리
