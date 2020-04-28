# Project Chronos 
**Presented by RIP Studios**

Members: Kavin Shravan Krishnan, Lang Wu, Peter Everett Zupke, Rachel M Techau, Semin Choi, Yong Jian Quek

## Description
You wake up in a room alone, with nothing but a single sword in front of you. You don’t know who you are or how you got there. All you hear is a voice that taunts you. 

**"Kill Them All. Reach The End. All Of Time Is Yours”.**

You reach for the sword. It's time to kill.

Built on Unity 2019.2.19f1

## Requirements Completed
1. 3D Feel Game
    1. Players directly interact within the simulated space as a character with real-time control and polish
    2. Goal to kill the enemy and reach the end intuitive
    3. Game over and stage clear menus implemented
    4. "Rewind Time" as a flavourful way to restart the level
    5. Third person slasher
2. Fun Gameplay
    1. Main goal for the tutorial stated in the introduction by a robotic voice
    2. Choice of when to use the TimeShift mechanic
    3. TimeShift limited to refilling gauge, players are challenged to think on how they would want to manage this finite resource.
    4. Players engage with interactive platforms and enemies
    5. Death awaits those who fail puzzles or get hit by enemies
3. 3D Character with Real-Time Control
    1. Movement is key to solving puzzles and avoiding enemy attacks like in any third person slasher
    2. Control script and animation controller developed by our team
    3. Multiple animations such as attacking blended together with movements
    4. Movement is tied to analog control. Simulated analog control available for keyboard by pressing number 1 - 9
    5. Camera is smooth and does not cut through any walls
    6. Main character ragdolls if killed
4. 3D World with Physics and Spatial Simulation
    1. Game world built by our team specifically to achieve puzzle goals using 3D assets as building blocks
    2. Associated audio triggered by specific actions. For example, sword swining or door opening
    3. Players are confined to the world appropriately
    4. Animated objects such as moving platforms and blaster beams
    5. Character has 6 DOF
    6. Consistent character controls
5. Artifical Intelligence
    1. AI animation control and finite state machine developed by our team
    2. AI reacts and changes state based on player's behaviour
    3. Smooth transition of states
    4. AI only killable with use of TimeShift functionality
6. Polish
    1. Start menu GUI (with thematic background music)
    2. Pause menu with ability to quit
    3. Unified (cyberpunk) theme
    4. Sounds syncs up with visual activities

## Additional Resources
### Models & Animations 
Protagonist Model 
- [FGC Male Adam by Freedom's Gate](https://assetstore.unity.com/packages/3d/characters/humanoids/fgc-male-adam-70002) 

Boss Enemy Model
- [Bodyguards by Batewar](https://assetstore.unity.com/packages/3d/characters/humanoids/bodyguards-31711)

Character Animations 
- [Basic Motions FREE Pack by Kevin Iglesias](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271) 
- [Warrior Pack Bundle 2 Free by Explosive](https://assetstore.unity.com/packages/3d/animations/warrior-pack-bundle-2-free-42454) 

Character Sword Model 
- [LowPoly Cyber Ninja Sword by Cole](https://assetstore.unity.com/packages/3d/props/weapons/lowpoly-cyber-ninja-sword-129464) 

Robot Enemy Model & Animations 
- [Sci-Fi Soldier](https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi-soldier-29559)
- [Volumetric Lines](https://assetstore.unity.com/packages/tools/particles-effects/volumetric-lines-29160)

Environment
- [Futuristic Panel Textures LITE](https://assetstore.unity.com/packages/2d/textures-materials/futuristic-panel-textures-lite-80176)
- [Sci-Fi Styled Modular Pack by karboosx](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913)
- [Starfield Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/starfield-skybox-92717)

Spaceship Model
- [Destructor Spaceship by Eldanu](https://assetstore.unity.com/packages/3d/vehicles/space/destructor-spaceship-3229)


### Audio
SFX
- [Star Wars DC-15A Blaster Rifle Sound Effect](https://www.youtube.com/watch?v=KM3IWzhBIHw)
- [Lightsaber Sound Effect HQ - HD](https://www.youtube.com/watch?v=__sDEWIjQ_g)

Voice
- [Robotic Voice Generator](https://lingojam.com/RobotVoiceGenerator)
- [Sound Effect Lab](https://soundeffect-lab.info/)

Background Music
- [CXNTRAST - Saturn](https://www.youtube.com/watch?v=B5sNT98RVOU)

### Scripts 
- EventManager.cs - Jeff Wilson for CS4455/CS6457 Spring 2020 Video Game Design at Georgia Institute of Technology

## Testing Steps
1. Launch game
2. Observe dynamic start menu of moving robot and background music
3. Start game with "Start"
4. Listen to the goal stated by the robotic voice
5. Move using forward and backward keys
6. Rotate using mouse
7. Jump with "Jump" button, mapped to spacebar by default
8. Pick up sword right ahead
9. Test sword with "Fire1" button, mapped to left click and left ctrl key by default
10. Listen to sword swing sound effect
11. Move through door
12. Listen to door opening sound effect
13. Test TimeShift ability with "Fire2" button, mapped to right left click and left alt key by default
14. Observe TimeShift gauge going down
15. Turn off TimeShift ability
16. Notice TimeShift gauge refilling
17. Walk down stairs and observe enemy stopping patrol, and engaging player
18. Die to laser beams
19. Observe ragdoll animation
20. Restart game with "Rewind Time"
21. Collect sword, slow time down, kill the enemy
22. Proceed through the door and the hallway to reach the moving platforms
23. Jump down the abyss and observe the constrained play environment
24. Repeat the above and reach the moving platforms
25. Slow time down
26. Jump across the slow moving platforms to reach the other side
27. Go through the door to clear the stage
28. Restart the level with "Rewind Time"
29. Press the esc key to pause
30. Exit game with "Exit Game"

## Scene File to Open
"Main Menu Fix.unity" found in Assets/Scenes
