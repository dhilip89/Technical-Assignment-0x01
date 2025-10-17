namespace XYZ.Platform.Core.Application.Model
{
    public sealed class ValidationFailedReason
    {
        public required string Field { get; set; }

        public required string Detail { get; set; }
    }
}
