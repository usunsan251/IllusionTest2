# IllusionTest2
## 📏 V-H Illusion Measurer (Technical Detail)

知覚情報科学の知見に基づき、垂直水平錯覚（V-H錯覚）を定量化するための精密測定ツールです 。
単なる結果の記録にとどまらず、Unityのフレームワークを活かした**「知覚の探索プロセス」の可視化**を目的として設計されています 。

## 🧠 Application Concept
人間が垂直な線を水平な線よりも長く感じてしまう性質（V-H錯覚）を利用し、被験者が「物理的に同じ長さ」ではなく「主観的に同じ長さ」と感じるポイントを特定します 。探索の可視化: 調整中に「一度長くしてから少し戻す」といった被験者の特有の挙動を、時系列データから分析可能です 。高精度な測定: 10 cmの基準に対し、0.5 cm刻み（5%ステップ）での緻密な調整に対応しています 。

## 🛠 Tech Stack Details
### Architecture

- Engine: Unity 

- Input System: UnityEngine.InputSystem

- Data Handling: System.IO (Stream Writing)

### Key Mechanics

- Real-time Ratio Calculation: 操作中の長さを基準値で除算し、リアルタイムで「錯覚倍率」を算出します 。

- State Management: isConfirmed フラグによるシーケンス制御を行い、データ確定後の誤操作を防止します。

- Dynamic Overlay: OnGUI を使用し、実験者に必要な情報をコードから直接画面に描画します。

## 🚀 Measurement Protocols
2つの異なる視覚条件を個別のスクリプトで制御しています。
### Pattern A: Horizontal Adjustment (ScaleChanger.cs)
- 配置: 垂直線の下に水平線を設置 。
- タスク: 垂直線（固定）に合わせて、水平線の長さを左右キーで調整 。
- 目的: 基準が「垂直」にある場合の水平方向の知覚特性を測定 。
### Pattern B: Vertical Adjustment (ScaleChanger2.cs)
- 配置: 水平線の横に垂直線を設置 。
- タスク: 水平線（固定）に合わせて、垂直線の長さを上下キーで調整 。
- 目的: 基準が「水平」にある場合の垂直方向の知覚特性を測定 。

## 📊 Data Schema
出力されるCSV（ScaleLog.csv / ScaleLog2.csv）は、以下の構造で保存されます 。
| Column | Type | Description |
|--------|------|-------------|
| Time(s) | float | 実験開始からの経過時間（秒） |
| Scale | float | 調整された直線の物理的な長さ | 
| Ratio | float | 基準（10 cm）に対する現在の倍率  |
| Event | string | 操作内容（Initial / Increase / Decrease / CONFIRMED） |

## 使用方法
Application(アプリケーション)フォルダからIllusionTest2.exeを起動してください。
