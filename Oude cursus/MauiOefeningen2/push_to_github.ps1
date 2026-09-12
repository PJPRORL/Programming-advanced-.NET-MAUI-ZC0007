$ErrorActionPreference = "Stop"

Write-Host "Starting GitHub Setup..." -ForegroundColor Cyan

# Check for .gitignore
if (-not (Test-Path ".gitignore")) {
    Write-Warning ".gitignore not found! Please ensure it exists to avoid uploading bin/obj folders."
    # Optional: Create it here if missing, but we expect it to be created by the agent.
} else {
    Write-Host ".gitignore found." -ForegroundColor Green
}

# Initialize Git
if (-not (Test-Path ".git")) {
    Write-Host "Initializing Git repository..."
    git init
} else {
    Write-Host "Git repository already initialized." -ForegroundColor Yellow
}

# Add all files
Write-Host "Adding files..."
git add .

# Commit
$gitStatus = git status --porcelain
if ($gitStatus) {
    Write-Host "Committing files..."
    git commit -m "Initial commit of MAUI exercises"
} else {
    Write-Host "Nothing to commit." -ForegroundColor Yellow
}

# Add Remote
$remoteUrl = "https://github.com/PJPRORL/MauiOefeningen2"
$startErrorAction = $ErrorActionPreference
$ErrorActionPreference = "Continue" # Don't stop if remote doesn't exist yet
$currentRemote = git remote get-url origin 2>$null
$ErrorActionPreference = $startErrorAction

if (-not $currentRemote) {
    Write-Host "Adding remote origin: $remoteUrl"
    git remote add origin $remoteUrl
} elseif ($currentRemote -ne $remoteUrl) {
    Write-Warning "Remote origin already exists but points to $currentRemote. Updating to $remoteUrl"
    git remote set-url origin $remoteUrl
} else {
    Write-Host "Remote origin already correctly configured." -ForegroundColor Green
}

# Push
Write-Host "Pushing to GitHub..." -ForegroundColor Cyan
try {
    git push -u origin master
    Write-Host "Successfully pushed to GitHub!" -ForegroundColor Green
} catch {
    Write-Error "Failed to push. Please ensure:"
    Write-Error "1. The repository '$remoteUrl' exists on GitHub."
    Write-Error "2. You have permissions to write to it."
    Write-Error "3. If it's your first time, you might need to sign in."
    Write-Host "Trying again..."
    git push -u origin main # Sometimes default branch is master
}
