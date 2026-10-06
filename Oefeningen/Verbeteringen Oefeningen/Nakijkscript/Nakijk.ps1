<#
.SYNOPSIS
    Kijkt een oefening van Programming Advanced na en schrijft Nakijk_<oefening>_V#.md en .docx.

.DESCRIPTION
    Het script bouwt je project, start je API, stuurt de verzoeken uit het nakijkbestand van de
    oefening, vergelijkt de antwoorden met de opgave, zoekt bekende valkuilen in je code en in de
    uitvoer van je API, en schrijft een nakijkverslag. Het verslag bevat nooit verbeterde code.

.PARAMETER Oefening
    Het nummer van de oefening, bv. 01_03, 06_01 of TM_02_04.

.PARAMETER Project
    De map met het .csproj-bestand van je oplossing. Standaard: de map waarin je nu staat.

.PARAMETER Uitvoer
    De map voor de verslagen. Standaard: de map Nakijken naast je project.

.PARAMETER Poort
    De poort waarop het script je API start. Standaard 5199.

.PARAMETER BasisUrl
    Draait je API al (bv. vanuit Visual Studio)? Geef dan het adres mee, bv. http://localhost:5123.
    Het script bouwt en start dan niets, en kan niet herstarten of de database opnieuw opbouwen.

.PARAMETER SqlScript
    Eén of meer SQL-scripts met startgegevens (hoofdstuk 04 en 05), in de volgorde waarin ze moeten lopen.
    Zonder deze parameter voert het script alle .sql-bestanden uit je projectmap uit, in alfabetische volgorde.

.PARAMETER DockerContainer
    De naam van je PostgreSQL-container, als het script die niet zelf vindt.

.PARAMETER ZonderDocx
    Schrijf enkel het .md-verslag.

.EXAMPLE
    .\Nakijk.ps1 -Oefening 01_03 -Project C:\School\PA\Oefening_01_03
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string]$Oefening,
    [string]$Project = (Get-Location).Path,
    [string]$Uitvoer,
    [int]$Poort = 5199,
    [string]$BasisUrl,
    [string[]]$SqlScript,
    [string]$DockerContainer,
    [switch]$ZonderDocx,
    [Parameter(DontShow = $true)][string]$TestProgramma,
    [Parameter(DontShow = $true)][string]$TestArgumenten
)

$ErrorActionPreference = 'Stop'
try { [Console]::OutputEncoding = [System.Text.Encoding]::UTF8 } catch { }

$hier = $PSScriptRoot
$Project = (Resolve-Path -LiteralPath $Project).Path
if (-not $Uitvoer) { $Uitvoer = Join-Path (Split-Path -Parent $Project) 'Nakijken' }

# De motor van het script staat in de map Motor, in C#. PowerShell compileert die hier eenmalig per sessie.
if (-not ('Nakijkscript.Nakijker' -as [type])) {
    $bronnen = Get-ChildItem -LiteralPath (Join-Path $hier 'Motor') -Filter '*.cs' | ForEach-Object { $_.FullName }
    if ($PSVersionTable.PSEdition -eq 'Core') {
        Add-Type -Path $bronnen    # PowerShell 7 verwijst zelf naar alle assemblies van .NET
    }
    else {
        Add-Type -AssemblyName 'System.Net.Http', 'System.IO.Compression', 'System.IO.Compression.FileSystem'
        Add-Type -Path $bronnen -ReferencedAssemblies 'System.Core', 'System.Net.Http', 'System.IO.Compression', 'System.IO.Compression.FileSystem'
    }
}

$n = New-Object Nakijkscript.Nakijker
$n.ProjectMap = $Project
$n.Oefening = $Oefening
$n.NakijkbestandenMap = Join-Path $hier 'nakijkbestanden'
$n.CatalogusPad = Join-Path $hier 'valkuilen.json'
$n.UitvoerMap = $Uitvoer
$n.Poort = $Poort
if ($BasisUrl) { $n.BasisUrl = $BasisUrl }
if ($DockerContainer) { $n.Container = $DockerContainer }
if ($SqlScript) { foreach ($s in $SqlScript) { $n.SqlScripts.Add((Resolve-Path -LiteralPath $s).Path) } }
$n.ZonderDocx = [bool]$ZonderDocx
if ($TestProgramma) { $n.TestProgramma = $TestProgramma; $n.TestArgumenten = $TestArgumenten }
$n.Meld = [System.Action[string]] { param($t) Write-Host "  $t" -ForegroundColor DarkGray }

Write-Host "Nakijken van oefening $Oefening in $Project" -ForegroundColor Cyan
try {
    $n.Voer()
}
catch {
    $fout = $_.Exception
    while ($fout.InnerException) { $fout = $fout.InnerException }
    Write-Host "Het nakijken stopte: $($fout.Message)" -ForegroundColor Red
    exit 1
}

if ($n.Compileerfouten -gt 0) {
    Write-Host ("Je project bouwt niet: {0} compileerfout(en). Het verslag toont waar." -f $n.Compileerfouten) -ForegroundColor Red
}
elseif ($n.Totaal -eq 0) {
    Write-Host "Het script kon geen enkele stap uitvoeren. Het verslag toont waarom." -ForegroundColor Red
}
else {
    $kleur = if ($n.Juist -eq $n.Totaal) { 'Green' } else { 'Yellow' }
    Write-Host ("{0} van de {1} stappen juist." -f $n.Juist, $n.Totaal) -ForegroundColor $kleur
}
Write-Host "Verslag: $($n.VerslagMd)"
if ($n.VerslagDocx) { Write-Host "         $($n.VerslagDocx)" }
