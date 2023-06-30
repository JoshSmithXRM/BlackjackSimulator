$directory = "D:\VS\BlackjackSimulator\src"

$directoryStructure = Get-ChildItem -Recurse $directory -File |
    Where-Object {
        $_.DirectoryName -notlike "*\bin*" -and $_.DirectoryName -notlike "*\obj*"
    } |
    Select-Object -ExpandProperty FullName

$directoryStructure | Set-Clipboard
