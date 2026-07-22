# Git HOL-2: Git Ignore — Ignoring Unwanted Files and Folders

## Objectives
- Understand what `.gitignore` is and how it works
- Ignore `.log` files and `logs/` folders from being tracked by Git

---

## What is .gitignore?
A `.gitignore` file tells Git which files and folders to **never track**.
- Git will completely ignore anything listed in `.gitignore`
- Ignored files will NOT appear in `git status` as untracked
- Useful for: log files, build output, IDE config, secrets, node_modules, etc.

---

## Step 1: Start from an existing Git repo (from HOL-1)

```bash
cd GitDemo
git status
```

---

## Step 2: Create a .log file and a logs/ folder

```bash
# Create a log file
echo "Application started" > app.log

# Create a logs folder with a file inside
mkdir logs
echo "Error: something failed" > logs/error.log

# Check status — both will show as untracked
git status
```

Expected output:
```
Untracked files:
  app.log
  logs/
```

---

## Step 3: Create and configure .gitignore

```bash
# Create .gitignore file
touch .gitignore

# Open in editor and add rules
code .gitignore
```

Add the following rules to `.gitignore`:
```
# Ignore all .log files
*.log

# Ignore the entire logs folder
logs/
```

---

## Step 4: Verify git status now ignores them

```bash
git status
```

Expected output — `app.log` and `logs/` are **no longer listed**:
```
Untracked files:
  .gitignore
```

Only `.gitignore` itself appears as untracked — that should be committed.

---

## Step 5: Stage and commit .gitignore

```bash
git add .gitignore
git commit -m "Add .gitignore to ignore log files and logs folder"
git status
# Output: nothing to commit, working tree clean
```

---

## Step 6: Verify ignored files are truly ignored

```bash
# Try staging a .log file — Git will refuse silently
git add app.log
# Output: The following paths are ignored by one of your .gitignore files: app.log

# Force-check what is ignored
git status --ignored
```

---

## Step 7: Push to remote

```bash
git push origin master
```

---

## Summary of .gitignore Patterns

| Pattern | What it ignores |
|---|---|
| `*.log` | All files ending in `.log` |
| `logs/` | The entire `logs` folder |
| `build/` | The entire `build` folder |
| `*.class` | All Java compiled files |
| `node_modules/` | Node.js dependencies |
| `*.env` | Environment variable files |
| `!important.log` | Exception — do NOT ignore this file |

---

## Key Points
- `.gitignore` only ignores **untracked** files. If a file was already committed, you must run `git rm --cached <file>` to stop tracking it.
- `.gitignore` itself should be committed so all team members share the same ignore rules.
- Use `git status --ignored` to see which files are being ignored.
