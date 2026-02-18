# **TodoApp \- Onion Architecture Implementation**

ASP.NET Core Web API を用いて構築した、オニオンアーキテクチャ（Onion Architecture）実践のためのサンプルプロジェクトです。

## **🚀 プロジェクトの目的**

単なる CRUD アプリケーションの構築にとどまらず、以下の設計思想をコードで具現化することを目的としています。

* **ドメイン中心設計**: 業務ロジックを技術的な詳細（DB、外部API等）から分離し、システムの核（ドメイン）を保護する。  
* **疎結合な設計**: 依存性の逆転（DIP）を活用し、インフラストラクチャの変更がビジネスロジックに影響を与えない構造を実現する。  
* **高い保守性とテスト性**: 外部依存を切り離すことで、ドメインロジックのユニットテストを容易にする。

## **🏗 アーキテクチャ構成**

本ソリューションは、以下の 4 つの物理プロジェクトで構成されています。

### **1\. TodoApp.Domain (Core)**

* **役割**: システムの「王様（主役）」。業務ルールと知識を保持する。  
* **内容**: エンティティ、値オブジェクト、リポジトリのインターフェース。  
* **依存性**: 他のどの層にも依存しない（完全な独立）。

### **2\. TodoApp.Application (UseCases)**

* **役割**: 「料理長（指示役）」。ドメインモデルを利用して処理の流れを制御する。  
* **内容**: アプリケーションサービス、DTO、バリデーションルール。  
* **依存性**: Domain 層のみを参照。

### **3\. TodoApp.Infrastructure (Tools)**

* **役割**: 「調理器具・保存容器」。データの永続化などの技術的な詳細を担当。  
* **内容**: AppDbContext (Entity Framework Core)、リポジトリの実装、外部サービス連携。  
* **依存性**: Domain 層、Application 層を参照。

### **4\. TodoApp.Web (Presentation)**

* **役割**: 「窓口・受付」。外部からのリクエストを受け取り、レスポンスを返す。  
* **内容**: Controllers、Program.cs、ミドルウェア、Swagger/Scalar 設定。  
* **依存性**: Application 層を参照（依存性の注入のため Infrastructure も参照）。

## **🛠 技術スタック**

* **Framework**: .NET 9.0 (ASP.NET Core Web API)  
* **Database**: SQLite / Entity Framework Core  
* **Validation**: FluentValidation  
* **API Documentation**: OpenAPI / Scalar  
* **Architecture**: Onion Architecture (Clean Architecture)

## **🧠 本プロジェクトで学んだ重要な概念**

* **「王様」の独立**: レイヤードアーキテクチャ（DBが王様）とは異なり、ドメインを王様に据えることで、DBの都合で業務ルールが汚染されるのを防ぐ設計。  
* **物理的な壁（プロジェクト分割）**: フォルダ分け（論理分割）ではなくプロジェクトを分けることで、不適切な依存関係をコンパイルエラーとして検知できるようにした。  
* **依存性の逆転（DIP）**: Application 層が Infrastructure 層を直接参照せず、Domain 層のインターフェースを介して通信する仕組みの実装。  
* **PoC からの進化**: スピード優先の検証（PoC）ではなく、長期運用に耐えうる「堅牢な家づくり」としてのアーキテクチャ構築。

## **🔧 セットアップと実行**

1. リポジトリをクローン  
2. データベースの準備（EF Core Migration）  
   dotnet ef database update \--project src/TodoApp.Infrastructure \--startup-project src/TodoApp.Web

3. プロジェクトの実行  
   dotnet run \--project src/TodoApp.Web

4. API ドキュメントの確認  
   ブラウザで https://localhost:\<port\>/scalar/v1 を開く。

*Created as part of a deep dive into modern software architecture.*
