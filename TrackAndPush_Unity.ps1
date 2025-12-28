
$SIZE_LIMIT = 94371840  # 90 MB in bytes
$GITATTR = ".gitattributes"
# Unity projects need Assets, ProjectSettings, and Packages folders
$UNITY_DIRS = @("Assets", "ProjectSettings", "Packages")
$BRANCH = "main"           

if (-not (Test-Path $GITATTR)) {
    Write-Host "Creating $GITATTR file..." -ForegroundColor Cyan
    New-Item -ItemType File -Path $GITATTR -Force | Out-Null
}

# 1. Pre-track Unity-specific binary types (even if small)
# exclude .meta files from this list intentionally
Write-Host "Tracking Unity binary types with LFS..." -ForegroundColor Cyan
$unityExtensions = @("*.psd", "*.png", "*.jpg", "*.wav", "*.mp3", "*.ogg", "*.fbx", "*.asset", "*.unity", "*.prefab", "*.exr", "*.tga")
foreach ($ext in $unityExtensions) {
    git lfs track $ext | Out-Null
}

# 2. Dynamic Scan for files > 90MB (Catch-all for large atlases or video)
Write-Host "Scanning Unity directories for files > 90MB..." -ForegroundColor Yellow
$trackedCount = 0
$CurrentDir = (Get-Location).Path + "\"

foreach ($dir in $UNITY_DIRS) {
    if (Test-Path $dir) {
        Get-ChildItem -Path $dir -Recurse -File | ForEach-Object {
            # Ensure we don't track .meta files in LFS even if they are large
            if ($_.Length -ge $SIZE_LIMIT -and $_.Extension -ne ".meta") {
                $relPath = $_.FullName.Replace($CurrentDir, "").Replace('\', '/')
                $alreadyTracked = Select-String -Path $GITATTR -Pattern ([regex]::Escape($relPath)) -Quiet
                if (-not $alreadyTracked) {
                    Write-Host "Tracking large file: $relPath" -ForegroundColor Magenta
                    git lfs track "$relPath" | Out-Null
                    $trackedCount++
                }
            }
        }
    }
}

# 3. Chunked Upload Logic
$files = (git ls-files -m -o --exclude-standard)
if (-not $files) {
    Write-Host "No changes to push." -ForegroundColor Green
    exit
}

$fileArray = $files -split "`r?`n"
$chunkSize = 40  # Smaller chunks for high-res 2D assets/atlases
$totalFiles = $fileArray.Count
$chunkNum = 1

Write-Host "Starting chunked push of $totalFiles files..." -ForegroundColor Cyan

for ($i = 0; $i -lt $totalFiles; $i += $chunkSize) {
    Write-Host "--- Committing Chunk $chunkNum ---" -ForegroundColor Yellow
    git reset > $null

    for ($j = $i; $j -lt ($i + $chunkSize) -and $j -lt $totalFiles; $j++) {
        $currentFile = $fileArray[$j]
        if ($currentFile) { git add "$currentFile" }
    }

    $message = "Unity RPG Build: Chunk $chunkNum"
    git commit -m "$message"

    Write-Host "Pushing chunk $chunkNum to $BRANCH..." -ForegroundColor Green
    git push origin $BRANCH
    $chunkNum++
}