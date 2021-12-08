# Travers-Cole-Coding-Test

Coding test for pikpok.

# How to use
In the repository is the following

- Visual studio project with the source files.
- Shortcut to a built executable in the debug folder.
- This readme.

## Starting the program
The project can be built from the vs project or the already existing excutable can be run.

## Using the program

The program consists of a console and a display window. Display window displays the scene of shapes and console is used for issuing commands.
Type 'help' in the console for a list of commands.

Commands allow you to:
- Spawn shapes.
- Set shapes animations.
- Set shapes color.
- Set shapes Position, Rotation and Scale.
- Load custom shapes and animations from a text file.

## Custom shapes

Custom shapes can be saved and loaded from a text file, the format of this is:

#Vertices:
//vertex positions
#VertexColors:
//vertex colors.

An example of this format describing a triangle is below:

#Vertices:
0, -25, 
-35, 25, 
35, 25, 
#VertexColors:
127, 255, 212, 
127, 255, 212, 
127, 255, 212, 

## Custom animations

Custom animations follow a sequence of positions in a loop.
They can be saved and loaded from a text file, the format is:

#Positions:
//Sequence of positions
#MovementRate:
//float movement rate

An example custom animation sequence.

#Positions:
30, 100
10, 300
300, 45
500, 500
#MovementRate:
50

# Design decisions

I used c# for the project as it is my preferred language.
For rendering I used openGL for graphics API and Glfw for the display window loop.

Shapes:
To define shapes I used an abstract class that handle all of the logic for rendering and animating the a shape. 
This meant superclasses were responsible for defining the vertices of the shape.

Animation:
Animation used an abstract class used to store the animations movement rate and an abstract method that provides the shapes transform and deltaTime.
Shapes transform was used to define the shapes position, rotation and scale in the scene. This, the movement rate and the deltaTime, 
was all that was required to animate a shape in any given way by the superclasses.

Commands:
I created a console command line interface as it seemed the quickiest way of creating an interface for controlling shapes at runtime.
To do this is read the console input async on a different thread and added any inputs to a thread safe concurrrent queue.
These inputs could then be processed by the main thread. 
Using abstract command base class I could create super classes that could handle a generic input. These generic command classes could then be used in many different ways
to create the different commands needed to interface with the shapes.


