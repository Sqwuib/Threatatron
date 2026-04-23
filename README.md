# Threatotron

Threatotron is a C# console application developed for CAB201 Programming Principles at Queensland University of Technology. The program simulates an obstacle avoidance and pathfinding system where users can place hazards on a grid, inspect map locations, and calculate safe routes for an agent.

## Overview

The project was designed to demonstrate core programming principles including:

- Object-oriented programming
- Class design and encapsulation
- Input handling and validation
- Grid-based logic
- Pathfinding algorithms
- Modular code structure
- Console application development in C#

## Features

Users can interact with the system through text commands to:

- Add guard obstacles with directional coverage
- Add fences with configurable orientation and length
- Add sensors with radius detection
- Add cameras with directional vision
- Check whether a location is safe
- Display a text-based map of the environment
- Generate a safe path for an agent to a target destination
- View help commands
- Exit the application

## Technologies Used

- C#
- .NET
- Console Application
- Object-Oriented Programming

## Project Structure

The application is organised into multiple classes, including:

- `Program.cs` – entry point of the application
- `Menu.cs` – user interface and command menu
- `CommandHandler.cs` – command processing
- `ObstacleSystem.cs` – obstacle management
- `FindPath.cs` – pathfinding logic
- `GridBuilder.cs` – map generation
- Obstacle classes such as Guard, Fence, Camera, Sensor

## How to Run

### Requirements

- .NET SDK installed

### Run Locally

```bash
dotnet build
dotnet run
