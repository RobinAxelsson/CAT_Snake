# Catia V5 Part Design Snake Game in C#

> This project is done with an ambition to distract CAD Designers/Engineers on their path to build cars, airplanes and things you can find in the aero space industry.
>
> //Robin

Catia V5 is a software I used a lot as a Design Engineer student and later as employed engineer in the automotive industry of Gothenburg. Catia V5 is from 1995 and has the scripting language VBScript built in the software, also the VBA editor is built in which gives you some help with debugging (if you are comfortable with the VBA editor). I was not really, at the time I started to script Catia I was getting familiar with Visual Studio and C# which is a lot more helpful writing code.

Catia scripting was the first I tried in 2020 and I enjoy making better tools for engineering.

## Overview ##

- C# Console app

- Using the COM-libraries of Catia V5

- At the moment working only with Part Design work bench.

## Ready Features ##

- Multiple Snake objects made of cubes moving around

- Creating points, lines, planes, axis systems, extracts, thickness, bodies, geometrical sets...

- Wrapper classes for the above objects.

- Startup and teardown for a game session.

## Missing Features ##

- Controls

- Implementation firing events in the actual Catia window (now only one direction code, the console app -> Catia V5 app not Console app <-> Catia V5).

- SignalR multiplayer

## Inspiration ##

- There are a [Catia Snake game](https://www.youtube.com/watch?v=ywNO1ztY-0E&t=5s) on Youtube but I haven't found any source code.

- [Roland T CADs videos](https://www.youtube.com/watch?v=IT9QzVj8ghg&list=PLhZd1DP2sOEquml57lek0Hx_6Ryh5DOlo) on setting up a Catia project in Visual Studio.

- Some scripts on the https://www.scripting4v5.com/
