﻿/*
 * Simulation.cs
 * ---
 * Author: René Schütz
 * Der Simulationsklasse kann mehrere Simulations Objekte gegeben werden welche durch die Step Methode simuliert werden.
 * 
 * ---
 */
 
using System.Collections.Generic;

namespace SIM
{
    public class Simulation
    {
        public List<ISimulator> Simulators { get; private set; } = new List<ISimulator>();

        // Ein Simulator zur Simulation hinzufügen
        public void AddSimulator(ISimulator simulator)
        {
            Simulators.Add(simulator);
        }

        // Die Step Methode für alle Simulatoren ausführen
        public void Run(long timeMs)
        {
            foreach (var simulator in Simulators)
            {
                simulator.Step(timeMs);
            }
        }
    }
}