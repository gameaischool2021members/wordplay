# wordplay

We created a random map generator that created over 55 thousand random layouts. These layouts was then feeded into a NN to make semantic annotations. We are going to create dungeons out of natural language by asking the NN to create dungeons with specific room nubers and/or differnt difficulty levels.

We also added some gameplay mechanics, simple enemy behaviours and some 3D asstes in order to use those generated maps in a mini dungeon crawler game.

This app allows the use of natural language prompts to generate maps for a 2d dungeon crawler, using a variety of semantic information:
- structure: "A map with a high number of rooms", "A map with twelve total rooms"
- difficulty: "A low difficulty map"
- structure and difficulty: "A very high difficulty map with nine total rooms"
- specific space properties: "A map with two easy rooms", "A map with three normal rooms"

The range and scales used for semantic annotation are the following, but natural language prompts outside of these might work!
- number of rooms: 7-50 rooms
- difficulty & structure: 'very low', 'low', 'average', 'high', 'very high'

**Team Members**
- Theodoros Galanos
- Konstantinos Banakakis
- Christos Davillas
- Vladimir Skabelkin




https://user-images.githubusercontent.com/18182924/125065058-08234480-e0ba-11eb-80e5-f36e923d9a3e.mp4

