$excludedFiles = @(".NETCoreApp,Version=v7.0.AssemblyAttributes.cs", "Blackjack.AssemblyInfo.cs", "Blackjack.GlobalUsings.g.cs")

$codeFiles = Get-ChildItem -Recurse -Filter *.cs | Where-Object { $_.Name -notin $excludedFiles }
$codeString = ""

foreach ($file in $codeFiles) {
    $fileContent = Get-Content $file.FullName -Raw
    $codeString += "// File: $($file.Name)`r`n$fileContent`r`n`r`n"
}

$codeString | Set-Clipboard
