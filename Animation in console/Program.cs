// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using SimulationGame.Game;
using SimulationGame.Game.NPCs;

Console.WriteLine("Hello, World!");
World world = World.This();
new Sheep();
new Sheep();
new Sheep();
new Sheep();
string ifTerminateGame = "1";
while (ifTerminateGame != "0")
{
    world.TakeATurn();
    ifTerminateGame = Console.ReadLine();
}


