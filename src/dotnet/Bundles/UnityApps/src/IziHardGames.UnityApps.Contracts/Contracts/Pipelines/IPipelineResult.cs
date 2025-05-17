/*
Pipeline - более сложный процесс, который требует больше контроля. Упор делается на гибкость, а не производительность
*/

namespace IziHardGames.UnityApps.Contracts.Pipelines
{
    public interface IPipelineResult
    {
        bool IsEnded { get; }
        bool IsCompleted { get; }
        bool IsCanceled { get; }
        bool IsFailed { get; }
        bool IsRunning { get; }
        bool IsScheduled { get; }
    }
}
