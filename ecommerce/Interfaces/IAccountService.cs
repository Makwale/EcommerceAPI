using ecommerce.DTO;

namespace ecommerce.Interfaces
{
    public interface IAccountService
    {
        public Task<UserResponse> CreateUser(UserRequest userRequest);
        public Task<UserResponse> Login(LoginRequest loginRequest);

    }
}
