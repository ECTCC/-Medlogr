namespace µMedlogr.core.Exceptions;
internal class TemperatureOutOfRangeException(string? paramName) : ArgumentOutOfRangeException(paramName) {
}
