# Fox Game — 2D Platformer (Unity)

A small **2D platformer game developed in Unity** as an introduction to the Unity engine and gameplay scripting in **C#**.  
The project includes **custom player mechanics, enemy behaviors, moving platforms, environmental interactions, and UI systems**, built on top of the **Sunny Valley Studio asset pack**.

The game demonstrates fundamental concepts of **2D game development**, including physics-based movement, scene management, animation control, and gameplay state management.

---

## Tech Stack

* **Game Engine:** Unity (2D)
* **Language:** C#
* **Graphics:** Sunny Valley Studio asset pack
* **Physics:** Unity 2D Physics
* **UI:** Unity Canvas System
* **Audio:** Unity AudioSource

---

## Core Features

* **Player Movement System:** Physics-based character controller with jumping, ladder climbing, and directional animation.
* **Enemy Behaviors:** Patrol-based enemy movement with configurable ranges.
* **Moving Platforms:** Dynamic platforms that move between positions or follow waypoint paths.
* **Interactive Objects:** Lever-triggered mechanisms affecting environmental objects.
* **Score & Game State System:** GameManager controlling gameplay states, menus, and UI.
* **Camera Tracking:** Smooth camera following with configurable movement bounds.
* **Menu System:** Main menu, pause menu, options menu, and scene transitions.

---

## Gameplay Systems

### Player Controller

The `PlayerController` script handles:

* horizontal movement
* jumping mechanics
* ladder climbing
* animation transitions
* audio triggers
* collision detection with environment

Movement uses **Unity Rigidbody2D physics**, allowing smooth and responsive control.

---

### Enemy Controller

Enemies move within a **defined horizontal range** and automatically reverse direction when reaching limits.

Key mechanics include:

* patrol movement
* sprite flipping based on direction
* animation state control
* configurable speed and patrol distance

Example parameters:

```
moveSpeed
moveRange
```

---

### Moving Platforms

Two platform systems are implemented:

#### MovingPlatform

Platforms move **back and forth between two horizontal positions**.

Parameters include:

```
moveSpeed
moveRange
```

---

#### WaypointFollower

Platforms or objects move along **multiple predefined waypoints**, creating more complex movement paths.

Movement is handled using:

```
Vector2.MoveTowards()
```

---

### Generated Platforms System

The `GeneratedPlatforms` script dynamically creates a set of platforms arranged in a circular layout.

These platforms can be **activated via a lever**, creating interactive environmental puzzles.

Key mechanics:

* procedural platform positioning
* synchronized movement
* lever-controlled activation

---

### Lever Interaction System

The `LeverController` script implements a **toggle-based environmental switch**.

When the player enters the trigger collider:

* the lever changes state
* the animation updates
* connected systems (such as platforms) can activate

Example state variable:

```
isOn
```

---

### Camera System

The `CameraFollow` script provides **smooth camera tracking** of the player.

Features include:

* horizontal and vertical smoothing
* configurable camera boundaries
* player movement margins before camera adjustment

Example parameters:

```
xMargin
yMargin
xSmooth
ySmooth
minXAndY
maxXAndY
```

---

### Game Manager

The `GameManager` controls the **global state of the game**.

Implemented game states include:

```
GAME
PAUSE_MENU
LEVEL_COMPLETED
LEVEL_LOOSE
OPTIONS
```

Responsibilities include:

* UI canvas management
* game state transitions
* pause functionality
* level completion handling
* score and UI updates

The system is implemented using a **singleton pattern** for easy global access.

---

### Main Menu System

The `MainMenuManager` handles navigation from the main menu.

Features include:

* starting levels
* scene loading
* exiting the game

Scene transitions are performed using:

```
SceneManager.LoadScene()
```

---

## Project Structure

```
fox-game/
│
├── Assets
│   ├── Scenes
│   │   └── MainMenu.unity
│   │
│   ├── Prefabs
│   │
│   └── Scripts
│       ├── PlayerController.cs
│       ├── EnemyController.cs
│       ├── CameraFollow.cs
│       ├── GameManager.cs
│       ├── LeverController.cs
│       ├── GeneratedPlatforms.cs
│       ├── MovingPlatformController.cs
│       ├── WaypointFollower.cs
│       └── MainMenuManager.cs
│
└── README.md
```

---

## Running the Project

1. **Clone the repository**

```bash
git clone https://github.com/<username>/fox-game.git
```

2. **Open the project in Unity**

Open the project folder using **Unity Hub**.

3. **Load the main scene**

Navigate to:

```
Assets → Scenes → MainMenu
```

4. **Start the game**

Press **Play** inside the Unity editor.

---

## Gameplay Elements

The game currently includes:

* collectible items (gems, cherries, hearts)
* enemies
* moving platforms
* lever-based puzzles
* UI menus
* score tracking

Level design uses **custom layouts combined with Sunny Valley assets**.

---

## Learning Goals

This project was created primarily to explore:

* Unity scene workflow
* player movement systems
* interaction systems
* enemy scripting
* camera control
* UI integration
* gameplay state management

---

## Future Improvements

Potential future additions include:

* additional levels
* improved enemy AI
* save system
* checkpoints
* particle effects
* sound improvements
* additional platform mechanics

---

## Assets

Environment and character graphics are based on the **Sunny Valley Studio 2D platformer asset pack**.

```
https://assetstore.unity.com/
```

---

## License

This project is intended primarily for **learning and experimentation with Unity development**.
