#!/bin/bash
# Git HOL-1: All commands in sequence
# Run each block manually or source this file

# ── Step 1: Verify Git ──────────────────────────────────────
git --version

# ── Step 2: Configure identity ──────────────────────────────
git config --global user.name  "Your Name"
git config --global user.email "your.email@example.com"
git config --list

# ── Step 3: Set default editor (VS Code) ────────────────────
git config --global core.editor "code --wait"
# Open global config in editor to verify
git config --global -e

# ── Step 4: Initialise local repo ───────────────────────────
mkdir GitDemo
cd GitDemo
git init
ls -la

# ── Step 5: Create and verify file ──────────────────────────
echo "Welcome to GitDemo repository!" > welcome.txt
ls -la
cat welcome.txt

# ── Step 6: Stage and commit ────────────────────────────────
git status
git add welcome.txt
git status
git commit -m "Initial commit: add welcome.txt"
git status

# ── Step 7: Push to remote ──────────────────────────────────
git remote add origin https://github.com/your-username/GitDemo.git
git pull origin master --allow-unrelated-histories
git push -u origin master
