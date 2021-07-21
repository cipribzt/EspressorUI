# EspressorUI

# General Description

The Espressor works as a state machine with 26 states and 11 possible actions to navigate through states. TO BE INSERTED - SCREENSHOT WITH TABEL

The application receives commands from the user via the console to prepare Espresso. The classes for this aplication are Pot, Plate, Boiler and Espressor. 

![alt text](https://github.com/cipribzt/EspressorUI/blob/master/Class%20Diagram.jpg)

Espressor contains a map of that has as key the current state, and as value a list with the possible actions from that state and their destination states. FillTransitionMap()
fills the map with the necesary data for each state. 


# How to install

1. Clone the project from Git

2. Open with your favorite IDE

3. Run the main class Program.cs

# Execute the Program

1. Run the project

2. The program will suggest you the available actions according to the state the espressor is in

3. You type an action through the console (Ex: AddWater)

4. Navigate through states until the desired result. 

To end the program the user has to type the FINISH command. 
