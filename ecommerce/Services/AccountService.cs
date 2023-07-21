using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using ecommerce.Data;
using ecommerce.DTO;
using ecommerce.Entities;
using ecommerce.Interfaces;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Domain
{
    public class AccountService: IAccountService
    {
        private DataContext _context;
        private FirebaseAuthProvider _authProvider;
        public AccountService(DataContext dataContext) { 
            _context = dataContext;
            _authProvider = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyD31j8I9pR55IHOjUu8dXJk-sqSj_Zqp84"));
        }

        /// <summary>
        /// This method creates user account
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async  Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            try
            {
                var userDb = _context.User.ToList().Where( user => user.Email == userRequest.Email);

                if (userDb.Any())
                {
                    throw new Exception($"User with email {userRequest.Email} exists");
                }

                var firebaseAuthLink =  await _authProvider.CreateUserWithEmailAndPasswordAsync(userRequest.Email, userRequest.Password);

                var user = new Customer
                {
                    Id = firebaseAuthLink.User.LocalId,
                    Name = userRequest.Name,
                    Email = userRequest.Email,
                    Password = userRequest.Password,
                    Surname = userRequest.Surname,
                    PhoneNo = userRequest.PhoneNo,
                    Username = userRequest.Username
                };

                await _context.User.AddAsync(user);

                await _context.SaveChangesAsync();

                var userResponse = new UserResponse
                {
                    Id = firebaseAuthLink.User.LocalId,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Surname = user.Surname,
                    PhoneNo = user.PhoneNo,
                    Username = user.Username
                };

                return userResponse;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> Login(LoginRequest loginRequest)
        {
            var firebaseAuthLink = await _authProvider.SignInWithEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

            var user = await _context.User.FindAsync(firebaseAuthLink.User.LocalId);

            var userResponse = new UserResponse
            {
                Id = firebaseAuthLink.User.LocalId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Surname = user.Surname,
                PhoneNo = user.PhoneNo,
                Username = user.Username
            };

            return userResponse;
        }
    }
}
