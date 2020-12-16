namespace IIMes.Infrastructure.Exception
{
    public enum Level
    {
        /// <summary>
        /// Info will be returned to service client, no influence on wf instance.
        /// </summary>
        Info,

        /// <summary>
        /// Warning will be returned to service client, no influence on wf instance.
        /// </summary>
        Warning,

        /// <summary>
        /// Error will be returned to service client,
        /// it will cause subsequent activites to be skipped,
        /// but it won't stop wf instance.
        /// </summary>
        Error,

        /// <summary>
        /// fatal exception will cause a wf instance being
        /// terminated immediately
        /// </summary>
        Fatal,
    }
}
