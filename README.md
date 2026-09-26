# Clash of Clans Private Server
Open source private server emulator for Clash of Clans 10.322 (2018) written in C#

![screen](./Screens/game.png)

## Credits
- [astralsc](https://github.com/astralsc) — original author
- [McStudio-inc](https://github.com/McStudio-inc) — further development

## Setup

### Requirements
- .NET Core 3.1
- MySQL Server
- Xampp

### Database
1. Create a MySQL database
2. Import `src/ClashofClans/GameAssets/database.sql`

### Configuration
1. Copy `config.json` from the build output
2. Fill in your MySQL credentials:
```json
{
  "MySqlServer": "localhost",
  "MySqlDatabase": "your_database",
  "MySqlUserId": "your_user",
  "MySqlPassword": "your_password"
}
```

### Running
1. Open `src/ClashofClans.sln` in Visual Studio
2. Build the solution
3. Run `ClashofClans.exe`
4. Connect with a Clash of Clans 10.322 client to `your_ip:9339`
