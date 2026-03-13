# AR Knick-Knack — Wrigley Field & New York City

**CS 5124 — Augmented Reality | University of Cincinnati**
**Soham Vakani**

---

## Motivation

A knick-knack is a small object that carries a memory — a snow globe on a shelf, a figurine from a trip. This project reimagines that idea in Augmented Reality: two physical cubes, each one a memory of a place that means something to me.

**Wrigley Field, Chicago** — I went to two Cubs games at Wrigley Field with my friend Ryan and his family. The atmosphere there is unlike anything else.The small outfield and stadium, the history of the stadium, the rooftop bleachers across the street and the way the whole neighborhood comes alive on game day. So this was one of the locations I chose for my cube.

**New York City** — I traveled to NYC for a Major League Hacking (MLH) Hackathon event and spent time exploring the city after the event was done in upstate NY. Walking around Midtown, seeing the Empire State Building, grabbing food from Chinatown, smaller shops was so much fun. It was one of those trips that reminds you how alive a city can feel. The NYC knick-knack is my attempt to capture that energy with some of my favorite landmarks.

Both knick-knacks sit on tracked physical cubes and can be viewed simultaneously through a webcam, bringing two cities onto your desk at the same time.

---

## Design

### Wrigley Field Knick-Knack

The Wrigley cube uses a printed merge cube as its tracking target. On top of the cube sits a miniature baseball scene:

| Model            | Source                   |
| ---------------- | ------------------------ |
| Baseball Stadium | Poly Pizza (CC License)  |
| Baseball         | Poly Pizza (CC License)  |
| Bench            | Poly Pizza (CC License)  |
| Baseball Cap     | Created by me in Blender |
| Baseball Bat     | Created by me in Blender |

The stadium is the centerpiece, with the other models arranged around it to evoke the feel of a ballpark. The cap and bat were modeled from scratch in Blender using basic primitive shapes — the cap is built from a flattened cylinder and hemisphere, and the bat from tapered cylinders.

An ambient crowd/ballpark sound loops quietly in the background to reinforce the atmosphere.

**[SCREENSHOT: Wrigley cube with all 5 models visible on top face]**

Each side of the Wrigley cube shows a different information panel:

- **Location Panel** — "Wrigley Field, Chicago, IL" in clean white text
- **Weather Panel** — Live current weather for Chicago pulled from the OpenWeatherMap API, including temperature in °F and a condition description.
  Text color changes based on conditions: blue for rain, yellow for sunny, grey for cloudy, white for snow. (Level 4 requirement)
- **Time Panel** — The current local time in Chicago (Central Time), updated every second. Text color changes based on time of day: orange for sunrise, yellow for daytime, red-orange for sunset, blue for nighttime. (Level 4 requirement)
- **Fun Fact Panel** — A static panel with historical information about Wrigley Field.

**[SCREENSHOT: Wrigley cube side panels showing weather and time]**

---

### New York City Knick-Knack

The NYC cube uses another merge cube as its tracking target — each face of the dice is visually unique, making it ideal for Vuforia multi-target tracking.

| Model             | Source                   |
| ----------------- | ------------------------ |
| Skyscraper        | Poly Pizza (CC License)  |
| Hot Dog           | Poly Pizza (CC License)  |
| Statue of Liberty | Poly Pizza (CC License)  |
| Chair             | Created by me in Blender |
| Baseball Bat      | Created by me in Blender |

The Statue of Liberty and skyscraper anchor the NYC scene visually, while the hot dog is a nod to the classic NYC street food experience. The chair was modeled in Blender using box modeling techniques with extruded legs and a backrest.

**[SCREENSHOT: NYC dice cube with all models visible on top face]**

Each side of the NYC cube mirrors the same panel structure as Wrigley:

- **Location Panel** — "New York City, New York, NY"
- **Weather Panel** — Live weather for NYC with the same color-coded text system
- **Time Panel** — Current local time in New York (Eastern Time) with day/night color coding
- **Fun Fact Panel** — Key facts about New York City

**[SCREENSHOT: NYC cube side panels]**

---

### Proximity Interaction

When both cubes are brought close together in the webcam view, a floating text message — **"Chicago meets NYC!"** — appears between them in 3D space, facing the camera. This is a Level 5 interaction triggered by proximity detection between both Multi Targets.

**[SCREENSHOT: Both cubes close together with floating text visible]**

---

## Process

### How to Run

This application runs locally on a Mac or PC with a webcam. To run it:

1. Clone the repository from GitHub: `https://github.com/sohamvakani/ar-knick-knack`
2. Open the project in **Unity** (tested on Unity 6.x)
3. Install **Vuforia Engine** via the Package Manager
4. Create a `config.txt` file in `Assets/Resources/` and paste your OpenWeatherMap API key inside it (this file is gitignored for security)
5. Print the merge cube template (included in the repo) and the dice template
6. Hit **Play** in Unity and point your webcam at either or both cubes

### Code Structure

