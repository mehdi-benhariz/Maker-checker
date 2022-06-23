using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.models.DTO
{
    public class AuthController : ControllerBase
    {
        private readonly RequestDataStore _requestDataStore;
        public AuthController(RequestDataStore requestDataStore)
        {
            _requestDataStore = requestDataStore;
        }


    }
}