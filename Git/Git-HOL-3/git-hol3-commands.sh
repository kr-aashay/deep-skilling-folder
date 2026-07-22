#!/bin/bash
# Git HOL-3: Branching and Merging — all commands in sequence

# ── BRANCHING ────────────────────────────────────────────────

# Step 1: Create new branch
git branch GitNewBranch

# Step 2: List all branches (* marks current branch)
git branch
git branch -r   # remote branches
git branch -a   # all branches

# Step 3: Switch to new branch and add a file
git checkout GitNewBranch
git branch   # verify active branch

echo "This is a feature file added in GitNewBranch" > feature.txt
cat feature.txt

# Step 4: Stage and commit in the branch
git add feature.txt
git commit -m "Add feature.txt in GitNewBranch"

# Step 5: Check status
git status

# ── MERGING ──────────────────────────────────────────────────

# Step 1: Switch back to master
git checkout master
ls   # feature.txt should NOT be here yet

# Step 2: CLI diff between master and branch
git diff master GitNewBranch

# Step 3: Visual diff (VS Code on macOS instead of P4Merge)
git config --global diff.tool vscode
git config --global difftool.vscode.cmd 'code --wait --diff $LOCAL $REMOTE'
git difftool master GitNewBranch

# Step 4: Merge branch into master
git merge GitNewBranch
ls           # feature.txt now exists in master
cat feature.txt

# Step 5: View graph log after merge
git log --oneline --graph --decorate

# Step 6: Delete merged branch
git branch -d GitNewBranch
git branch   # only master remains
git status

# ── Push to remote ───────────────────────────────────────────
git push origin master
