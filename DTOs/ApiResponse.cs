namespace ECommerceApp.DTOs
{
    // Standardized API response structure.
    // T represents then Type of the response data
    public class ApiResponse<T>
    {
        // HTTP status code of the response.
        public int StatusCode { get; set; }

        // Indicates whether the request was successful.
        public bool Success { get; set; }

        // Response data in case successful
        public T Data { get; set; }

        // List of error messages, if any.
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Success = true;
            Errors = new List<string>();
        }

        public ApiResponse(int statusCode, T data)
        {
            StatusCode = statusCode;
            Success = true;
            Data = data;
            Errors = new List<string>();
        }

        public ApiResponse(int statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Success = false;
            Errors = errors;
        }

        public ApiResponse(int statusCode, string error)
        {
            StatusCode = statusCode;
            Success = false;
            Errors = new List<string> { error };
        }
    }
}