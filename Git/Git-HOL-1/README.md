# Git HOL-1: Git Configuration, Editor Integration & First Repository

## Objectives
- Set up Git with user configuration
- Integrate a text editor as the default Git editor
- Create a local repository, track files, and push to remote

---

## Step 1: Verify Git Installation

```bash
git --version
```
Expected output: `git version 2.x.x` — confirms Git is installed correctly.

---

## Step 2: Configure User Identity (Global)

```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

Verify configuration was saved:
```bash
git config --list
```
Expected output shows `user.name` and `user.email` entries.

---

## Step 3: Set Default Editor (VS Code on macOS)

> Note: The lab references Notepad++ (Windows only).
> On macOS, use VS Code instead.

```bash
# Set VS Code as default Git editor
git config --global core.editor "code --wait"

# Verify editor is set
git config --global -e
```

This opens the global `.gitconfig` file in VS Code.

Your `~/.gitconfig` should contain:
```ini
[user]
    name  = Your Name
    email = your.email@example.com
[core]
    editor = code --wait
```

---

## Step 4: Create a Local Repository

```bash
# Create project folder and initialise Git
mkdir GitDemo
cd GitDemo
git init
```

`git init` creates a hidden `.git/` directory — this is the local repository.

```bash
# Verify the hidden .git folder was created
ls -la
```

---

## Step 5: Create a File and Track It

```bash
# Create welcome.txt with content
echo "Welcome to GitDemo repository!" > welcome.txt

# Verify file was created
ls -la

# Verify file content
cat welcome.txt
```

---

## Step 6: Stage and Commit

```bash
# Check status — welcome.txt is untracked
git status

# Stage the file (move from Working Directory → Staging Area)
git add welcome.txt

# Check status again — welcome.txt is now staged
git status

# Commit with multi-line message using the default editor
git commit
# (editor opens — add your commit message, save and close)

# OR commit with inline message
git commit -m "Initial commit: add welcome.txt"

# Verify local repository matches working directory
git status
# Output: nothing to commit, working tree clean
```

---

## Step 7: Push to Remote Repository

```bash
# Add remote origin (replace with your GitHub/GitLab URL)
git remote add origin https://github.com/your-username/GitDemo.git

# Pull remote to sync first (if repo was initialised on remote)
git pull origin master --allow-unrelated-histories

# Push local commits to remote
git push -u origin master
```

---

## Summary of Commands Used

| Command | Purpose |
|---|---|
| `git --version` | Verify Git is installed |
| `git config --global user.name` | Set global username |
| `git config --global user.email` | Set global email |
| `git config --list` | View all config settings |
| `git config --global core.editor` | Set default editor |
| `git config --global -e` | Open global config in editor |
| `git init` | Initialise a new local repository |
| `ls -la` | List all files including hidden |
| `echo "..." > file.txt` | Create file with content |
| `cat file.txt` | View file content |
| `git status` | Show working directory status |
| `git add <file>` | Stage a file |
| `git commit -m "msg"` | Commit staged changes |
| `git remote add origin <url>` | Link local repo to remote |
| `git pull origin master` | Pull remote changes |
| `git push origin master` | Push local commits to remote |
