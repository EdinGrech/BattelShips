# BattelShips

### Description:

An object oriented programming assignment to create a battle ships game. The game runs in a console window and uses an SQL data base to store user data.

## Instillation

### Requirements

- .Net 8 is required
- NuGet Package manager

### Data Base set up

Run the migration command in the package manager console:

```
Update-Database initialDBCreation
```

Note the DB can be dropped with the following command:

```
Drop-Database
```

## Usage

### Game Flow

After you have set up the data base and build the project the following life cycle has been implemented:

- The players must first lock into a game by logging in or creating an account
- Players must then configure their ships on their respective boards
  - This will create 2 states that will persist through out the game and saved once configuration is complete.
- Players are then able to select the attack cycle option that will ask both players to attack on another saving the the changes both to the state and DB after reach round
- Players may quite the game at any time during the game when the menu is available.

==Note due to the unclear nature of the assignment the flow is not optimised for player experience nor is the information provided to the user as to best adhear to the assignment criterea.==

## Architecture

The game is with 4 layers:

- Management layers
  - This is the game's top level that can be used in the future to add additional services that would have to be start up individually or in a particular sequence.
  - This layer could additionally be used to create a CLI tool chain to better maintain the project
- Business Layer
  - Final Data validation, conversion and save controllers are found in this layer
  - This layer also houses the game loop as well as the game state
- Data Layer
  - This layer primary contains the ORM classes for the DB as well add accessors to the data going to and from the DB
  - The layer also has frequently used structures that are used through out the project. This was done to retain consistency through out.
- Presentation Layer
  - This player contains the grid rendering engine
  - Basic validation to so the user can be prompted to enter valid data
  - Condensed methods housing housing player flows.
    - Player place ships
    - Player attack

## Improvement

- Hidden password method
  - User cant remove the password
- Testing
  - Create unite tests
- Improved structure ==(The structure was never made clear in the assignment leading to sudden changes mid development. To minimise the risk or a rewrite most of the program was made to be very dynamic and could easily have large sections refactored with ease)==
  - Improved validation
  - Optimisations Processing
    - I am sure there is a few loops that could have been made more concise to save on processing time
  - Optimise State
    - If we had a clear picture from the start a Single game state could have been implemented to reduce any technical debt

*PS Typo is a tribute to the chaotic state of the assignment.*
