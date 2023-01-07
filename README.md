# Forest Dash
#### Video Demo: https://www.youtube.com/watch?v=CqMTOhxm3qY
## Description:

Forest Dash is an endless runner game set in a forest environment. As you run through the forest, you will need to pick up as much coin as you can and avoid obstacles in order to get the highest running distance possible.

To play the game, simply choose your character and dash! The game will end if you collide with an obstacle, so be sure to keep an eye out for trees, logs, and other hazards as you run.

You can use the coins to unlock new character.

### Control:

To change lane: use the left arrow or right arrow to move left to right. You can also swipe with your fingers. 

To jump, use the jump botton at the bottom of the screen.

## Game Design

## Goal

The goal was to create a game under 2 weeks due to other incomings commitments. Due to that, I took a simple game to make and used a lot of free assets available in the unity asset store.

The game in total took me 10 days to develop. Probably around 60 hours. 

## Scene

The game is devised in 5 scenes:
- Main Menu (Where player choose his character)
- Game Menu (Where the dash through the forest happen)
- Game Over
- Store
- Leaderboard


## Map generations

The map is generated randomly. Each side has 5 different template that has been made as prefab. Each one have been personalized to be unique. With this principle, this game can go infinitly. Each part have been done with low-poly environement package from unity.

The clouds and coins are also generated randomly.

## Obstacle generations

Obstacles are spawn at random distance from each other. There 3 scrips that happen at the same time, log (low obstacle but wide as the trail), small obstacle (that can be jump over) and tall obstacles.

On every script there also code that was added to make sure that no objects are created over any other objects.


## Player movement & Objects 

Player only move in 1 direction (on X axis). The speed is incrementend at random interval so the game become harder more the player is far. All game objects are destroyed and spawned depending of the player location to limit requirement to play the game.

# Special Unity Asset

## Amazing Asset Curved World

## Link : https://assetstore.unity.com/packages/vfx/shaders/curved-world-2020-173251

I decided to buy this asset to facilitate the implementation of a random curved world. As it's a paid asset, I didn't include the script in this directory. The curve world rendering can be done without this script, but it did make my life way easier and I was able to focus on other functionality.

# Conclusion

This was definitivly a good learning curve to do a project A to Z and to officially publish it on the Google Store. I'm not expecting it to be popular as the replay value is low. But I'm really happy with what I have accomplished. I had a couple of other ideas for video game that I wanted to do that
could have been a lot more interesting. But, the time to developpe them, would have been months.

