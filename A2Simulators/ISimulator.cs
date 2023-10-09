/*
 * ISimulator.cs
 * ---
 * Author: René Schütz
 * This interface is used to create a simulator object.
 * In the simulation the simulator objects are connected to each other. and are calling the Step method on each other.
 * ---
 */

using System.Collections.Generic;

namespace A01
{
    public interface ISimulator
    {

        /// <summary>
        /// Current Simulator Time
        /// </summary>
        long CurrentTime { get; set; }

        /// <summary>
        /// All Simulatorobjects that are Inputs of the Current object
        /// </summary>
        List<ISimulator> ConnectedInputs { get; set; }

        /// <summary>
        /// All Simulatorobjects that are Outputs of the current object
        /// </summary>
        List<ISimulator> ConnectedOutputs { get; set; }
        /// <summary>
        /// Connects a Simulatorobject as an Input and adds it to ConnectedInputs. Also adds the object to ConnectedOutputs of the selected input.
        /// </summary>
        /// <param name="input"></param>
        void Connect(ISimulator input);

        /// <summary>
        /// Simulate until input time
        /// </summary>
        /// <param name="time"></param>
        void Step(long time);

        /// <summary>
        /// Gets the Output of the Simulatorobject
        /// </summary>
        /// <returns></returns>
        object GetOutput();
    }
}