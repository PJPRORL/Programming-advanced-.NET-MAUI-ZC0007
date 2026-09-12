@echo off
setlocal

echo ----------------------------------------
echo       GitHub Auto-Uploader
echo ----------------------------------------

:: 1. Initialize Git if needed
if not exist ".git" (
    echo [INFO] Initializing new Git repository...
    git init
)

:: 2. Configure Remote (if not exists)
git remote get-url origin >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo [INFO] Setting remote origin...
    git remote add origin https://github.com/PJPRORL/MauiOefeningen2
)

:: 3. Add all files
echo [INFO] Adding files...
git add .

:: 4. Commit changes
git diff-index --quiet HEAD --
if %ERRORLEVEL% NEQ 0 (
    echo [INFO] Committing changes...
    git commit -m "Auto-update: %date% %time%"
) else (
    echo [INFO] No changes to commit.
)

:: 5. Push to GitHub
echo [INFO] Ensuring local branch is named 'main'...
git branch -M main

echo [INFO] Pulling remote changes (Keeping local files on conflict)...
git pull origin main --allow-unrelated-histories --no-rebase -s recursive -X ours --no-edit

echo [INFO] Pushing to GitHub...
git push -u origin main

echo.
echo ----------------------------------------
echo       Done!
echo ----------------------------------------
pause
