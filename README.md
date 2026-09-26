# 🏰 Clash of Clans Private Server
> Open-source private server emulator for Clash of Clans 10.322 (2018)

![C#](https://img.shields.io/badge/C%23-.NET%20Core%203.1-purple?style=for-the-badge&logo=csharp)
![MySQL](https://img.shields.io/badge/MySQL-Database-blue?style=for-the-badge&logo=mysql)
![Platform](https://img.shields.io/badge/Platform-Windows-0078d7?style=for-the-badge&logo=windows)
![Version](https://img.shields.io/badge/CoC%20Version-10.322-orange?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

> 📢 **Join our community!** For development updates, support, and discussions — join our **[Discord Server](#)**.

---

![screen](./Screens/game.png)

## ✨ Features

- ✅ Clash of Clans **10.322** (2018) support
- ✅ Building construction & upgrades
- ✅ Resource collection
- ✅ Hero upgrades
- ✅ Troop training
- ✅ Alliance system
- ✅ NPC battles
- ✅ Leaderboard & rankings

---

## 📱 Client Support

| Platform | Status |
|----------|--------|
| Android (APK sideload) | ✅ Supported |
| BlueStacks / LDPlayer / MEmu | ✅ Supported |
| iOS | ❌ Not tested |

> **Required CoC Version:** `10.322`

---

## 🖥️ Requirements

| Tool | Version | Link |
|------|---------|------|
| Visual Studio | 2019 / 2022 | [Download](https://visualstudio.microsoft.com/) |
| .NET Core SDK | 3.1 | [Download](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) |
| XAMPP | Latest | [Download](https://www.apachefriends.org/) |
| MySQL | 5.7+ (included in XAMPP) | — |
| CoC APK | 10.322 | Release |

---

## ⚙️ Setup Guide

### 1. Install Requirements
- Install **Visual Studio 2019/2022** with `.NET desktop development` workload
- Install **.NET Core 3.1 SDK**
- Install **XAMPP** and start **Apache** + **MySQL**

### 2. Database Setup
- Open **phpMyAdmin** (`http://localhost/phpmyadmin`)
- Create a new database (e.g. `clashofclans`)
- Import `src/ClashofClans/GameAssets/database.sql`

### 3. Configuration
Edit `config.json` in the build output folder:
```json
{
  "MySqlServer": "localhost",
  "MySqlDatabase": "clashofclans",
  "MySqlUserId": "root",
  "MySqlPassword": ""
}
```

### 4. Build & Run
```
1. Open src/ClashofClans.sln in Visual Studio
2. Build Solution (Ctrl+Shift+B)
3. Run ClashofClans.exe
4. Server starts on port 9339
```

### 5. Connect Client
```
1. Download CoC APK version 10.322
2. Install on Android device or emulator (BlueStacks / LDPlayer / MEmu)
3. Patch the APK server IP to your machine IP
4. Launch and play!
```

---

## 📁 Project Structure

```
CoS-10.322/
├── src/
│   ├── ClashofClans/
│   │   ├── Core/              # Network & configuration
│   │   ├── Database/          # MySQL database layer
│   │   ├── Files/             # CSV logic & game data
│   │   ├── Logic/             # Game logic (Home, Player, etc.)
│   │   ├── Protocol/          # Packet handling & commands
│   │   └── GameAssets/        # CSV files, levels, config
│   └── ClashofClans.Utilities/
├── Screens/
└── README.md
```

---

## 👥 Credits

| Role | Person |
|------|--------|
| 🏗️ Original Author | [astralsc](https://github.com/astralsc) |
| 🔧 Further Development | [McStudio-inc](https://github.com/McStudio-inc) |

---

## ⚠️ Disclaimer

This project is for **educational purposes only**.  
Clash of Clans is owned by **Supercell**. This project is not affiliated with or endorsed by Supercell.

---

<p align="center">Made with ❤️ by <a href="https://github.com/McStudio-inc">McStudio</a></p>
