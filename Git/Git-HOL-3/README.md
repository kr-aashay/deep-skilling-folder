# Git HOL-3: Branching and Merging

## Objectives
- Create a branch, make changes, and merge it back to master
- Understand branch listing, diff, merge, and cleanup

---

## What is Branching?
A branch is an independent line of development.
- `master` (or `main`) is the stable production branch
- Feature/bug branches let you work in isolation without affecting master
- Once work is complete, branches are merged back into master

## What is Merging?
Merging combines changes from one branch into another.
- Git compares the two branches and integrates the differences
- Conflicts occur when the same lines are changed in both branches

---

## BRANCHING

### Step 1: Create a new branch "GitNewBranch"

```bash
git branch GitNewBranch
```

### Step 2: List all local and remote branches

```bash
# List local branches — * marks the current active branch
git branch

# List remote branches
git branch -r

# List all (local + remote)
git branch -a
```

Expected output:
```
* master
  GitNewBranch
```

### Step 3: Switch to the new branch and add files

```bash
# Switch to GitNewBranch
git checkout GitNewBranch
# OR using modern syntax:
git switch GitNewBranch

# Verify you are on the new branch
git branch

# Create a new file with content
echo "This is a feature file added in GitNewBranch" > feature.txt

# View the file
cat feature.txt
```

### Step 4: Commit the changes to the branch

```bash
git add feature.txt
git commit -m "Add feature.txt in GitNewBranch"
```

### Step 5: Check git status

```bash
git status
# Output: nothing to commit, working tree clean
```

---

## MERGING

### Step 1: Switch back to master

```bash
git checkout master
# OR:
git switch master

# Notice feature.txt does NOT exist here — it lives only in GitNewBranch
ls
```

### Step 2: View differences between master and branch (CLI)

```bash
# Shows all file/line differences between the two branches
git diff master GitNewBranch
```

Expected output shows `feature.txt` added in GitNewBranch.

### Step 3: View visual differences with a diff tool

```bash
# Using VS Code as diff tool (macOS alternative to P4Merge)
git difftool master GitNewBranch

# To set VS Code as the default diff tool:
git config --global diff.tool vscode
git config --global difftool.vscode.cmd 'code --wait --diff $LOCAL $REMOTE'
```

> Note: The lab references P4Merge (Windows). On macOS use VS Code or run:
> `git diff master GitNewBranch` for CLI output.

### Step 4: Merge GitNewBranch into master

```bash
# Must be on master before merging
git merge GitNewBranch

# Verify feature.txt now exists in master
ls
cat feature.txt
```

### Step 5: View git log after merging

```bash
# Visual one-line log with branch graph and decorations
git log --oneline --graph --decorate
```

Expected output:
```
*   abc1234 (HEAD -> master) Merge branch 'GitNewBranch'
|\
| * def5678 (GitNewBranch) Add feature.txt in GitNewBranch
|/
* ghi9012 Initial commit: add welcome.txt
```

### Step 6: Delete the branch after merging

```bash
# Delete local branch (safe — only works if already merged)
git branch -d GitNewBranch

# Verify branch is deleted
git branch
# Output: only master remains

git status
# Output: nothing to commit, working tree clean
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
| `git branch <name>` | Create a new branch |
| `git branch` | List local branches (* = current) |
| `git branch -a` | List all branches including remote |
| `git checkout <branch>` | Switch to a branch |
| `git switch <branch>` | Switch to a branch (modern syntax) |
| `git diff master <branch>` | Show differences between branches |
| `git difftool master <branch>` | Visual diff using configured tool |
| `git merge <branch>` | Merge branch into current branch |
| `git log --oneline --graph --decorate` | Visual commit history with branches |
| `git branch -d <branch>` | Delete a merged branch |

---

## Merge Request (GitLab / Pull Request in GitHub)

Instead of merging locally, teams use **Merge Requests** (GitLab) or **Pull Requests** (GitHub):

1. Push your branch to remote: `git push origin GitNewBranch`
2. Go to GitLab/GitHub → open a **Merge Request / Pull Request**
3. Set source branch = `GitNewBranch`, target = `master`
4. Add description, assign reviewers
5. Reviewer approves → merge is done on the server
6. Delete branch after merge

This is the standard team workflow for code review before merging.
