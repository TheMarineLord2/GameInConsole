// See https://aka.ms/new-console-template for more information
using SimulationGame;
using SimulationGame.NPCs;

Console.WriteLine("Hello, World!");
World mainW = World.GetInstance();
mainW.AddEntity(new Dandelion());
mainW.AddEntity(new Dandelion());
mainW.AddEntity(new Dandelion());
mainW.TakeATurn();