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
    2. Goal to kill the enemy and reach the end is intuitive. Instructions are also easily available on the main menu
    3. Game over and stage clear menus implemented
    4. "Rewind Time" as a flavourful way to restart the level
    5. Checkpoint system so you repsawn in the room you've last reached. Prevents players from feeling overly frustrated at having to constantly restart the whole level
    6. Third person slasher
2. Fun Gameplay
    1. Main goal available in game as a robotic voice and on the start menu
    2. Variety of skills available to players
        a. TimeShift
        b. Dash
        c. Double jump
        d. Air dash
    3. Skills can be combined together (eg. slow time + jump) in a variety of different ways to tackle both the puzzles and enemies
    4. Different skills work better on different enemies. It is more effective to slow time and kill a ranged enemy as compared to a melee enemy. However, a dash works better at getting close to a meele enemy to kill them.
    5. Dash and TimeShift both consume your time gauge, so you have to choose wisely which to use when confronted with different types of enemies at the same time
    6. Not all enemies have to be killed by design. It might sometimes make more sense to dash through a closing door then engage with the enemy. This is still not a trivial task as a certain combinations of skills (eg. double jump + air dash) must be done rapidly and timed well
    7. TimeShift limited to refilling gauge, players are challenged to think on how they would want to manage this finite resource.
    8. Players engage with interactive platforms and enemies
    9. Death awaits those who fail puzzles or get hit by enemies
    10. Players start of in a controlled tutorial with only 1 simple enemy. Puzzles ramp up in difficulty in later levels with fresh puzzles each time
    11. Good balance of difficulty ensures that players are increasingly challenged, but never beyond what is frustrating. The checkpoint mechanic also helps
3. 3D Character with Real-Time Control
    1. Movement is key to solving puzzles and avoiding enemy attacks like in any third person slasher
    2. Control script and animation controller developed by our team
    3. Camera script developed by our team
    4. Multiple animations such as attacking blended together with movements
    5. Movement is tied to analog control. Simulated analog control available for keyboard by pressing number 1 - 9
    6. Camera is smooth and does not cut through any walls
    7. Main character ragdolls if killed
    8. Audio signals attacking functions
    9. Character does not pass through walls, even while dashing 
4. 3D World with Physics and Spatial Simulation
    1. Game world built by our team specifically to achieve puzzle goals using 3D assets as building blocks
    2. Associated audio triggered by specific actions. For example, sword swinging, door opening, or enemy dying
    3. Players are confined to the world appropriately with themed doors
    4. Animated objects such as moving platforms and blaster beams
    5. Fully working buttons that are animated and open doors differently depending on the timestream you're in
    6. Character has 6 DOF
    7. Consistent character controls. Not tied to framerate
    8. Visual shift in color to represent different flow of time. Fades in gradually.
5. Artifical Intelligence
    1. AI animation control and finite state machine developed by our team
    2. AI reacts and changes state based on player's behaviour
    3. Smooth transition of states when entering/exiting rooms.
    4. AI emits sounds when killed
    5. Difficulty of AI slowly increases in terms of quantity and also diversity
    6. Different skills are better against different AI
6. Polish
    1. Start menu GUI (with thematic background music)
    2. Pause menu with ability to quit
    3. Unified (cyberpunk) theme
    4. Sounds syncs up with visual activities
    5. No godmode accessible via built game
    6. Auditory effects for observable game events

## Additional Resources
### Models & Animations 
Protagonist Model 
- [FGC Male Adam by Freedom's Gate](https://assetstore.unity.com/packages/3d/characters/humanoids/fgc-male-adam-70002) 

Character Animations 
- [Basic Motions FREE Pack by Kevin Iglesias](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271) 
- [Warrior Pack Bundle 2 Free by Explosive](https://assetstore.unity.com/packages/3d/animations/warrior-pack-bundle-2-free-42454) 

Character Sword Model 
- [LowPoly Cyber Ninja Sword by Cole](https://assetstore.unity.com/packages/3d/props/weapons/lowpoly-cyber-ninja-sword-129464) 

Human Enemy Model
- [Bodyguards by Batewar](https://assetstore.unity.com/packages/3d/characters/humanoids/bodyguards-31711)

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
3. Click on instructions and read
3. Start game with "Start"
4. Listen to the goal stated by the robotic voice
5. Move using forward and backward keys
6. Rotate using mouse
7. Jump with "Jump" button, mapped to spacebar by default
8. Test sword with "Fire1" button, mapped to left click and left ctrl key by default
9. Listen to sword swing sound effect
10. Press "Fire3" button, mapped to left shift by default to dash
11. Notice that TimeShift gauge goes down
10. Move forward through door
11. Listen to door opening sound effect
12. Test TimeShift ability with "Fire2" button, mapped to right right click and left alt key by default
13. Observe TimeShift gauge going down
14. Turn off TimeShift ability
15. Notice TimeShift gauge refilling
17. Walk down stairs and observe enemy stopping patrol, and engaging player
18. Die to laser beams
19. Observe ragdoll animation
20. Restart game with "Rewind Time"
21. Collect sword, slow time down, kill the enemy
22. Proceed through the door and the hallway to reach the moving platforms
23. Jump down the abyss and observe the constrained play environment
24. Respawn in the same room due to the automatic checkpoint system
25. Repeat the above and reach the moving platforms
26. Slow time down
27. Jump across the slow moving platforms to reach the other side
28. Go through the door to clear the stage
29. Press next stage to load the next level
30. Slow down time
31. Run forward and ignore the enemy
32. Dash through the door. Unlikely to make it without dash.
33. Stop slowing down time and try to kill the enemy
34. Probably die
35. Slow down time and kill the enemy
36. Press both buttons while slowed
37. Enter next room and solve the puzzle
38. Notice new melee enemy. Dash for best results
39. Complete the level
40. Start in Level 2
41. The difficulty of the puzzles and enemy has increased in this level by changing environmental designs (eg. rotating platforms, enmeies across gaps, simultaneous enemies). However, the fundemental design remains the same and puzzles can be solved with clever combination of skills.
42. Finish the game. We hope you've enjoyed playing it as much as we've enjoyed building it :)

## Scene File to Open
"Main Menu Fix.unity" found in Assets/Scenes
