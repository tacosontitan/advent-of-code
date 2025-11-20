param (
    [int]$Year,
    [int]$StartingDay = 1
)

function CreateIssueForDay {
    param (
        [int]$year,
        [int]$day
    )

    Write-Host "Creating issue for year $year, day $day."
    $issueUrl = gh issue create `
        --title "Solve Day $day" `
        --body "Solve [Advent of Code day $day](https://adventofcode.com/$year/day/$day)." `
        --assignee "@me" `
        --milestone "$year" `
        --project "Roadmaps" `


    $issueNumber = [int]([regex]::Match($issueUrl, '/issues/(\d+)$').Groups[1].Value)
    Write-Host "Created issue #$($issueNumber)."
}

function UpdateProjectFieldsForNewIssue {
    param (
        [int] $issueNumber
    )

    Write-Host "Querying project item..."
    $projectItems = gh project item-list 10 `
        --format json `
        --limit 9999 `
        --owner "tacosontitan" `
        --jq '.items | sort_by(.updatedAt) | .[-1]' |
        ConvertFrom-Json

    if ($null -eq $projectItems) {
        Write-Host "ERROR: Could not find project item for issue #$issueNumber"
        exit 1
    }

    Write-Host "Updating project fields..."
    gh project item-edit --project-id $ROADMAPS_PROJECT_ID --id $projectItems.id --field-id $STATUS_FIELD_ID --single-select-option-id $READY_FOR_WORK_OPTION_ID
    gh project item-edit --project-id $ROADMAPS_PROJECT_ID --id $projectItems.id --field-id $BLOCKED_FIELD_ID --single-select-option-id $NOT_BLOCKED_OPTION_ID
    gh project item-edit --project-id $ROADMAPS_PROJECT_ID --id $projectItems.id --field-id $PRIORITY_FIELD_ID --single-select-option-id $NO_PRIORITY_OPTION_ID
    gh project item-edit --project-id $ROADMAPS_PROJECT_ID --id $projectItems.id --field-id $DIFFICULTY_FIELD_ID --single-select-option-id $NO_DIFFICULTY_OPTION_ID
}

Set-Variable ROADMAPS_PROJECT_ID -Option Constant -Value "PVT_kwHOA-Zq-s4Anjr1"

Set-Variable STATUS_FIELD_ID -Option Constant -Value "PVTSSF_lAHOA-Zq-s4Anjr1zgfRg2s"
Set-Variable READY_FOR_WORK_OPTION_ID -Option Constant -Value "2b19fe15"

Set-Variable BLOCKED_FIELD_ID -Option Constant -Value "PVTSSF_lAHOA-Zq-s4Anjr1zgsL3Xw"
Set-Variable NOT_BLOCKED_OPTION_ID -Option Constant -Value "3d2c158c"

Set-Variable PRIORITY_FIELD_ID -Option Constant -Value "PVTSSF_lAHOA-Zq-s4Anjr1zgsMHZU"
Set-Variable NO_PRIORITY_OPTION_ID -Option Constant -Value "887c52fc"

Set-Variable DIFFICULTY_FIELD_ID -Option Constant -Value "PVTSSF_lAHOA-Zq-s4Anjr1zgsnbxM"
Set-Variable NO_DIFFICULTY_OPTION_ID -Option Constant -Value "9b183a0b"

for ($day = $StartingDay; $day -le 25; $day++) {
    $issueNumber = CreateIssueForDay -year $Year -day $day
    UpdateProjectFieldsForNewIssue -issueNumber $issueNumber
}