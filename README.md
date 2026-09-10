# 王震工作进度台

Windows 10/11 桌面工作进度提醒工具。

## V1 目标

- 深蓝半透明“工作指挥中心”界面
- 今日任务与总体完成度
- 自动完成进度 / 手动百分比进度
- Windows 桌面提醒 + 提示音
- 桌面模式 / 悬浮模式切换
- 每日固定任务自动生成
- 项目节点倒计时
- 临时任务快速添加
- 开机自启
- 下班一键生成“今日完成 + 明日重点”汇报
- 本地 SQLite 保存数据

## 已预置工作节点

- 每日上午：昨日销售日报
- 每日：抖音 3 条
- 每日：小红书 3 条
- 每日下班前：记录今日完成
- 2026-09-11：事业单位日不落直播
- 2026-09-15：Skill 大赛截止
- 2026-09-24：中秋专场启动

## 开发技术

- .NET 8
- WPF
- SQLite
- Windows 托盘提醒

## 本地运行

```powershell
dotnet restore
dotnet run
```

## 发布 Windows x64 便携版

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o publish
```

仓库内已配置 GitHub Actions，推送后可自动构建 Windows 便携版。