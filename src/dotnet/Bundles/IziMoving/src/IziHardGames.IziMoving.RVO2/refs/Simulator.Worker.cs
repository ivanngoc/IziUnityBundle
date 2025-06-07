/*
 * Simulator.Worker.cs
 * RVO2 Library C#
 *
 * SPDX-FileCopyrightText: 2008 University of North Carolina at Chapel Hill
 * SPDX-License-Identifier: Apache-2.0
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Please send all bug reports to <geom@cs.unc.edu>.
 *
 * The authors may be contacted via:
 *
 * Jur van den Berg, Stephen J. Guy, Jamie Snape, Ming C. Lin, Dinesh Manocha
 * Dept. of Computer Science
 * 201 S. Columbia St.
 * Frederick P. Brooks, Jr. Computer Science Bldg.
 * Chapel Hill, N.C. 27599-3175
 * United States of America
 *
 * <http://gamma.cs.unc.edu/RVO2/>
 */

using System.Threading;

namespace RVO
{
    public partial class Simulator
    {
        /**
         * <summary>Defines a worker.</summary>
         */
        private class Worker
        {
            private readonly ManualResetEvent doneEvent_;
            private readonly int end_;
            private readonly Simulator simulator;
            private readonly int start_;

            /**
             * <summary>Constructs and initializes a worker.</summary>
             *
             * <param name="start">Start.</param>
             * <param name="end">End.</param>
             * <param name="doneEvent">Done event.</param>
             */
            internal Worker(Simulator simulator, int start, int end, ManualResetEvent doneEvent)
            {
                this.simulator = simulator;
                start_ = start;
                end_ = end;
                doneEvent_ = doneEvent;
            }

            /**
             * <summary>Performs a simulation step.</summary>
             */
            internal void step(object _)
            {
                for (int agentNo = start_; agentNo < end_; ++agentNo)
                {
                    simulator.agents_[agentNo].computeNeighbors();
                    simulator.agents_[agentNo].computeNewVelocity();
                }

                doneEvent_.Set();
            }

            /**
             * <summary>updates the two-dimensional position and
             * two-dimensional velocity of each agent.</summary>
             */
            internal void update(object _)
            {
                for (int agentNo = start_; agentNo < end_; ++agentNo)
                {
                    simulator.agents_[agentNo].update();
                }

                doneEvent_.Set();
            }
        }
    }
}
