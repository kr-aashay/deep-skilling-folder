# Git HOL-5: Clean Up and Push Back to Remote Git

## Objectives
- Verify local repo is clean after conflict resolution (HOL-4)
- Pull latest changes from remote
- Push all pending local commits to remote repository

---

## What is Push & Pull?
- `git pull` — fetches changes from remote and merges into local branch (keeps local in sync with team)
- `git push` — sends local commits to remote repository (shares your work with the team)

Always **pull before push** to avoid conflicts with teammates' changes.

---

## Step 1: Verify master is in a clean state

```bash
git status
```

Expected output:
```
On branch master
nothing to commit, working tree clean
```

If there are uncommitted changes, commit or stash them first:
```bash
# Option 1: commit pending changes
git add .
git commit -m "Cleanup before push"

# Option 2: stash temporarily
git stash
```

---

## Step 2: List all available branches

```bash
git branch
```

Expected output after HOL-4 cleanup:
```
* master
```

`GitWork` branch should already be deleted from HOL-4.
If not, delete it now:
```bash
git branch -d GitWork
```

---

## Step 3: Pull remote repository to master

```bash
# Fetch + merge remote changes into local master
git pull origin master
```

If remote has changes not in local:
```
remote: Counting objects...
Updating abc1234..def5678
Fast-forward
```

If already up to date:
```
Already up to date.
```

> Pull before push ensures you have the latest remote changes and reduces push conflicts.

---

## Step 4: Push all pending changes to remote

This pushes all commits from HOL-4 (conflict resolution, .gitignore update) to remote:

```bash
git push origin master
```

If the remote branch doesn't exist yet:
```bash
git push -u origin master
# -u sets upstream tracking so future pushes just need: git push
```

---

## Step 5: Verify changes are reflected in remote

```bash
# Check local vs remote is in sync
git status
# Output: Your branch is up to date with 'origin/master'.

# View log — local and remote HEAD should match
git log --oneline --graph --decorate
```

Expected output:
```
* xyz1234 (HEAD -> master, origin/master) Add *.orig to .gitignore
* abc1234 Resolve merge conflict in hello.xml
* ...
```

Both `HEAD -> master` and `origin/master` point to the same commit — local and remote are in sync.

Also verify on GitHub/GitLab:
- Open your repository in the browser
- Confirm `hello.xml` (resolved), `.gitignore` (with `*.orig`) are visible
- Check commit history matches your local log

---

## Summary of Commands

| Command | Purpose |
|---|---|
| `git status` | Verify working directory is clean |
| `git branch` | List all local branches |
| `git pull origin master` | Pull remote changes into local master |
| `git push origin master` | Push local commits to remote |
| `git push -u origin master` | Push and set upstream tracking |
| `git log --oneline --graph --decorate` | Verify local and remote are in sync |

---

## Key Points
- Always verify `git status` is clean before pushing
- Always `git pull` before `git push` when working in a team
- After push, `origin/master` and `HEAD -> master` should point to the same commit in the log
- Use `git push -u origin master` on first push to set up tracking — after that just `git push`
