$directory = "D:\VS\Personal\Blackjack"
$directoryStructure = Get-ChildItem -Recurse $directory | Select-Object FullName

$directoryStructureString = ""
foreach ($item in $directoryStructure) {
    $directoryStructureString += $item.FullName + "`n"
}

$directoryStructureString | Set-Clipboard