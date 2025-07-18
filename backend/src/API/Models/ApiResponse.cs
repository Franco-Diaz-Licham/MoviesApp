﻿namespace backend.src.API.Models;

public class ApiResponse
{
    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public ApiResponse(int statusCode, object data)
    {
        StatusCode = statusCode;
        Message = GetDefaultMessageForStatusCode(statusCode);
        Data = data;
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "That was a bad request...",
            401 => "Unauthorized...",
            404 => "Resource was not found...",
            500 => "There was an internal error...",
            200 => "Response okay...",
            _ => null
        };
    }
}
