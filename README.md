# FrokBoard

FrokBoard는 .NET 9.0, ASP.NET Core, .NET Aspire를 기반으로 개발된 게시판 시스템입니다. 이 프로젝트는 Inflearn 강의 "스프링부트로 직접 만들면서 배우는 대규모 시스템 설계 - 게시판"의 ASP.NET Core 포팅 버전으로, 대규모 시스템 설계 패턴을 학습하고 실습하는 것을 목표로 합니다.

## 기술 스택
- 🚀 **.NET 9.0**
- 🌐 **ASP.NET Core**
- 🏗 **.NET Aspire**

## 디렉토리 구조
```
FrokBoard
|- 📜 .gitignore
|- 📂 FrokBoard.sln
|- 📄 README.md
|- 📁 Article
  |- 📄 Pages
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 ArticleRead
  |- 📄 Pages
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 BoardCommon
|- 📁 Comment
  |- 📄 Pages
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 HotArticle
  |- 📄 Pages
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 Like
  |- 📄 Pages
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 View
  |- 📄 Pages
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 FrokBoard.AppHost
  |- ⚙️ appsettings.json
  |- 🏗 Program.cs
|- 📁 FrokBoard.ServiceDefaults
  |- 🔧 Extension.cs
```

## 주요 모듈 설명
### 1. 📝 **Article**
게시글 작성을 담당하는 모듈로, 게시글 등록 및 수정 기능을 포함합니다.

### 2. 🔍 **ArticleRead**
게시글 조회 기능을 제공하는 모듈입니다.

### 3. ⚙️ **BoardCommon**
게시판 공통 기능을 제공하는 모듈로, 공통 유틸리티 및 서비스가 포함됩니다.

### 4. 💬 **Comment**
게시글에 대한 댓글 기능을 담당하는 모듈입니다.

### 5. 🔥 **HotArticle**
인기 게시글을 조회하는 기능을 담당하는 모듈입니다.

### 6. 👍 **Like**
게시글 및 댓글에 대한 좋아요 기능을 제공하는 모듈입니다.

### 7. 🎨 **View**
게시판의 프론트엔드 역할을 담당하는 모듈로, ASP.NET Core Razor Pages 기반으로 구현됩니다.

### 8. 🏠 **FrokBoard.AppHost**
서비스 전체를 호스팅하는 역할을 합니다.

### 9. 🛠 **FrokBoard.ServiceDefaults**
공통 서비스 설정을 제공하는 모듈입니다.

## 실행 방법
### 1. 📥 프로젝트 클론
```sh
git clone https://github.com/FroKCreativeTM/FrokBoard.git
cd FrokBoard
```

### 2. ▶️ 실행 명령어
```sh
dotnet run --project FrokBoard.AppHost
```