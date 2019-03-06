workflow "Build and Publish" {
  on = "push"
  resolves = "Build"
}

action "ECLint" {
  uses = "./ci/action-eclint-wrapper"
  args = "check"
}

action "Test" {
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = "test"
}

action "Build" {
  needs = ["ECLint", "Test"]
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = "build"
}

workflow "Pull Request Status Checks" {
  resolves = "PR Status Giphy"
  on = "pull_request"
}

action "PR Status Giphy" {
  uses = "jzweifel/pr-status-giphy-action@master"
  secrets = ["GITHUB_TOKEN", "GIPHY_API_KEY"]
}
