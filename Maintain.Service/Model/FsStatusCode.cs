using System;

namespace IIMes.Services.Maintain.Model
{
    public enum FsStatusCode
    {
        /// <summary>
        /// 未上料
        /// </summary>
        NONE = -1,

        /// <summary>
        /// 已下料
        /// </summary>
        LAYINGOFF = 0,

        /// <summary>
        /// 已上料
        /// </summary>
        FEEDING = 1
    }
}
