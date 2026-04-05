@echo off
setlocal
set "REPO_URL=https://github.com/PJPRORL/MauiIntroductie"

echo --------------------------------------------------
echo        MauiIntroductie GitHub Uploader
echo --------------------------------------------------

:: Check if git is installed
where git >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo Git is not installed or not in the PATH. Please install Git and try again.
    pause
    exit /b 1
)

:: Navigate to the script's directory (project root)
pushd "%~dp0"

:: Initialize Git if not already
if not exist ".git" (
    echo Initializing Git repository...
    git init
) else (
    echo Git repository already initialized.
)

:: Configure Remote
git remote get-url origin >nul 2>nul
if %ERRORLEVEL% equ 0 (
    echo Remote 'origin' exists. Setting URL to %REPO_URL%...
    git remote set-url origin %REPO_URL%
) else (
    echo Adding remote 'origin' - %REPO_URL%...
    git remote add origin %REPO_URL%
)

:: Add Files
echo Adding files to staging...
git add .

:: Commit
echo Committing changes...
git commit -m "Upload project via script %date% %time%"
:: We ignore the error level here because if there's nothing to commit, it returns 1, which is fine.

:: Push
echo Pushing to GitHub (Overwriting remote)...
:: We use --force to Ensure local files overwrite remote files as requested.
git push -u origin main --force
if %ERRORLEVEL% neq 0 (
    echo.
    echo --------------------------------------------------
    echo [ERROR] Failed to push to GitHub.
    echo Please check your internet connection and GitHub credentials.
    echo You may need to authenticate.
    echo --------------------------------------------------
    pause
    exit /b 1
)

echo.
echo --------------------------------------------------
echo [SUCCESS] Successfully uploaded to GitHub!
echo --------------------------------------------------
pause
popd
