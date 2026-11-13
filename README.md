# SQL Nightmare

> A SQL Server exploitation tool for authorized security testing and education.

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
![Language](https://img.shields.io/badge/language-C%23-239120.svg)
![Platform](https://img.shields.io/badge/platform-Windows-0078D6.svg)

## Overview

SQL Nightmare automates the exploitation of SQL injection vulnerabilities in applications backed by Microsoft SQL Server. It is built for penetration testers and students who want to understand how injection flaws can be leveraged against a database back end.

## Features

- **Directory navigation** — browse the file system on the database server.
- **Database dumping** — enumerate and extract database contents.
- **File reading** — read files accessible to the SQL Server process.
- **Shell spawning** — obtain command execution on the host.

## Download

This repository contains the source code. A prebuilt executable is available here:

- [SQL-nightmare.exe](https://www.dropbox.com/s/tld2ta55rajgcgf/SQL-nightmare.exe?dl=0)

## Usage

Supply a target URL with the vulnerable parameter crafted in the following format:

```
http://localhost:1234/index.aspx?param=1 AND 0 union select 1,'rummykhan',2 --X-
```

- Replace the vulnerable/injectable column with the marker `rummykhan`.
- Terminate the statement with the trailing comment marker `--X-`.

## Building

Open `SQL-nightmare.sln` in Visual Studio and build the solution, or compile the project with MSBuild.

## Disclaimer

This software is provided for **educational and authorized security testing purposes only**. Do not use it against any system without the explicit, prior permission of its owner. The author accepts no liability for misuse or for any damage caused by this tool. You are solely responsible for complying with all applicable laws.

## Contact

Questions or feedback: [rehan_manzoor@outlook.com](mailto:rehan_manzoor@outlook.com)

## License

Released under the [MIT License](LICENSE).
