# Git HOL-4: Conflict Resolution During Merge

## Objectives
- Understand how merge conflicts occur
- Resolve conflicts manually and using a 3-way merge tool

---

## What is a Merge Conflict?
A conflict happens when **two branches modify the same line(s)** of the same file.
Git cannot decide which version to keep — it marks the file and waits for you to resolve it manually.

---

## Step 1: Verify master is in a clean state

```bash
git status
# Output: nothing to commit, working tree clean
git log --oneline
```

---

## Step 2: Create branch "GitWork" and add hello.xml

```bash
git branch GitWork
git checkout GitWork

# Create hello.xml in the branch
cat > hello.xml << 'EOF'
<?xml version="1.0" encoding="UTF-8"?>
<greeting>
    <message>Hello from GitWork branch!</message>
    <author>Branch Developer</author>
</greeting>
EOF
```

---

## Step 3: Update content and check status

```bash
cat hello.xml    # verify content
git status
# Output: Untracked files: hello.xml
```

---

## Step 4: Commit to branch

```bash
git add hello.xml
git commit -m "Add hello.xml in GitWork branch"
git status
# Output: nothing to commit, working tree clean
```

---

## Step 5: Switch to master

```bash
git checkout master
# Notice hello.xml does NOT exist here yet
ls
```

---

## Step 6: Add hello.xml to master with DIFFERENT content

```bash
# Same filename, different content — this will cause a conflict on merge
cat > hello.xml << 'EOF'
<?xml version="1.0" encoding="UTF-8"?>
<greeting>
    <message>Hello from Master branch!</message>
    <author>Master Developer</author>
</greeting>
EOF
```

---

## Step 7: Commit to master

```bash
git add hello.xml
git commit -m "Add hello.xml in master with different content"
```

---

## Step 8: Observe the log

```bash
git log --oneline --graph --decorate --all
```

Expected output — two diverged branches:
```
* abc1234 (HEAD -> master) Add hello.xml in master
| * def5678 (GitWork) Add hello.xml in GitWork branch
|/
* ghi9012 Initial commit
```

---

## Step 9: Check differences with git diff

```bash
git diff master GitWork
```

Shows the conflicting lines between master and GitWork in `hello.xml`.

---

## Step 10: Visual diff with merge tool (VS Code on macOS)

```bash
# Configure VS Code as merge/diff tool
git config --global merge.tool vscode
git config --global mergetool.vscode.cmd 'code --wait $MERGED'
git config --global diff.tool vscode
git config --global difftool.vscode.cmd 'code --wait --diff $LOCAL $REMOTE'

# Run visual diff
git difftool master GitWork
```

> Note: Lab references P4Merge (Windows). On macOS use VS Code or IntelliJ.

---

## Step 11: Merge GitWork into master — conflict will occur

```bash
git merge GitWork
```

Expected output:
```
Auto-merging hello.xml
CONFLICT (add/add): Merge conflict in hello.xml
Automatic merge failed; fix conflicts and then commit the result.
```

---

## Step 12: Observe the Git conflict markup in hello.xml

```bash
cat hello.xml
```

Git marks the conflict like this:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<greeting>
<<<<<<< HEAD
    <message>Hello from Master branch!</message>
    <author>Master Developer</author>
=======
    <message>Hello from GitWork branch!</message>
    <author>Branch Developer</author>
>>>>>>> GitWork
</greeting>
```

- `<<<<<<< HEAD` = your current branch (master) version
- `=======` = separator
- `>>>>>>> GitWork` = incoming branch version

---

## Step 13: Resolve conflict using 3-way merge tool

```bash
# Open VS Code merge tool — shows 3 panes: LOCAL | BASE | REMOTE
git mergetool
```

**Manually edit hello.xml** to keep the desired content:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<greeting>
    <message>Hello from both Master and GitWork branches - merged!</message>
    <author>Master Developer</author>
</greeting>
```

Remove all `<<<<<<<`, `=======`, `>>>>>>>` markers after resolving.

---

## Step 14: Commit the resolved conflict

```bash
git add hello.xml
git commit -m "Resolve merge conflict in hello.xml between master and GitWork"
git status
```

---

## Step 15: Add backup file to .gitignore

Git mergetool creates a `.orig` backup file (e.g. `hello.xml.orig`). Add it to `.gitignore`:

```bash
git status
# You may see: hello.xml.orig (untracked backup file)

echo "*.orig" >> .gitignore
git status
# hello.xml.orig no longer appears
```

---

## Step 16: Commit the .gitignore update

```bash
git add .gitignore
git commit -m "Add *.orig to .gitignore to ignore merge backup files"
```

---

## Step 17: List all available branches

```bash
git branch
# Output:
# * master
#   GitWork
```

---

## Step 18: Delete the merged branch

```bash
git branch -d GitWork
git branch
# Output: only master remains
```

---

## Step 19: Final log after everything

```bash
git log --oneline --graph --decorate
```

Expected output:
```
*   xyz1234 (HEAD -> master) Resolve merge conflict in hello.xml
|\
| * def5678 Add hello.xml in GitWork branch
* | abc1234 Add hello.xml in master with different content
|/
* ghi9012 Initial commit
```

---

## Push to Remote

```bash
git push origin master
```

---

## Summary of Commands

| Command | Purpose |
|---|---|
| `git branch GitWork` | Create new branch |
| `git checkout GitWork` | Switch to branch |
| `git merge GitWork` | Merge branch into current branch |
| `git diff master GitWork` | Show differences between branches |
| `git mergetool` | Open 3-way merge tool to resolve conflicts |
| `git log --oneline --graph --decorate --all` | Visual log of all branches |
| `git branch -d GitWork` | Delete a merged branch |

---

## Key Points
- A conflict happens when the **same line** is changed in both branches
- Git marks conflicts with `<<<<<<<`, `=======`, `>>>>>>>` — you must remove all markers
- After resolving, always `git add` the resolved file before committing
- `*.orig` backup files are created by mergetool — add to `.gitignore`
