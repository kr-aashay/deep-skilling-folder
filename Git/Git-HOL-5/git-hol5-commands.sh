#!/bin/bash
# Git HOL-5: Clean Up and Push Back to Remote — all commands in sequence

# Step 1: Verify master is in clean state
git status
# Expected: nothing to commit, working tree clean

# If there are pending changes, commit them first
# git add .
# git commit -m "Cleanup before push"

# Step 2: List all available branches
git branch
# Expected: only * master (GitWork deleted in HOL-4)

# If GitWork still exists, delete it
# git branch -d GitWork

# Step 3: Pull remote repository to get latest changes
git pull origin master
# Merges any remote changes into local master before pushing

# Step 4: Push all pending changes to remote
git push origin master
# Pushes HOL-4 commits: conflict resolution + .gitignore update

# First time push (set upstream tracking):
# git push -u origin master

# Step 5: Verify local and remote are in sync
git status
# Expected: Your branch is up to date with 'origin/master'.

git log --oneline --graph --decorate
