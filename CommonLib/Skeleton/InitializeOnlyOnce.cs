using CommonLib.Interfaces;

namespace CommonLib.Skeleton
{
    /// <summary>
    /// 代表只可以初始化一次的类型
    /// </summary>
    public abstract class InitializeOnlyOnce : IInitializable
    {
        #region 字段定义

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// 互斥量
        /// </summary>
        private object mutex;

        /// <summary>
        /// 初始化参数
        /// </summary>
        private object initArgs;

        #endregion

        #region 属性定义

        /// <summary>
        /// 获取初始化参数
        /// </summary>
        protected object InitArgs
        {
            get
            {
                return this.initArgs;
            }
        }

        /// <summary>
        /// 获取是否已经初始化的标识
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
        }

        #endregion

        #region 构造函数定义

        /// <summary>
        /// 创建类型实例
        /// </summary>
        protected InitializeOnlyOnce()
        {
            this.mutex = new object();
            this.isInitialized = false;
            this.initArgs = null;
        }

        #endregion

        #region public方法定义

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.Initialize(null);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initArgs">初始化参数</param>
        public void Initialize(object initArgs)
        {
            lock (this.mutex)
            {
                if (!this.isInitialized)
                {
                    this.InnerInitialize();
                    this.isInitialized = true;
                    this.initArgs = initArgs;
                }
            }
        }

        /// <summary>
        /// 终结并清理资源
        /// </summary>
        public void Terminate()
        {
            lock (this.mutex)
            {
                if (this.isInitialized)
                {
                    this.InnerTerminate();
                    this.isInitialized = false;
                }
            }
        }

        #endregion

        #region protected方法定义

        /// <summary>
        /// 子类实现的初始化
        /// </summary>
        protected abstract void InnerInitialize();

        /// <summary>
        /// 子类实现的终结并清理资源
        /// </summary>
        protected abstract void InnerTerminate();

        #endregion
    }
}