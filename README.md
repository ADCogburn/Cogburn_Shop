# Local Development Setup: SQL Server with Podman

This project is testing using **Podman** for the Microsoft SQL database.

Why use containers?

> **Quicker DB Resets** - Simply blow the volume away ("purge") and the container will simply make a new one from scratch. This is agnostic to machine, environment, etc.
> **Quicker Onboarding** - New users simply need to install (via a few command lines) Podman and Podman-compose, and then can have their Database running without installing many different software.
> **Consistency** - The ports for appsettings can be set for all devs to the same, which then connect to the same ports in the container. These shouldn't ever go out of sync without manual changes.
> **Automatable** - Because containers are environment agnostic (and ephemeral), these can run on Dev, QA, or even CI/CD pipelines. This allows CI/CD to have it's own temporary DB, run migrations as need, do all UI/API tests, merge if it all passes, then remove this DB. This is all automated and requires no manual setup/teardown.
> **Free** - Podman is a free alternative to Docker. Docker is free for small business (under 250 employees), but charges government agencies regardless of cost. Podman is a free open source alternative.

---

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Podman installed](https://podman.io/getting-started/installation)

---

## Install Podman

### MacOS Instructions
1. Install Podman using Homebrew:
    ```bash
    brew install podman
    ```

2. Initialize and start the Podman virtual machine:
    ```bash
    podman machine init
    podman machine start
    ```

3. Verify Podman is running:
    ```bash
    podman info
    ```

---

### Windows Instructions
1. Download and install Podman Desktop for Windows:
   - [Podman Desktop Download](https://podman.io/getting-started/installation)

2. After installation, initialize and start the Podman machine:
    ```powershell
    podman machine init
    podman machine start
    ```

3. Verify Podman is running:
    ```powershell
    podman info
    ```

> **Note:** On Windows, Podman may run through WSL2 (Windows Subsystem for Linux). Make sure WSL2 is installed if prompted.

---

## Install Podman Compose

Podman Compose is a tool for managing multi-container setups using `docker-compose.yml` files.


Install Podman Compose:

### MacOS
```bash
brew install podman-compose
```

### Windows
```powershell
pip3 install podman-compose
```

Confirm it is installed:

```bash
podman-compose --version
```

---

## Workflow when running the app

Podman runs the Database inside a container, so starting this up is just like starting it up in Microsoft SQL. It can also run indefinitely like SQL. Finally, you can also have startup scripts (.sh or .ps) to start them up, but here I will show the simple command. Be sure to run this BEFORE you run the API, so that the API can run migrations after the DB is running.

When you are ready to start the DB, first make sure Podman is running:

```bash
podman machine start
```

Then spin up the database:

```bash
podman-compose up
```

If you want to stop the database, you can spin it down:

```bash
podman-compose down
```

Finally, you can see what containers are running with:

```bash
podman ps
```

and what volumes are running (should only ever be one from these instructions):

```bash
podman volume ls
```