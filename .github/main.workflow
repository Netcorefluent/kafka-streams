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
