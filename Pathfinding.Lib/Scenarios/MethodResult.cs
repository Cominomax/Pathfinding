namespace Pathfinding.Lib.Scenarios
{
    public struct MethodResult
    {
        internal static MethodResult WithSuccess { get; } = new MethodResult(true, "Success");

        public MethodResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
