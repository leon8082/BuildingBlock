namespace CommonLib.Interfaces
{
    /// <summary>
    /// 表示类型可以被初始化（准备资源）和终结（释放资源）
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// 获取是否已经初始化成功的标志
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 终结并清理资源
        /// </summary>
        void Terminate();
    }
}