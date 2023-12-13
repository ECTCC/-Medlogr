using System.Runtime.Serialization;

namespace µMedlogr.core.Exceptions;
internal class TemperatureOutOfRangeException(string? paramName, object? actualValue, string? message) : ArgumentOutOfRangeException(paramName, actualValue, message) {
    public TemperatureOutOfRangeException() : this (null, null, null){
    }
    public TemperatureOutOfRangeException(string? paramName, string? message) : this(paramName, null , message) {
    }
}
