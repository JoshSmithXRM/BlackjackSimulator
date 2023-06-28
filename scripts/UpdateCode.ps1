$codeOutput = @"
"@

$codeFiles = @{
    "D:\VS\Personal\Blackjack\src\CompositionRoot.cs" = "CompositionRoot.cs"
    "D:\VS\Personal\Blackjack\src\GlobalUsings.cs" = "GlobalUsings.cs"
    "D:\VS\Personal\Blackjack\src\Program.cs" = "Program.cs"
    "D:\VS\Personal\Blackjack\src\Enums\PlayerAction.cs" = "PlayerAction.cs"
    "D:\VS\Personal\Blackjack\src\Enums\Rank.cs" = "Rank.cs"
    "D:\VS\Personal\Blackjack\src\Enums\Suit.cs" = "Suit.cs"
    "D:\VS\Personal\Blackjack\src\Interfaces\ICard.cs" = "ICard.cs"
    "D:\VS\Personal\Blackjack\src\Interfaces\IHand.cs" = "IHand.cs"
    "D:\VS\Personal\Blackjack\src\Interfaces\IRankable.cs" = "IRankable.cs"
    "D:\VS\Personal\Blackjack\src\Interfaces\IShoe.cs" = "IShoe.cs"
    "D:\VS\Personal\Blackjack\src\Interfaces\ISuitable.cs" = "ISuitable.cs"
    "D:\VS\Personal\Blackjack\src\Models\Card.cs" = "Card.cs"
    "D:\VS\Personal\Blackjack\src\Models\Hand.cs" = "Hand.cs"
    "D:\VS\Personal\Blackjack\src\Models\Shoe.cs" = "Shoe.cs"
    "D:\VS\Personal\Blackjack\src\Services\GameService.cs" = "GameService.cs"
}

foreach ($file in $codeFiles.Keys) {
    $codeMarker = "// File: $($codeFiles[$file])"
    $newContent = $codeOutput -replace "(?s)(?<=${codeMarker}).*"

    $fileContent = Get-Content -Path $file -Raw
    $updatedContent = $fileContent -replace "(?s)(?<=${codeMarker}).*", $newContent -replace '\s+$'

    $updatedContent = $updatedContent.TrimEnd()

    Set-Content -Path $file -Value $updatedContent
    Write-Host "Updated $($codeFiles[$file])"
}