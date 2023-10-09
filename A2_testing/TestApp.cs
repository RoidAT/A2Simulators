/*
 * TestApp.cs
 * ---
 * Author: René Schütz, Marcel Schörghuber, Marlene Berger
 * Diese Test App erstellt einige Simulatoren und verknüpft diese miteinander.
 * Anschließend wird die Simulation ausgeführt und die Ausgangsleistung der PhotovoltaikAnlage ausgegeben.
 * ---
 */

using System;
using System.Threading;
using SIM;

long simulationsGeschwindigkeit = 1000;

var simulation = new Simulation();

// Erstellen Sie einige Simulatoren
var solarmodul1 = new Solarmodul();
var solarmodul2 = new Solarmodul();
var solarmodul3 = new Solarmodul();
var solarmodul4 = new Solarmodul();
var solarmodul5 = new Solarmodul();
var solarmodul6 = new Solarmodul();
var solarmodul7 = new Solarmodul();
var solarmodul8 = new Solarmodul();
var solarmodul9 = new Solarmodul();
var solarmodul10 = new Solarmodul();
var solarmodul11 = new Solarmodul();
var solarmodul12 = new Solarmodul();
var solarmodulString = new SolarmodulString();
var wechselrichter = new Wechselrichter();
var wechselrichterNetz = new Wechselrichter();
var photovoltaikAnlage = new PhotovoltaikAnlage();
var batteriemodul = new Batteriemodul(100000);
var batteriemodul2 = new Batteriemodul(100000);
var batteriespeicher = new Batteriespeicher();
var ladestation = new Ladestation(6000);
var ladestation2 = new Ladestation(2000);
var ladestation3 = new Ladestation(1000);
var ladestation4 = new Ladestation(3000);
var ladepark = new Ladepark();

// Verknüpfen Sie die Simulatoren
solarmodulString.Connect(solarmodul1);
solarmodulString.Connect(solarmodul2);
solarmodulString.Connect(solarmodul3);
solarmodulString.Connect(solarmodul4);
solarmodulString.Connect(solarmodul5);
solarmodulString.Connect(solarmodul6);
solarmodulString.Connect(solarmodul7);
solarmodulString.Connect(solarmodul8);
solarmodulString.Connect(solarmodul9);
solarmodulString.Connect(solarmodul10);
solarmodulString.Connect(solarmodul11);
solarmodulString.Connect(solarmodul12);
wechselrichter.Connect(solarmodulString);
//photovoltaikAnlage.Connect(solarmodulString);
photovoltaikAnlage.Connect(wechselrichter);
batteriemodul.Connect(photovoltaikAnlage);
batteriemodul2.Connect(photovoltaikAnlage);
batteriespeicher.Connect(batteriemodul);
batteriespeicher.Connect(batteriemodul2);
ladepark.Connect(wechselrichterNetz);
ladestation.Connect(ladepark);
ladestation2.Connect(ladepark);
ladestation3.Connect(ladepark);
ladestation4.Connect(ladepark);
wechselrichterNetz.Connect(batteriespeicher);

// Fügen Sie die Simulatoren zur Simulation hinzu
simulation.AddSimulator(photovoltaikAnlage);
simulation.AddSimulator(batteriespeicher);
simulation.AddSimulator(ladepark);

// Führen Sie die Simulation aus


for(long i = 0; i < 922372036854775807; i+= 250 * simulationsGeschwindigkeit)
{
    long hour = (i / 3600000) % 24;
    simulation.Run(i);
    Console.WriteLine($"Hour: {hour}");
    Console.WriteLine($"Ausgangsleistung der PhotovoltaikAnlage: {photovoltaikAnlage.GetOutput()} W");
    Console.WriteLine($"Cap1: {batteriemodul.GetOutput()} Wh");
    Console.WriteLine($"Cap2: {batteriemodul2.GetOutput()} Wh");
    Console.WriteLine($"CapGes: {batteriespeicher.GetOutput()} Wh");
    Console.WriteLine($"Station1: {ladestation.GetOutput()} W");
    Console.WriteLine($"Station3: {ladestation2.GetOutput()} W");
    Console.WriteLine($"Station3: {ladestation3.GetOutput()} W");
    Console.WriteLine($"Station4: {ladestation4.GetOutput()} W");
    Console.WriteLine($"Park: {ladepark.GetOutput()} W");
    Console.WriteLine($"WechselrichterNetz: {wechselrichterNetz.GetOutput()}");
    Console.WriteLine("");
    Console.WriteLine("");
    Thread.Sleep(100);
}


