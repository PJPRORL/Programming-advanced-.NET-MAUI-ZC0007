# GitHub Upload Script voor Programming advanced (ZC0007)

$repoUrl = "https://github.com/PJPRORL/.NET-MAUI.git"
$branch = "main"

Write-Host "=============================================" -ForegroundColor Cyan
Write-Host " GitHub Auto-Push Script (.NET MAUI ZC0007)" -ForegroundColor Cyan
Write-Host "=============================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Bestemming: $repoUrl"
Write-Host "Branch: $branch"
Write-Host ""

$gitExists = Get-Command git -ErrorAction SilentlyContinue
if (-not $gitExists) {
    Write-Host "Git is niet geïnstalleerd of staat niet in de systeem PATH variabele." -ForegroundColor Red
    pause
    exit
}

# Initialize repository if it doesn't exist
if (-not (Test-Path ".git")) {
    Write-Host "[1/5] Git repository initialiseren..." -ForegroundColor Yellow
    git init
    git branch -M $branch
} else {
    Write-Host "[1/5] Bestaande Git repository gevonden." -ForegroundColor Green
}

# Set or update remote origin
$remotes = git remote -v
if (-not ($remotes -match "origin")) {
    Write-Host "[2/5] Remote 'origin' toevoegen..." -ForegroundColor Yellow
    git remote add origin $repoUrl
} else {
    Write-Host "[2/5] Remote 'origin' updaten..." -ForegroundColor Yellow
    git remote set-url origin $repoUrl
}

# Add all files to staging
Write-Host "[3/5] Bestanden toevoegen aan staging..." -ForegroundColor Yellow
git add .

# Create timestamped commit
$timestamp = Get-Date -Format "dd/MM/yyyy HH:mm:ss"
$commitMsg = "Automatische upload en backup: $timestamp"

Write-Host "[4/5] Bestanden committen... ($commitMsg)" -ForegroundColor Yellow
git commit -m $commitMsg

# Push to GitHub
Write-Host "[5/5] Bestanden pushen naar GitHub (force)..." -ForegroundColor Yellow
git push -u origin $branch --force

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ Upload succesvol voltooid!" -ForegroundColor Green
} else {
    Write-Host "`n❌ Er is een fout opgetreden tijdens het pushen naar GitHub." -ForegroundColor Red
}

Write-Host ""
pause
