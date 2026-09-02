## Context

First attempt at making a game from scratch. Tried to replicate Unity's game object/component system, but became quite
complex and hard to manage later on. The game was a continuation in a way of a game I attempted to make years ago. I
retained the art, but most of the gameplay elements have change since then. To motivate the project, the Brackeys game
jam 2026.2 was used.

## Reasonining

- Wanting to create an engine to see how it works.
- A desire for a 2D engine that has a comprehensive game object system with inheritance and retained mode UIs -
  something missing from other 2D game engines.
- A lighter-weight engine that has small export sizes but retains features.

## What I Learnt

- Components system is hard to manage without a GUI. References have to be parsed around, leading to hard to follow code
  and also more time being spent manging dependencies between objects.
- Text rendering seems to be broken for the Staatlitches font and for text that changes often. An alternative would be
  to use the stb true type library.
- The scope was too ambitious for a week long project.
- Needs a UI system as all the values were hard-coded.

## What Next

- Create an improved engine that fixes the above points. For example, having a resource manager object which can store
  global references to objects which can be referenced in any class. This would be equivilant to Unity's GetComponent,
  but would work for all game objects.
    - Create new systems such as a retained-mode UI system. This would make the bulk of the gameplay easier to add, as
      one
      of the greatest challenges was the UI.
- Find a way to have hot-reloading features and better crash handling. This is probably due to using SDLs callbacks.