```
Assets/
├── Scripts/
│   ├── WeatherDisplay.cs          # Chicago weather API call + display
│   ├── WeatherDisplay_NYC.cs      # NYC weather API call + display
│   ├── WeatherColorDisplay.cs     # Standalone weather condition color script
│   ├── TimeDisplay.cs             # Chicago local time display
│   ├── TimeDisplay_NYC.cs         # NYC local time display
│   ├── DayNightIcon.cs            # Chicago day/night text + color
│   ├── DayNightIcon_NYC.cs        # NYC day/night text + color
│   ├── LocationDisplay.cs         # Chicago location name
│   ├── LocationDisplay_NYC.cs     # NYC location name
│   ├── ProximityDetector.cs       # Detects distance between both cubes
│   └── AutoScale.cs               # Auto-scales imported models to fit cube
├── Models/                        # All imported 3D models (FBX/DAE/OBJ)
├── Materials/                     # Unity materials for models and panels
├── Audio/                         # Ambient sound clips
└── Resources/
    └── config.txt                 # API key (gitignored)
```

### Libraries and APIs

- **Unity 6** — Game engine and AR scene management
- **Vuforia Engine 11.x** — AR tracking via Multi Image Targets for both cubes
- **TextMeshPro** — All in-scene text rendering on cube face panels
- **OpenWeatherMap API** — Free tier weather data, polled every 10 minutes using Unity's `UnityWebRequest` and parsed with `JsonUtility`
- **C# System.TimeZoneInfo** — Used to convert UTC to local Chicago (Central) and NYC (Eastern) time zones

### GitHub Repository

[https://github.com/sohamvakani/ar-knick-knack](https://github.com/sohamvakani/ar-knick-knack)

The repository includes regular commits throughout development, tracking each feature as it was implemented.

---

## Challenges and Future Work

### Challenges

**Large Files in Git History**
Early in the project, Unity's `Library/` and `Packages/` folders — including a 131MB Vuforia `.tgz` file — were accidentally committed to the repository. GitHub rejected the push. Resolving this required using `git filter-branch` to rewrite the entire commit history and remove the large files retroactively, followed by a force push. A proper `.gitignore` was added afterward to prevent recurrence.

**SKP File Conversion**
Finding a 3D model of Wrigley Field led down a rabbit hole of SketchUp `.skp` files, which Unity cannot import natively. Attempts to use a Blender SketchUp importer addon failed due to incompatibility with Blender 5.0. After significant time spent on conversion attempts, the decision was made to source models directly from Poly Pizza in game-ready formats, which was significantly faster.

**Vuforia Face Confusion (Faces 2 and 6)**
The merge cube's face 2 and face 6 share very similar patterns — essentially the same image rotated 180 degrees. Vuforia frequently confused the two faces, causing the knick-knack to flip unpredictably. The fix was to disable the bottom face entirely in the Unity Hierarchy and avoid placing content on the problematic face.

**API Key Activation Delay**
After regenerating the OpenWeatherMap API key for security reasons, the new key took approximately 30 minutes to activate on OpenWeatherMap's servers. This caused the weather panels to show "Weather Unavailable" during testing, which was initially mistaken for a code bug.

**Emoji Rendering in TextMeshPro**
Attempts to use emoji characters (☀️ 🌙) for the day/night indicator resulted in blank squares in TextMeshPro, which does not support emoji by default without a custom sprite asset. The solution was to use colored text labels instead ("Daytime", "Nighttime") with matching colors, which actually produced a cleaner visual result.

**Point Light Not Rendering in Game View**
A Point Light added as a child of the Multi Target was visible in the Scene view but did not appear in the Game view during Play. This is a known issue with Vuforia's initialization of child objects. After troubleshooting, the lighting approach was replaced with text color changes for day/night and weather conditions, which proved more readable and reliable.

**Proximity Text Orientation**
The floating "Chicago meets NYC!" text appears correctly in the Scene view but renders sideways in the Game view. This is a known issue with `LookAt` rotation when TextMeshPro objects are parented differently than expected. A full fix was not completed before the deadline but the text does appear and trigger correctly on proximity.

### Future Work

- **Fix proximity text orientation** so it renders upright in the Game view
- **Add particle systems** for weather conditions — rain particles for rainy weather, snow for winter
- **iPhone camera integration** via the Camo app for higher quality AR tracking and reduced jitter
- **Animated models** — stadium lights turning on at night, models reacting to proximity between cubes
- **Deploy as a standalone build** so the app can be run without Unity Editor

---

## Use of AI and Collaboration

**AI:** I used Claude (Anthropic) extensively throughout this project as a technical resource. Claude helped with debugging Vuforia multi-target setup, writing and iterating on the C# scripts for weather display, time zones, day/night detection, and proximity detection. Claude also helped navigate the git history rewriting issue, model format conversion troubleshooting, and finding appropriate 3D model sources online. I found it most useful as a debugging partner describing an error and working through the cause systematically rather than as a code generator. All code was reviewed, understood, and integrated by me.

---
