#!/bin/bash
# Git HOL-4: Conflict Resolution — all commands in sequence

# Step 1: Verify master is clean
git status
git log --oneline

# Step 2: Create GitWork branch and add hello.xml
git branch GitWork
git checkout GitWork
cat > hello.xml << 'EOF'
<?xml version="1.0" encoding="UTF-8"?>
<greeting>
    <message>Hello from GitWork branch!</message>
    <author>Branch Developer</author>
</greeting>
EOF

# Step 3: Check status
cat hello.xml
git status

# Step 4: Commit to branch
git add hello.xml
git commit -m "Add hello.xml in GitWork branch"
git status

# Step 5: Switch to master
git checkout master
ls   # hello.xml not here yet

# Step 6: Add DIFFERENT hello.xml to master
cat > hello.xml << 'EOF'
<?xml version="1.0" encoding="UTF-8"?>
<greeting>
    <message>Hello from Master branch!</message>
    <author>Master Developer</author>
</greeting>
EOF

# Step 7: Commit to master
git add hello.xml
git commit -m "Add hello.xml in master with different content"

# Step 8: View diverged log
git log --oneline --graph --decorate --all

# Step 9: CLI diff
git diff master GitWork

# Step 10: Visual diff (VS Code)
git config --global merge.tool vscode
git config --global mergetool.vscode.cmd 'code --wait $MERGED'
git difftool master GitWork

# Step 11: Merge — CONFLICT will occur
git merge GitWork

# Step 12: View conflict markup
cat hello.xml

# Step 13: Open 3-way merge tool to resolve
git mergetool
# Manually edit hello.xml to keep desired content
# Remove all <<<<<<< ======= >>>>>>> markers

# Step 14: Commit resolved conflict
git add hello.xml
git commit -m "Resolve merge conflict in hello.xml between master and GitWork"
git status

# Step 15: Add *.orig backup files to .gitignore
echo "*.orig" >> .gitignore
git status   # hello.xml.orig no longer shown

# Step 16: Commit .gitignore
git add .gitignore
git commit -m "Add *.orig to .gitignore"

# Step 17: List all branches
git branch

# Step 18: Delete merged branch
git branch -d GitWork
git branch   # only master remains

# Step 19: Final graph log
git log --oneline --graph --decorate

# Push to remote
git push origin master
