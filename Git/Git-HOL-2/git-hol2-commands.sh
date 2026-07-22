#!/bin/bash
# Git HOL-2: All commands in sequence

# ── Start from existing repo ─────────────────────────────────
cd GitDemo
git status

# ── Step 2: Create .log file and logs/ folder ────────────────
echo "Application started" > app.log
mkdir logs
echo "Error: something failed" > logs/error.log

# Both appear as untracked
git status

# ── Step 3: Create .gitignore ────────────────────────────────
touch .gitignore
echo "*.log"  >> .gitignore
echo "logs/"  >> .gitignore

cat .gitignore

# ── Step 4: Verify git status ignores them ───────────────────
git status
# app.log and logs/ should NOT appear — only .gitignore is listed

# ── Step 5: Confirm files are ignored ────────────────────────
git add app.log
# Output: The following paths are ignored by one of your .gitignore files

git status --ignored
# Shows all ignored files explicitly

# ── Step 6: Commit .gitignore ────────────────────────────────
git add .gitignore
git commit -m "Add .gitignore to ignore log files and logs folder"
git status

# ── Step 7: Push to remote ───────────────────────────────────
git push origin master
