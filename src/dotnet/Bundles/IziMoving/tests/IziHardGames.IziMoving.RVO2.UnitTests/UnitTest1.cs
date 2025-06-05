using RVO;

namespace IziHardGames.IziMoving.RVO2.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sim = Simulator.Instance;
            sim.setTimeStep(0.025f);
            sim.setAgentDefaults(neighborDist: 15.0f, 10, 10.0f, 10.0f, 1.5f, 2.0f, new Vector2(0.0f, 0.0f));
            var a1 = sim.addAgent(default);
            var posA1 = sim.getAgentPosition(a1);
            var a2 = sim.addAgent(default);
            var posA2 = sim.getAgentPosition(a2);

            sim.doStep();
            var a3 = sim.addAgent(default);
            var posA3 = sim.getAgentPosition(a3);
            var a4 = sim.addAgent(default);
            var posA4 = sim.getAgentPosition(a4);
            var a5 = sim.addAgent(default);
            var posA5 = sim.getAgentPosition(a5);

            //sim.RemoveAgent(a5);
            sim.doStep();
            sim.doStep();
            sim.doStep();

        }
    }
}