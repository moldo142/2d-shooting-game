# 2D Shooting Game

Un joc 2D simplu cu arme dezvoltat în C# folosind Windows Forms.

## Caracteristici

- ✅ Jucător controlabil
- ✅ Sistem de tragere
- ✅ Inamici care se mișcă
- ✅ Coliziuni
- ✅ Sistem de puncte și vieți
- ✅ Dificultate progresivă

## Controale

- **WASD** sau **Săgeți** - Mișcare
- **Mouse** - Ține mouse-ul pentru a viza
- **Click Stânga** - Tragere
- **R** - Restart (când jocul se termină)

## Cum să rulez jocul

### Prerequisite
- .NET 6.0 sau superior
- Visual Studio 2022 sau alt IDE C#

### Pași
1. Clonează repository-ul
   ```bash
   git clone https://github.com/moldo142/2d-shooting-game.git
   cd 2d-shooting-game
   ```

2. Restaurează NuGet packages
   ```bash
   dotnet restore
   ```

3. Rulează jocul
   ```bash
   dotnet run
   ```

## Gameplay

1. Controlezi un cerc **albastru** (jucătorul)
2. Inamicii roșii se apropie de tine
3. Ține mouse-ul pentru a viza și click pentru a trage
4. Ucide inamicii pentru a câștiga puncte
5. Ai 3 vieți - ai grijă!
6. Dificultatea crește pe măsură ce progresezi

## Structura Proiectului

- `Program.cs` - Punct de intrare
- `GameForm.cs` - Fereastra principală
- `GameEngine.cs` - Logica jocului
- `Player.cs` - Clasa jucător
- `Bullet.cs` - Clasa gloanțe
- `Enemy.cs` - Clasa inamici

## Versiune
1.0.0

## Autor
moldo142
