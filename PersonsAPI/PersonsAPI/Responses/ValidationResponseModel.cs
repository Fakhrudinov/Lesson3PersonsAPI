using System.Collections.Generic;

namespace PersonsAPI.Responses
{
    public class ValidationResponseModel
    {
        public ValidationResponseModel()
        {
            IsValid = true;
            ValidationMessages = new List<string>();
        }
        public bool IsValid { get; set; }
        public List<string> ValidationMessages { get; set; }
    }
}
