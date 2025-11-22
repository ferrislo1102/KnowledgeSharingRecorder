# GitHub 上傳指南 / GitHub Upload Guide

## 步驟 1: 在 GitHub 上創建新倉庫

1. 登入 GitHub (https://github.com)
2. 點擊右上角的 **"+"** 按鈕，選擇 **"New repository"**
3. 填寫倉庫資訊：
   - **Repository name**: `KnowledgeSharingRecorder` (或您喜歡的名稱)
   - **Description**: "Windows desktop app for recording knowledge-sharing posts with URLs and timestamps"
   - **Visibility**: 選擇 Public 或 Private
   - **不要**勾選 "Initialize this repository with a README" (我們已經有 README 了)
4. 點擊 **"Create repository"**

## 步驟 2: 連接本地倉庫到 GitHub

在終端機中執行以下命令（將 `YOUR_USERNAME` 替換為您的 GitHub 用戶名）：

```powershell
git remote add origin https://github.com/YOUR_USERNAME/KnowledgeSharingRecorder.git
```

或者如果您使用 SSH：

```powershell
git remote add origin git@github.com:YOUR_USERNAME/KnowledgeSharingRecorder.git
```

## 步驟 3: 推送代碼到 GitHub

```powershell
git branch -M main
git push -u origin main
```

如果這是第一次推送，GitHub 可能會要求您輸入用戶名和密碼（或個人訪問令牌）。

## 使用個人訪問令牌 (Personal Access Token)

如果使用 HTTPS 且需要認證：

1. 前往 GitHub Settings → Developer settings → Personal access tokens → Tokens (classic)
2. 生成新令牌，選擇 `repo` 權限
3. 使用令牌作為密碼進行推送

## 完成！

推送完成後，您的代碼將在 GitHub 上可見。您可以：
- 在 GitHub 網頁上查看代碼
- 分享倉庫連結
- 繼續使用 `git push` 推送未來的更新

---

## Alternative: 使用 GitHub CLI (gh)

如果您已安裝 GitHub CLI，可以使用更簡單的方式：

```powershell
gh repo create KnowledgeSharingRecorder --public --source=. --remote=origin --push
```

