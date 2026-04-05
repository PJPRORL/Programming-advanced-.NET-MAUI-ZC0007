# Upload to GitHub Script
# This script initializes a git repo, adds files, commits, and pushes to the specified remote.

$ErrorActionPreference = "Stop"

$repoUrl = "https://github.com/PJPRORL/MauiIntroductie"
$projectDir = $PSScriptRoot

Write-Host "--------------------------------------------------" -ForegroundColor Cyan
Write-Host "       MauiIntroductie GitHub Uploader" -ForegroundColor Cyan
Write-Host "--------------------------------------------------" -ForegroundColor Cyan

# Check if git is installed
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Error "Git is not installed or not in the PATH. Please install Git and try again."
    exit 1
}

Set-Location $projectDir

# Initialize Git if not already
if (-not (Test-Path ".git")) {
    Write-Host "Initializing Git repository..." -ForegroundColor Yellow
    git init
} else {
    Write-Host "Git repository already initialized." -ForegroundColor Green
}

# Configure Remote
$remotes = git remote
if ($remotes -contains "origin") {
    Write-Host "Remote 'origin' exists. Setting URL to $repoUrl..." -ForegroundColor Yellow
    git remote set-url origin $repoUrl
} else {
    Write-Host "Adding remote 'origin' ($repoUrl)..." -ForegroundColor Yellow
    git remote add origin $repoUrl
}

# Add Files
Write-Host "Adding files to staging..." -ForegroundColor Yellow
git add .

# Commit
$status = git status --porcelain
if ($status) {
    Write-Host "Committing changes..." -ForegroundColor Yellow
    git commit -m "Upload project via script"
} else {
    Write-Host "No changes to commit." -ForegroundColor Green
}

# Push
Write-Host "Renaming branch to main..." -ForegroundColor Yellow
git branch -m main

Write-Host "Pushing to GitHub..." -ForegroundColor Yellow
try {
    git push -u origin main
    Write-Host "Successfully uploaded to GitHub!" -ForegroundColor Cyan
} catch {
    Write-Error "Failed to push to GitHub. Please check your internet connection and GitHub credentials."
    Write-Host "You may need to authenticate. Try running 'git push -u origin main' manually if this fails." -ForegroundColor Red
}

Read-Host "Press Enter to exit..."
